using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private LazerBeam lazerBeam;
    [SerializeField] private MeshRenderer endMesh;
    [SerializeField] private Color endColor;
    [SerializeField] private Color defColor;

    //[Header("Platform Spawning")]
    //[SerializeField] private List<GameObject> platformPrefabs;
    //[SerializeField] private List<Transform> spawnPoints;
    //private bool hasSpawnedPlatforms = false;

    void Start()
    {
        lazerBeam = FindAnyObjectByType<LazerBeam>();
        endMesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        LazerFinish();
    }

    void LazerFinish()
    {
        if (lazerBeam.hitFinish)
        {
            endMesh.material.color = endColor;

            //if (!hasSpawnedPlatforms)
            //{
            //    SpawnPlatforms();
            //    hasSpawnedPlatforms = true;
            //}
        }
        else
        {
            endMesh.material.color = defColor;
        }
    }

    //void SpawnPlatforms()
    //{
    //    if (platformPrefabs.Count == 0 || spawnPoints.Count == 0)
    //    {
    //        Debug.LogWarning("Missing platform prefabs or spawn points.");
    //        return;
    //    }

    //    int spawnCount = Mathf.Min(spawnPoints.Count, 5); // Max 5 platforms

    //    for (int i = 0; i < spawnCount; i++)
    //    {
    //        int randomIndex = Random.Range(0, platformPrefabs.Count);
    //        Instantiate(platformPrefabs[randomIndex], spawnPoints[i].position, spawnPoints[i].rotation);
    //    }
    //}
}
