using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePileSpawner : MonoBehaviour
{
    public float minSpawnTime;
    public float maxSpawnTime;
    private float spawnTime;
    public float spawnRadius;
    public List<GameObject> resourcePiles = new List<GameObject>();
    public bool startSpawnTimer;

    private float waitTime;

    public bool wait;

    private void Start()
    {
        startSpawnTimer = true;
    }

    public void Update()
    {
        if (wait) return;
        if (startSpawnTimer)
        {
            waitTime += Time.deltaTime;
            if(waitTime >= spawnTime)
            {
                waitTime = 0;
                startSpawnTimer = false;
                int randomIndex = Random.Range(0, resourcePiles.Count);
                float randomXPos = transform.position.x + Random.insideUnitCircle.x * spawnRadius;
                float randomZPos = transform.position.z + Random.insideUnitCircle.y * spawnRadius;
                Vector3 spawnPos = new Vector3(randomXPos,0.2f,randomZPos);
                GameObject x = Instantiate(resourcePiles[randomIndex], spawnPos, Quaternion.identity);
                x.ParentSet(transform);
                x.GetComponent<ResourcePile>().SetSpawner(this);
            }
        }
    }

    public void StartTimer()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        startSpawnTimer = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wait = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wait = false;
        }
    }
}
