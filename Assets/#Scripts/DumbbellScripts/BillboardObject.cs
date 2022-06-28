using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    /* This script billboards the object to the referenced transform(Camera,Player,Object)*/
    public bool isLookingAtCamera;
    public bool isLookingAtPlayer;
    public Transform anotherObject;
    Transform target;
    
    void Start()
    {
        if(isLookingAtCamera) target = Camera.main.transform;
        if(isLookingAtPlayer) target = GameObject.FindGameObjectWithTag("Player").transform;
        if(anotherObject != null) target = anotherObject;
    }
    
    void Update()
    {
        transform.LookAt(target);
    }
}
