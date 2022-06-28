using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval;
    public int amount;

    public bool spawnStarted;

    private void OnEngineCooldown()
    {
        if (spawnStarted) return;
        spawnStarted = true;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity).ParentSet(GlobalReferences.instance.debris);
            yield return new WaitForSeconds(spawnInterval);
        }

        spawnStarted = false;
    }

    private void OnEnable()
    {
        Drill.OnEngineCooldown += OnEngineCooldown;
    }

    private void OnDisable()
    {
        Drill.OnEngineCooldown -= OnEngineCooldown;
    }
}
