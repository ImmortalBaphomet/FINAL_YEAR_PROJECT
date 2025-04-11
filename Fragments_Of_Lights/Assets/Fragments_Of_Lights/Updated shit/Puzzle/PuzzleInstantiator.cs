using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInstantiator : MonoBehaviour
{
    public GameObject puzzlePrefab;        // Assign your puzzle prefab in the inspector
    public Transform spawnPoint;           // Optional: assign a specific spawn position
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnPuzzle();
            hasSpawned = true;  // Prevent multiple spawns
        }
    }

    void SpawnPuzzle()
    {
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion spawnRotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(puzzlePrefab, spawnPosition, spawnRotation);
        Debug.Log("Puzzle spawned!");
    }

}
