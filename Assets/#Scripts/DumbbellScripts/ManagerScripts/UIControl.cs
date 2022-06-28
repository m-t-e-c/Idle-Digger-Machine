using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using I2.Loc;

public class UIControl : MonoBehaviour
{
    [Header("===UI Panels===")]
    public GameObject tutorial;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject inGamePanel;
    public GameObject debugPanel;

    [Header("===Level Texts===")]
    public TextMeshProUGUI[] levelVariables;

    [Header("===UI Elements===")]
    public TextMeshProUGUI qualityText;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider hapticSlider;
    public Button buttonPrefab;
    
    // Script References
    [HideInInspector] public SettingsControl settings;
    [HideInInspector] public GameControl game;
    [HideInInspector] public GameObject winParticles;

    string localizedLevel;

    [Space(10)]
    public IntVariable PlayerFuelResource;
    public IntVariable PlayerMetalResource;
    public IntVariable PlayerGold;
    public IntVariable PlayerHealth;
    public IntVariable PlayerDamage;

    public Image playerHealthFill;
    public Animator playerHealthAnim;

    public TextMeshProUGUI playerDamageText;
    public TextMeshProUGUI fuelResourceText;
    public TextMeshProUGUI metalResourceText;
    public TextMeshProUGUI playerGoldText;

    private void Update()
    {
        if (PlayerHealth.Value < 10)
        {
            playerHealthAnim.SetBool("LowHealth", true);
        }
        else playerHealthAnim.SetBool("LowHealth", false);
        
        float playerHealth = PlayerHealth.Value;
        playerHealthFill.fillAmount = playerHealth.Remap01(0,100);
        playerDamageText.SetText(PlayerDamage.Value + "/s");
        fuelResourceText.SetText(PlayerFuelResource.Value.ToString());
        metalResourceText.SetText(PlayerMetalResource.Value.ToString());
        playerGoldText.SetText(PlayerGold.Value.ToString());
    }

    private void Player_PickUpHealth()
    {
        playerHealthAnim.SetTrigger("Heal");
    }

    private void OnEnable()
    {
        Player.OnPickUpHealth += Player_PickUpHealth;
    }

    private void OnDisable()
    {
        Player.OnPickUpHealth -= Player_PickUpHealth;
    }
    #region Scene Initializers
    public void InitializeTexts()
    { 
        localizedLevel = LocalizationManager.GetTranslation("Level");  
        winParticles = Camera.main.transform.GetChild(0).gameObject;
        if(winParticles != null) winParticles.SetActive(false);
        foreach(TextMeshProUGUI x in levelVariables)
        {
            x.text = localizedLevel + " " + game.levelNumber.ToString();
        }
        settings.CustomDebug("System Event : UI Texts Initialized");
    }
#endregion Scene Initializers
#region Game State
    public void Win()
    {
        game.isGameFinished = true;
        winPanel.SetActive(true);
        if(winParticles != null) winParticles.SetActive(true);
        inGamePanel.SetActive(false);
        game.audioControl.PlaySFX(game.audioControl.winSFX);
        settings.HapticsPlayClip(settings.winHaptic);
        settings.CustomDebug("Game Event : Level Win, Time = " + game.levelTime);
    }
    public void Lose()
    {
        game.isGameFinished = true;
        losePanel.SetActive(true);
        inGamePanel.SetActive(false);
        game.audioControl.PlaySFX(game.audioControl.loseSFX);
        settings.HapticsPlayClip(settings.loseHaptic);
        settings.CustomDebug("Game Event : Level Lose, Time = " + game.levelTime);
    }
#endregion Game State
#region Button
    public void Restart()
    {
        settings.AnalyticsTracker("lose");
    }
    public void NextLevel()
    {
        settings.AnalyticsTracker("win");
    }
    public void LevelSelect()
    {
        int x;
        int.TryParse(EventSystem.current.currentSelectedGameObject.name, out x);
        PlayerPrefs.SetInt("currentLevelNumber", x);
        settings.SelectLevel(x);
        settings.CustomDebug("Game Event : Level Selected = Level "+ x);
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
    }
    public void TogglePausePanel(GameObject y)
    {
        bool x = inGamePanel.activeInHierarchy;
        x =! x;
        inGamePanel.SetActive(x);
        y.SetActive(!x);
        if(!x) { Time.timeScale = 0; settings.CustomDebug("Game Event : Game Paused"); }
        else { Time.timeScale = settings.startTimeScale; settings.CustomDebug("Game Event : Game Resumed"); }
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        hapticSlider.value = PlayerPrefs.GetFloat("haptics", 0.5f);
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("http://privacy.dumbbellgames.com");
        settings.CustomDebug("UI Event : Privacy Policy Opened");
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
    }
    public void ToggleQuality()
    {
        settings.ToggleGraphicsQuality();
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
        settings.CustomDebug("UI Event : Quality = " + qualityText);
    }
    public void ReturnToGame()
    {
        game.debugControl.debuggerActive = false;
        Time.timeScale = game.debugControl.timeScaleSlider.value;
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
        settings.CustomDebug("UI Event : Debugger Closed");
        if(game.debugControl.uiEnabled) inGamePanel.SetActive(true);
        debugPanel.SetActive(false);
    }
    public void OpenDebugger()
    {
        game.debugControl.debuggerActive = true;
        Time.timeScale = 0;
        game.audioControl.PlaySFX(game.audioControl.buttonSFX);
        settings.HapticsPlayClip(settings.subtle);
        settings.CustomDebug("UI Event : Debugger Opened");
        pausePanel.SetActive(false);
        debugPanel.SetActive(true);
    }
#endregion Button
}
