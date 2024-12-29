using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_Mechanics : MonoBehaviour
{
    public float pushForce = 10f; // How strong the push effect is
    private Color_Change playerColorScript;
    private CharacterController playerController;

    void Start()
    {
        // Assuming the player object is tagged as "Player" and has a CharacterController
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerColorScript = player.GetComponent<Color_Change>(); // Get reference to the player color change script
            playerController = player.GetComponent<CharacterController>(); // Get reference to the CharacterController
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerColorScript != null && playerController != null)
        {
            // Check if the player's color is green
            if (playerColorScript.isGreen) // Check if player's color is green
            {
                Debug.Log("Player is in green vent zone. Continuously pushing up!");
                // Apply continuous upward push force by modifying the player's movement
                Vector3 pushDirection = Vector3.up * pushForce * Time.deltaTime; // Apply force every frame
                playerController.Move(pushDirection); // Apply the push force through the CharacterController
            }
        }
    }
}
