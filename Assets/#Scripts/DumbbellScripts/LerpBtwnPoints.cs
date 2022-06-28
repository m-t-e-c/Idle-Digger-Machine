using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBtwnPoints : MonoBehaviour
{
    // Lerps items between points, creates a treadmill effect
    public List<Transform> items = new List<Transform>();
    public Transform[] points;
    public float movementSpeed;

    void Update()
    {
        float speed =  movementSpeed * Time.deltaTime;
        foreach(Transform tr in items)
        {
            if (Vector3.Distance(tr.position, points[0].position) > 0.1f) tr.position = Vector3.MoveTowards(tr.position, points[0].position, speed); 
            else tr.position = points[1].position;
        }
    }
}
