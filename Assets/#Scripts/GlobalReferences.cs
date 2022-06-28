using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform debris;

    public void ParentSet(Transform x)
    {
        x.parent = debris;
    }
}
