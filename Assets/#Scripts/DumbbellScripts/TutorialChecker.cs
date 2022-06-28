using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecker : MonoBehaviour
{
    /* Add this script to any object that is part of the in-game tutorial*/
    GameControl gameControl;
    bool checkTutorial;
    void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("Settings").GetComponent<GameControl>();
        checkTutorial = gameControl.CheckTutorial();
        if(checkTutorial) return;
        else gameObject.SetActive(false);
        gameControl.settings.CustomDebug("Game Event : Is Tutorial Level = " +checkTutorial);
    }
}
