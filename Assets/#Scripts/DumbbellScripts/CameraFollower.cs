using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Camera cam;
    public Transform target;
    public Transform player;
    [Range(0,1)] public float lerpSpeed = 0.125f;
    public Vector3 offset = new Vector3(0,-10,15);

    [Range(1,100)] public float cameraXMoveModifier = 1;
    [Range(0,90)] public float yRotationClamp;
    public bool cameraCanLookAt;
    Vector3 currPos, targetPos, lerpPos;
    float rotY, angle;
    float startFOV;

    public bool unlockCamera;

    void Start() 
    {   
        cam = Camera.main.GetComponent<Camera>();
        startFOV = cam.fieldOfView;
    }
    
    void LateUpdate() => CameraHandler();

    void CameraHandler()
    {
        if(target == null) return;
        currPos = transform.position;
        targetPos = target.position-offset;
        targetPos.x = targetPos.x/cameraXMoveModifier;

        lerpPos = Vector3.Lerp(currPos, targetPos, lerpSpeed);
        transform.position = lerpPos;
        transform.LookAt(target.position);
        
        if(!cameraCanLookAt) return;
        angle = transform.localEulerAngles.y;
        angle = (angle > 180) ? angle - 360 : angle;

        rotY = Mathf.Clamp (angle, -yRotationClamp, yRotationClamp);
        transform.rotation = Quaternion.Euler (transform.eulerAngles.x, rotY, transform.eulerAngles.z);
    }

    public void UnlockTransition(Transform target)
    {
        StartCoroutine(TransitionAnimation(target));
    }

    private IEnumerator TransitionAnimation(Transform target)
    {
        unlockCamera = true;
        float time = 0;
        while(time < 5f)
        {
            time += Time.deltaTime;
            Vector3 xPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.Slerp(transform.position, xPos, (5 / time));
        }

        yield return new WaitForSeconds(2f);
        unlockCamera = false;
        target = player;
    }
}
