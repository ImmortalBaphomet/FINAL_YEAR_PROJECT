using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryAndExit : MonoBehaviour
{
    [Tooltip("Target Transform to teleport the player to")]
    public Transform teleportTarget;

    [Tooltip("Tag of the player GameObject (default: Player)")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Disable controller to avoid teleport bug
                controller.enabled = false;
                other.transform.position = teleportTarget.position;
                other.transform.rotation = teleportTarget.rotation;
                controller.enabled = true;
            }
            else
            {
                // If not using CharacterController
                other.transform.position = teleportTarget.position;
                other.transform.rotation = teleportTarget.rotation;
            }

            Debug.Log("Teleported " + other.name + " to " + teleportTarget.position);
        }
    }
}
