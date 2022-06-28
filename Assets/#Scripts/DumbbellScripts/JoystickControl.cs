using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    //Script References
    GameObject settingObj;
    GameControl game;
    SettingsControl settings;
    Joystick joystick;
    Animator anim;

    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody rb;
    public float moveSpeed = 20;
    public float rotationSpeed = 5;
    public bool isMoving;

    public void InitializeReferences()
    {
        settingObj = GameObject.FindGameObjectWithTag("Settings");
        game = settingObj.GetComponent<GameControl>();
        settings = settingObj.GetComponent<SettingsControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        joystick = game.joystick;
    }

    private void Update()
    {
        if (isMoving) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
    }

    public void JoystickInput()
    {
        if (Input.GetMouseButton(0))
        {
            rb.constraints =
               RigidbodyConstraints.FreezePositionY |
               RigidbodyConstraints.FreezeRotationX |
               RigidbodyConstraints.FreezeRotationZ;
            float x = joystick.Horizontal;
            float y = joystick.Vertical;
            float heading = Mathf.Atan2(x * 100f, y * 100f);

            if (y >= 0.1f || y <= -0.1f || x >= 0.1f || x <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0f, (heading * Mathf.Rad2Deg), 0f);
                isMoving = true;
            }
            rb.velocity = new Vector3(x * moveSpeed * Time.deltaTime, 0, y * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
