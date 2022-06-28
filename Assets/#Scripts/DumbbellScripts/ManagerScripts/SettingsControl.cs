using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using GameAnalyticsSDK;
using ElephantSDK;
using Lofelt.NiceVibrations;
using I2.Loc;

public class SettingsControl : MonoBehaviour
{
    [Header("===Game Info===")]
    public string appleStoreID;

    [Header("===Setting Variables===")]
    [Range(0,144)] public int frameRate = 60;
    [Range(0,1)] public float startTimeScale = 1f;
    public int totalLevelCount = 30;
    public bool debugEnabled = false;

    [Header("===Control Type===")]
    public bool joystick = false;
    public bool dragDrop = false;
    public bool swerve = false;
    [Header("===Drag'n Drop Variables===")]
    public float dragDropYOffset;
    public LayerMask draggableLayer, groundLayer;
    [Header("===Swerve Variables===")]
    public bool planeSwerve = false;
    public float swerveSideways, swerveForward, clampValue;
    
    [Header("===Audio & Haptics Settings===")]
    public bool audioEnabled = false;
    public bool haptics = false;

    [Header("===Haptic Clips===")]
    public HapticClip subtle;
    public HapticClip winHaptic;
    public HapticClip loseHaptic;
    public HapticClip errorClip;

    //Post Processing
    PostProcessLayer ppLayer;
    PostProcessVolume ppVolume;
    bool highQuality = true;
    [HideInInspector] public bool isRatingShown = false;
    int y;

    [HideInInspector] public UIControl uIControl;
    [HideInInspector] public GameControl game;
    [HideInInspector] public JoystickControl joystickControl;
    [HideInInspector] public DragDropControl dragDropControl;
    [HideInInspector] public SwerveControl swerveControl;
    CameraShakeReciever shakeReciever;


