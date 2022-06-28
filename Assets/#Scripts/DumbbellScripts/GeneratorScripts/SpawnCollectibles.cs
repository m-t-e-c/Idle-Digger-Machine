using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    [Header("===Pickup Types===")]
    public GameObject[] benefits;
    public GameObject[] detriments;
    [Header("===Pickup Areas===")]
    public GameObject[] pickupAreas;
    List<GameObject> spawnedPickupAreas = new List<GameObject>();
    public float startPos, endPos;
    [Range(0,100)] public int spawnInterval;
    [Range(0,100)] public int distanceFromPlayer;

    float pickupAreasSpawnCount, distanceToStart, zLength;
    int distanceFromStart;
    
    int SpawnWeights(float spawnPosition, float zLength)
    {
        int minT = (int)(100 - (spawnPosition/10));
        int randomChance = Random.Range(0,minT);

        int objectIndex;

        if     (randomChance < 50  && spawnPosition > zLength*0.1f && spawnPosition <= zLength*0.2f) { objectIndex = Random.Range(7,10); return objectIndex; }
        else if(randomChance < 50  && spawnPosition > zLength*0.2f && spawnPosition <= zLength*0.3f) { objectIndex = Random.Range(6,10); return objectIndex; }
        else if(randomChance < 50  && spawnPosition > zLength*0.3f && spawnPosition <= zLength*0.4f) { objectIndex = Random.Range(5,10); return objectIndex; }
        else if(randomChance < 50  && spawnPosition > zLength*0.4f && spawnPosition <= zLength*0.5f) { objectIndex = Random.Range(4,10); return objectIndex; }
        else if(randomChance < 50  && spawnPosition > zLength*0.5f && spawnPosition <= zLength*0.6f) { objectIndex = Random.Range(3,8); return objectIndex; }
        else if(randomChance >= 50 && spawnPosition > zLength*0.6f && spawnPosition <= zLength*0.7f) { objectIndex = Random.Range(2,7); return objectIndex; }
        else if(randomChance >= 50 && spawnPosition > zLength*0.7f && spawnPosition <= zLength*0.8f) { objectIndex = Random.Range(1,6); return objectIndex; }
        else if(randomChance >= 50 && spawnPosition > zLength*0.8f && spawnPosition <= zLength*0.9f) { objectIndex = Random.Range(0,5); return objectIndex; }
        else if(randomChance >= 50 && spawnPosition > zLength*0.9f && spawnPosition <= zLength)      { objectIndex = Random.Range(0,5); return objectIndex; }
        else if(randomChance >= 50 && spawnPosition > startPos)                                      { objectIndex = Random.Range(0,10); return objectIndex; }
        else                                                                                         { objectIndex = Random.Range(0,6); return objectIndex; }
    }

    void SetVariables()
    {
        distanceFromStart = (int)startPos + distanceFromPlayer;
        zLength = endPos - startPos;
        distanceToStart = zLength;
        pickupAreasSpawnCount = zLength / spawnInterval;
        if(pickupAreas == null)
        {
            Debug.Log("Please add some objects to Pickup Areas array");
            return;
        }
    }

    public void SpawnWeighted()
    {
        SetVariables();
        ClearSpawned();
        if(pickupAreas.Length < 11)
        {
            int x = 11 - pickupAreas.Length;
            Debug.Log("Please add " + x +" more objects to Pickup Areas array");
            return;
        }
        for(int i = 0; i < pickupAreasSpawnCount; i++)
        {
            int x = SpawnWeights(distanceToStart, zLength);
            GameObject go = Instantiate(pickupAreas[x]);
            go.transform.position = new Vector3(0, 0, distanceFromStart);
            go.transform.parent = transform;
            spawnedPickupAreas.Add(go);

            distanceFromStart += spawnInterval; 
            distanceToStart -= spawnInterval;
            if(distanceFromStart > zLength) return;
            if(distanceToStart < 0) distanceToStart = zLength;
        } 
        Debug.Log("Pickup Areas Created : Randomized");
    }

    public void SpawnLinear()
    {
        SetVariables();
        ClearSpawned();
        for(int i = 0; i < pickupAreasSpawnCount; i++)
        {
            GameObject go = Instantiate(pickupAreas[i]);
            go.transform.position = new Vector3(0, 0, distanceFromStart);
            go.transform.parent = transform;
            spawnedPickupAreas.Add(go);

            distanceFromStart += spawnInterval; 
            distanceToStart -= spawnInterval;
            if(distanceFromStart > zLength) return;
            if(distanceToStart < 0) distanceToStart = zLength;
        }
        Debug.Log("Pickup Areas Created : Ordered");
    }

    public void InstantiatePickups()
    {
        if(benefits == null || detriments == null)
        {
            Debug.Log("Please add some objects to Pickup Types array");
            return;
        }
        foreach(GameObject x in spawnedPickupAreas)
        {
            int randomBenefit = Random.Range(0, benefits.Length);
            int randomDetriment = Random.Range(0, detriments.Length);
            LayoutOrganizer lo = x.GetComponent<LayoutOrganizer>();
            if(lo == null) { Debug.Log("Couldn't find LayoutOrganizer in pickup areas. Aborting!"); return; }
            lo.Populate3DArray();
            foreach(Transform y in lo.pickups3D)
            {
                if(y.tag == "Benefit") SpawnObject(benefits[randomBenefit], y, "Beneficial Object Created");
                if(y.tag == "Detriment") SpawnObject(detriments[randomDetriment], y, "Detrimental Object Created");
            }
        }
    }

    void SpawnObject(GameObject x, Transform y, string z)
    {
        GameObject go = Instantiate(x) as GameObject;
        go.transform.position = y.position;
        go.name = x.name + "_Pickup";
        go.transform.SetParent(y);
        Debug.Log(z);
    }

    void ClearSpawned()
    {
        if(spawnedPickupAreas != null)
        {   
            foreach(GameObject x in spawnedPickupAreas) DestroyImmediate(x);
        }
        spawnedPickupAreas.Clear();
    }
}
