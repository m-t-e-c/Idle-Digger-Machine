using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [Header("===Levels===")]
    public Level[] levels;
    public int tutorialLevelsCount = 3;
    [HideInInspector] public int levelIndex;
    [HideInInspector] public int levelNumber;
    [HideInInspector] public GameObject currentLevel;
    [HideInInspector] public float levelTime = 0;
    
    //Pre-made references
    [HideInInspector] public GameObject mainCam;
    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public bool isGameFinished = false;
    [HideInInspector] public Joystick joystick;
    [HideInInspector] public UIControl uIControl;
    [HideInInspector] public SettingsControl settings;
    [HideInInspector] public AudioControl audioControl;
    [HideInInspector] public DebugControl debugControl;

#region Unity Main
    void Awake()
    {
        InitializeScripts();
        InitializeLevel();
        uIControl.InitializeTexts();
        settings.InitializeSettings();
        audioControl.InitializeAudio();
        debugControl.InitializeDebugger();
        InitializeAudioSliders();
    }
    void Update()
    {
        CheckGameStart();
        if(!isGameStarted) return;
        CheckInputType();
        if(!isGameFinished) levelTime += Time.deltaTime;
    }
#endregion Unity Main
#region Initializers
    void InitializeLevel()
    {
        levelNumber = PlayerPrefs.GetInt("currentLevelNumber", 1);
        if(levels.Length == 0) return; 
        levelIndex = (levelNumber % levels.Length);
        levels[levelIndex].Load(); 
        currentLevel = levels[levelIndex].currentLevel; 
        settings.CustomDebug("System Event : Level Created");
    }
    void InitializeScripts()
    {
        uIControl = GetComponent<UIControl>();
        settings = GetComponent<SettingsControl>();
        audioControl = FindObjectOfType<AudioControl>();
        debugControl = FindObjectOfType<DebugControl>(true);
        settings.uIControl = uIControl;
        settings.game = this;
        uIControl.settings = settings;
        uIControl.game = this;
        audioControl.uIControl = uIControl;
        settings.CustomDebug("System Event : References Passed");
    }
    void InitializeAudioSliders()
    {
        uIControl.sfxSlider.onValueChanged.AddListener(delegate{audioControl.SetVolume();});
        uIControl.bgmSlider.onValueChanged.AddListener(delegate{audioControl.SetVolume();});
        uIControl.hapticSlider.onValueChanged.AddListener(delegate{settings.HapticVolumeLevel();});
    }
#endregion Initializers
#region Game State
    void CheckGameStart()
    {
        if(Input.anyKey && !isGameStarted)
        {
            uIControl.tutorial.SetActive(false);
            isGameStarted = true;
            settings.CustomDebug("Game Event : Game Started");
        } 
    }
    void CheckInputType()
    {
        if(settings.joystick) settings.joystickControl.JoystickInput();
        if(settings.dragDrop) settings.dragDropControl.DragAndDrop();
        if(settings.swerve) settings.swerveControl.Swerve();
    }
    public bool CheckTutorial()
    {
        if(levelNumber < tutorialLevelsCount) return true;
        else return false;
    }
#endregion Game State
}

