using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class ResourcePile : MonoBehaviour, IDamageable
{
    public List<GameObject> resourceList;
    public TextMeshProUGUI durabilityText;
    public GameObject destroyParticle;
    public GameObject selectionImage;
    public int Durability;
    public int DropAmount;
    public bool delayDamage;

    private ResourcePileSpawner spawner;

    public IntVariable playerDamage;

    private void Start()
    {
        selectionImage.SetActive(false);
        durabilityText.SetText(Durability.ToString());
    }

    public void SetSpawner(ResourcePileSpawner x)
    {
        spawner = x;
    }

    public void GetDamage(int amount)
    {
        Durability -= amount;
        Camera.main.GetComponent<CameraShakeReciever>().InduceStress(0.1f);
        transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
        durabilityText.SetText(Durability.ToString());
        if (Durability <= 0)
        {
            GetDestroyed();
            return;
        }
    }

    private void SpawnResources()
    {
        for (int i = 0; i < DropAmount; i++)
        {
            float rndX = Random.Range(0f, 1.5f);
            float rndZ = Random.Range(0f, 1.5f);
            Vector3 spawnPos = new Vector3(transform.position.x + rndX, 0.25f, transform.position.z + rndZ);
            GameObject x = Instantiate(resourceList[Random.Range(0, resourceList.Count)], spawnPos, Quaternion.identity);
            x.ParentSet(GlobalReferences.instance.debris);
        }
    }

    public void GetDestroyed()
    {
        if (destroyParticle)
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity).ParentSet(GlobalReferences.instance.debris);
        }
        spawner.StartTimer();
        SpawnResources();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crowbar"))
        {
            if (delayDamage) return;
            delayDamage = true;
            StartCoroutine(DelayDamage());
            GetDamage(playerDamage.Value);
        }
    }

    private IEnumerator DelayDamage()
    {
        yield return null;
        delayDamage = false;
    }
}
