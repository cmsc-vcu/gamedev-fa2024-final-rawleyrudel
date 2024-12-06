using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab; // Fish prefab to spawn
    public float spawnIntervalMin = 2f; // Minimum spawn interval
    public float spawnIntervalMax = 5f; // Maximum spawn interval
    public Transform[] spawnPoints; // Array of spawn points in the scene

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (isSpawning)
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
