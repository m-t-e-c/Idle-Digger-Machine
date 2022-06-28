using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour
{
    public GameObject resourcePileHitParticle;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ResourcePile"))
        {
            if (!resourcePileHitParticle) return;
            Instantiate(resourcePileHitParticle, other.GetContact(0).point, Quaternion.identity).ParentSet(GlobalReferences.instance.debris);
        }
    }
}
