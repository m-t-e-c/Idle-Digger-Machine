using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Tayx.Graphy;
using Dumbbell;

public class DebugControl : MonoBehaviour
{
    [Header("===Debug Control Panels===")]
    public GameObject inGameUIPanel;
    public GameObject statsPanel;
    public GameObject cameraPanel;
    public GameObject logPanel;
    public GameObject generalPanel;

    [Header("===Debug Control Header===")]
    public TextMeshProUGUI versionText;
    public TextMeshProUGUI platformText;
    public TextMeshProUGUI gameNameText;

    [Header("===General Controls Panel===")]
    public TextMeshProUGUI totalGameTimeText;
    public TextMeshProUGUI loadedLevelText;
    public TextMeshProUGUI timeScaleValue;
    public Slider timeScaleSlider;
    public TextMeshProUGUI isRatingShownText;
    public TextMeshProUGUI debugEnabledText;
    public TextMeshProUGUI uiEnabledText;
    public TextMeshProUGUI fpsEnabledText;
    public TextMeshProUGUI totalLevelText;
    public TextMeshProUGUI systemLanguageText;

    [Header("===Camera Controls Panel===")]
    public TextMeshProUGUI camXcurrValue;
    public TextMeshProUGUI camYcurrValue;
    public TextMeshProUGUI camZcurrValue;
    public TextMeshProUGUI camFOVcurrValue;
    public Slider camXSlider;
    public Slider camYSlider;
    public Slider camZSlider;
    public Slider camFOVSlider;
    
    [Header("===Remote Controls Panel===")]
    public TextMeshProUGUI remoteHeader;
    
    [Header("===Logs Panel===")]
    public TextMeshProUGUI logsText;

    SettingsControl sc;
    GameControl gc;
    GraphyManager graphy;
    [HideInInspector] public bool uiEnabled = true;
    bool isFPSMonitorActive = false;
    bool debugEnabled = false;
    [HideInInspector] public bool debuggerActive = false;
    
    void Update() => DebuggerUpdates();

#region Start Functions
    public void InitializeDebugger()
    {
        graphy = FindObjectOfType<GraphyManager>(true);
        gc = FindObjectOfType<GameControl>();
        sc = FindObjectOfType<SettingsControl>();
        SetDebuggerTexts();
        sc.CustomDebug("System Event : Debugger Set");
    }
    void SetDebuggerTexts()
    {
        SetGeneralPanelTexts();
        GetCameraValues();   
    }
    void SetGeneralPanelTexts()
    {
        versionText.text = "v" + Application.version;
        platformText.text = Application.platform.ToString();
        gameNameText.text = Application.productName;
        systemLanguageText.text = Application.systemLanguage.ToString();
        loadedLevelText.text = gc.levels[gc.levelIndex].name;
        timeScaleSlider.value = Time.timeScale;
        isRatingShownText.text = sc.isRatingShown.ToString();
        debugEnabledText.text = sc.debugEnabled.ToString();
        totalLevelText.text = gc.levels.Length.ToString();
    }
    void GetCameraValues()
    {
        camXSlider.value = GameSingleton.Instance.cameraFollower.offset.x;
        camYSlider.value = GameSingleton.Instance.cameraFollower.offset.y;
        camZSlider.value = GameSingleton.Instance.cameraFollower.offset.z;
        camFOVSlider.value =  Camera.main.fieldOfView;
    }
#endregion
#region Update Functions
    void SetGameTime()
    { 
        PlayerPrefs.SetFloat("gameTime", PlayerPrefs.GetFloat("gameTime") + Time.deltaTime);  
        totalGameTimeText.text = Utilities.TimeFormatter(Mathf.FloorToInt(PlayerPrefs.GetFloat("gameTime", 0)));
    }
    void DebuggerUpdates()
    {
        SetGameTime();
        if(!debuggerActive) return;
        GameSingleton.Instance.cameraFollower.offset.x = camXSlider.value;  
        GameSingleton.Instance.cameraFollower.offset.y = camYSlider.value;  
        GameSingleton.Instance.cameraFollower.offset.z = camZSlider.value;  
        Camera.main.fieldOfView = (int)camFOVSlider.value;
        Time.timeScale = timeScaleSlider.value;

        timeScaleValue.text = timeScaleSlider.value.ToString("F1");
        camXcurrValue.text = GameSingleton.Instance.cameraFollower.offset.x.ToString("F0");
        camYcurrValue.text = GameSingleton.Instance.cameraFollower.offset.y.ToString("F0");
        camZcurrValue.text = GameSingleton.Instance.cameraFollower.offset.z.ToString("F0");
        camFOVcurrValue.text = Camera.main.fieldOfView.ToString("F0");
    }
#endregion
#region Buttons
    public void ToggleFPS()
    {
        isFPSMonitorActive =! isFPSMonitorActive; 
        graphy.gameObject.SetActive(isFPSMonitorActive); 
        fpsEnabledText.text = isFPSMonitorActive ? "ON" : "OFF"; 
        sc.CustomDebug("Debug Event : FPS Toggle = " + isFPSMonitorActive);
    }
    public void ToggleDebug()
    {
        debugEnabled =! debugEnabled; 
        GameSingleton.Instance.settingsControl.debugEnabled = debugEnabled; 
        debugEnabledText.text = debugEnabled ? "ON" : "OFF";
        sc.CustomDebug("Debug Event : Debug Toggle = " + debugEnabled);
    }
    public void ToggleUI()
    {
        uiEnabled =! uiEnabled;
        inGameUIPanel.SetActive(uiEnabled);
        uiEnabledText.text = uiEnabled ? "ON" : "OFF";
        sc.CustomDebug("Debug Event : UI Toggle = " + uiEnabled);
    }
    public void ClearPrefs() 
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        sc.CustomDebug("Debug Event : Reset Game");
    }
    public void ClearLogs()
    {
        logsText.text = "";
        sc.CustomDebug("Debug Event : Cleared Logs");
    }
#endregion Buttons
}
