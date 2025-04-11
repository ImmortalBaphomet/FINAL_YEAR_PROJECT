using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerSpawner : MonoBehaviour
{
    [Header("Platform Prefabs")]
    public List<GameObject> platformPrefabs;

    [Header("Spawn Points")]
    public List<Transform> spawnPoints;

    [Header("Settings")]
    public bool spawnOnlyOnce = true;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!spawnOnlyOnce || !hasSpawned))
        {
            SpawnPlatforms();
            hasSpawned = true;
        }
    }

    void SpawnPlatforms()
    {
        if (platformPrefabs.Count == 0 || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Missing platform prefabs or spawn points.");
            return;
        }

        foreach (Transform point in spawnPoints)
        {
            int randomIndex = Random.Range(0, platformPrefabs.Count);
            GameObject platformToSpawn = platformPrefabs[randomIndex];

            Instantiate(platformToSpawn, point.position, point.rotation);
        }
    }
}
