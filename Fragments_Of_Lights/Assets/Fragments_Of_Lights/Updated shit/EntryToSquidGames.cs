using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryToSquidGames : MonoBehaviour
{
    [Tooltip("Puzzle prefab to instantiate")]
    public GameObject puzzlePrefab;

    [Tooltip("Position where the puzzle prefab will be spawned")]
    public Transform puzzleSpawnPoint;

    [Tooltip("Tag of the player GameObject")]
    public string playerTag = "Player";

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag(playerTag))
        {
            hasSpawned = true;

            // Instantiate puzzle prefab
            GameObject spawnedPuzzle = Instantiate(puzzlePrefab, puzzleSpawnPoint.position, puzzleSpawnPoint.rotation);

            // Find the sendtoTarget inside the newly spawned prefab
            Transform sendtoTarget = spawnedPuzzle.transform.Find("sendtoTarget");

            if (sendtoTarget == null)
            {
                Debug.LogError("sendtoTarget not found in the instantiated prefab!");
                return;
            }

            // Move the player to the target
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = sendtoTarget.position;
                other.transform.rotation = sendtoTarget.rotation;
                controller.enabled = true;
            }
            else
            {
                other.transform.position = sendtoTarget.position;
                other.transform.rotation = sendtoTarget.rotation;
            }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            // Disable the current main camera
            Camera currentMainCamera = Camera.main;
            if (currentMainCamera != null)
            {
                currentMainCamera.gameObject.SetActive(false);
            }

            // Find and enable the camera inside the puzzle prefab
            Camera puzzleCam = spawnedPuzzle.GetComponentInChildren<Camera>(true); // true to include inactive objects
            if (puzzleCam != null)
            {
                puzzleCam.gameObject.SetActive(true);
                Debug.Log("Switched to puzzle camera.");
            }
            else
            {
                Debug.LogWarning("No camera found inside the puzzle prefab.");
            }

=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            Debug.Log("Teleported " + other.name + " to " + sendtoTarget.position);
        }
    }
}
