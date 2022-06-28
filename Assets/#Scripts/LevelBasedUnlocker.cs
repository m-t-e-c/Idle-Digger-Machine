using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBasedUnlocker : MonoBehaviour
{
    private CameraFollower cameraFollower;
    public IntVariable playerTotalGold;
    public int unlockAmount;
    

    private void Start()
    {
        cameraFollower = GameObject.FindWithTag("CameraContainer").GetComponent<CameraFollower>();
    }

    public void Update()
    {
        if(playerTotalGold.Value >= unlockAmount)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