    public void InitializeSettings()
    {
        Application.targetFrameRate = frameRate;
        Time.timeScale = startTimeScale;  
        game.mainCam = Camera.main.gameObject;
        ppVolume = game.mainCam.GetComponent<PostProcessVolume>();
        ppLayer = game.mainCam.GetComponent<PostProcessLayer>();
        shakeReciever = FindObjectOfType<CameraShakeReciever>();
        AppraterScript.SharedController().AppId = appleStoreID;

        JoystickCheck();
        DragDropCheck();
        SwerveCheck();
        
        AnalyticsTracker("start");
        PopulateLevels();
        CustomDebug("System Event : Initialized Settings");
    }
#region Analytics Functions
    public void AnalyticsTracker(string y)
    {
        int x = PlayerPrefs.GetInt("unlockedLevel", 1);
        if(game.levelNumber > x) PlayerPrefs.SetInt("unlockedLevel", game.levelNumber);
        if(y == "start")
        {
            Elephant.LevelStarted(game.levelNumber);
            GameAnalytics.Initialize();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level " + game.levelNumber, game.levelTime.ToString("F0"));
            CustomDebug("Analytics Event : Level Started");
            return;
        }
        if(y == "lose")
        {
            Elephant.LevelFailed(game.levelNumber);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level " + game.levelNumber, game.levelTime.ToString("F0"));
            CustomDebug("Analytics Event : Level Lose");
        }
        if(y == "win")
        {
            Elephant.LevelCompleted(game.levelNumber);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level " + game.levelNumber, game.levelTime.ToString("F0"));
            game.levelNumber++;
            PlayerPrefs.SetInt("currentLevelNumber", game.levelNumber);
            PlayerPrefs.Save();
            CustomDebug("Analytics Event : Level Win");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ElephantCustomTelemetry(string gameStatus, string eventName, float value)
    {
        Params p = Params.New().Set(eventName, value);
        Elephant.Event(gameStatus, 1, p);
    }
    public void GACustomTelemetry(string gameStatus, string header, string eventName, float value)
    {
        string eventID = gameStatus+":"+header+":"+eventName;
        GameAnalytics.NewDesignEvent(eventID, value);
    }
    public void CustomDebug(string x)
    {
        if(!debugEnabled) return;
        game.debugControl.logsText.text += "\n";
        game.debugControl.logsText.text += x;
        game.debugControl.logsText.rectTransform.sizeDelta += new Vector2(0,75);
    }
#endregion

#region Button Functions
    public void SelectLevel(int x)
    {
        if(x > game.levels.Length && game.levels.Length > 0) 
        {
            y = x % game.levels.Length;
            if(y == 0) y = x; 
        }
        PlayerPrefs.SetInt("currentLevelNumber", y);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleGraphicsQuality()
    {
        string highStr = LocalizationManager.GetTranslation("HIGH");
        string lowStr = LocalizationManager.GetTranslation("LOW");
        highQuality =! highQuality;
        ppVolume.enabled = highQuality;
        ppLayer.enabled = highQuality;
        uIControl.qualityText.text = highQuality ? highStr : lowStr;
        CustomDebug("System Event : Quality changed = "+ highQuality);
    }
    void PopulateLevels()
    {
        uIControl.buttonPrefab.gameObject.SetActive(false);
        var text = uIControl.buttonPrefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        var unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        for (var i = 1; i <= totalLevelCount; i++) {
            text.text = i.ToString();
            var buttonInstance = Instantiate(uIControl.buttonPrefab, uIControl.buttonPrefab.transform.parent, true);
            buttonInstance.name = i.ToString();
            buttonInstance.interactable = i <= unlockedLevel;
            buttonInstance.gameObject.SetActive(true);
        }
        CustomDebug("System Event : Level Selector Populated");
    }
#endregion Button Functions

#region Input Types
    void JoystickCheck()
    {
        game.joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        game.joystick.gameObject.SetActive(false);
        if(!joystick) return;
        game.joystick.gameObject.SetActive(true);
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        joystickControl = playerObj.GetComponent<JoystickControl>();
        if(playerObj == null || joystickControl == null) return;
        joystickControl.InitializeReferences();
        CustomDebug("System Event : Joystick Control Enabled");
    }
    void DragDropCheck()
    {
        if(!dragDrop) return;
        dragDropControl = GetComponent<DragDropControl>();
        dragDropControl.enabled = true;
        dragDropControl.InitializeReferences();
        CustomDebug("System Event : Drag & Drop Control Enabled");
    }
    void SwerveCheck()
    {
        if(!swerve) return;
        swerveControl = GetComponent<SwerveControl>();
        swerveControl.enabled = true;
        swerveControl.InitializeReferences();
        CustomDebug("System Event : Swerve Control Enabled");
    }
#endregion Input Types

#region Haptics
    public void HapticsPlayOnce(float x, float y)
    {
        if(haptics) HapticPatterns.PlayEmphasis(x, y);
    }
    public void HapticsPlayContinuous(float x, float y, float time)
    {
        if(haptics) HapticPatterns.PlayConstant(x, y, time);
    }
    public void HapticsPlayClip(HapticClip x)
    {
        HapticController.Play(x);
    }
    public void HapticVolumeLevel()
    {
        HapticController.outputLevel = uIControl.hapticSlider.value;
        PlayerPrefs.SetFloat("haptics", uIControl.hapticSlider.value);
        CustomDebug("System Event : Haptics Volume Changed");
    }   
#endregion
#region App Rater
    public void RatingSystem()
    {
        if(game.levelNumber >= game.tutorialLevelsCount && PlayerPrefs.GetInt("isRatingShown", 0) < 1 && !isRatingShown)
        {
            isRatingShown = true;
            PlayerPrefs.SetInt("isRatingShown", 1);
            PlayerPrefs.Save();
            AppraterScript.ShowRaterPopup();
            CustomDebug("System Event : App Rating Shown");
        }
    }
#endregion
#region Camera Shaker
    IEnumerator CamShakeDelayer(float delay, float maxRange)
    {
        yield return new WaitForSeconds(delay);
        shakeReciever.InduceStress(maxRange); 
    }
    public void ShakeCamera(float delay, float maxRange)
    {
        if(shakeReciever == null) return;
        StartCoroutine(CamShakeDelayer(delay, maxRange));
        CustomDebug("Game Event : Camera Shake, force : " + maxRange);
    }
#endregion
}


