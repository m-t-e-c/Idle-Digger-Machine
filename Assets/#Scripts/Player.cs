using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Action OnPickUpHealth;

    private Animator anim;
    public GameObject resourcePileHitParticle;
    public Transform crowbarParticlePoint;
    public Collider crowbarHurtBox;

    public LayerMask attackMask;
    public float gatherRayLength;

    public bool attacking;
    public bool damageDelay;

    public IntVariable Health;
    public IntVariable Damage;
    public IntVariable FuelResource;
    public IntVariable MetalResource;

    public IntVariable skeletonEnemyDamage;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void GetDamage(int amount)
    {
        Health.Value -= amount;
        if (Health.Value <= 0)
            GetDestroyed();
    }

    public void GetDestroyed()
    {
        transform.position = Vector3.zero;
        Health.Value = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            if (damageDelay) return;
            damageDelay = true;
            StartCoroutine(DelayDamage());
            GetDamage(skeletonEnemyDamage.Value);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ResourcePile"))
        {
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ResourcePile"))
        {
            anim.SetBool("Attack", false);
        }
    }

    private IEnumerator DelayDamage()
    {
        yield return null;
        damageDelay = false;
    }

    // Animator Methods
    public void ActivateHitBox()
    {
        if (resourcePileHitParticle)
        {
            Instantiate(resourcePileHitParticle, crowbarParticlePoint.position, Quaternion.identity).ParentSet(GlobalReferences.instance.debris);
        }
        crowbarHurtBox.enabled = true;
    }

    public void DeactivateHitBox()
    {
        crowbarHurtBox.enabled = false;
        anim.SetBool("Attack", false);
    }
}
