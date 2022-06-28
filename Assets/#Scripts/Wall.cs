using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    public IntVariable wallDefaultDurability;
    public IntVariable skeletonEnemyDamage;
    public int currentDurability;
    public Collider wallCol;

    public bool damageDelay;

    private void Start()
    {
        if (!wallCol)
        {
            wallCol = GetComponent<Collider>();
        }
        currentDurability = wallDefaultDurability.Value;
    }
    
    public void DeactivateWall()
    {
        wallCol.enabled = false;
        transform.DOScale(new Vector3(0.9f,0.05f,0.9f),0.1f).SetEase(Ease.OutQuad);
    }

    public void Build()
    {
        wallCol.enabled = true;
        transform.DOScale(new Vector3(0.9f, 0.5f, 0.9f), 0.1f).SetEase(Ease.OutQuad);
    }

    private void GetDamage(int amount)
    {
        currentDurability -= amount;
        transform.DOScale(new Vector3(0.9f, 0.3f, 0.9f), 0.1f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
        if (currentDurability <= 0)
        {
            DeactivateWall();
        }
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

    private IEnumerator DelayDamage()
    {
        yield return null;
        damageDelay = false;
    }
}
