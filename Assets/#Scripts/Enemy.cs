using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Player player;

    public Collider damageCollider;
    public LayerMask attackMask;

    public bool inAttackRange = false;
    public bool damageDelay;

    [Header("Enemy Properties")]
    public IntVariable speed;
    public IntVariable damage;
    public IntVariable health;

    [Header("Player Properties")]
    public IntVariable playerDamage;

    public int currentDamage;
    public int currentHealth;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        agent.speed = speed.Value;
        currentDamage = damage.Value;
        currentHealth = health.Value;
    }

    private void Update()
    {
        if (!player)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
            return;
        }

        if (inAttackRange)
        {
            agent.isStopped = true;
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
            agent.isStopped = false;
        }
    }


    public void GetDamage(int amount)
    {
        currentHealth -= amount;
        transform.position = transform.position - transform.forward * 0.5f;
        Camera.main.GetComponent<CameraShakeReciever>().InduceStress(0.1f);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crowbar"))
        {
            if (damageDelay) return;
            damageDelay = true;
            StartCoroutine(DelayDamage());
            GetDamage(playerDamage.Value);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            inAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            inAttackRange = false;
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
        damageCollider.enabled = true;
    }

    public void DeactivateHitBox()
    {
        damageCollider.enabled = false;
        inAttackRange = false;
    }
}

