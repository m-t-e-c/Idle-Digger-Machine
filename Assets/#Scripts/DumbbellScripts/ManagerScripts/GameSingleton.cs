using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    // Control Scripts
    [HideInInspector] public GameControl gameControl;
    [HideInInspector] public AudioControl audioControl;
    [HideInInspector] public UIControl uiControl;
    [HideInInspector] public CameraFollower cameraFollower;
    [HideInInspector] public SettingsControl settingsControl;
    [HideInInspector] public DebugControl debugControl;

    static GameSingleton _instance;
    public static GameSingleton Instance { get { return _instance; } }

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            //settingsControl.CustomDebug("Singleton Event : Destroyed");
            return;
        }
        _instance = this;
        DumpReferences();
        LoadReferences();
        DontDestroyOnLoad(this.gameObject);
        //settingsControl.CustomDebug("Singleton Event : Loaded");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void DumpReferences()
    {
        gameControl = null;
        audioControl = null;
        uiControl = null;
        cameraFollower = null;
        settingsControl = null;
        debugControl = null;
    }

    void LoadReferences()
    {
        gameControl = FindObjectOfType<GameControl>(true);
        audioControl = FindObjectOfType<AudioControl>(true);
        settingsControl = FindObjectOfType<SettingsControl>(true);
        uiControl = FindObjectOfType<UIControl>(true);
        cameraFollower = FindObjectOfType<CameraFollower>(true);
        debugControl = FindObjectOfType<DebugControl>(true);
    }
}
