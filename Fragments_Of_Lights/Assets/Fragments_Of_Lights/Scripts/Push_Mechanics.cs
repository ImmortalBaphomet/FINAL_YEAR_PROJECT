using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_Mechanics : MonoBehaviour
{
    public float pushForce = 10f, lowGravity; // How strong the push effect is
    public bool lowGravityZone = false;
    private Color_Change playerColorScript;
    private CharacterController playerController;
    private Player_Jump player_Jump;

    void Start()
    {
        // Assuming the player object is tagged as "Player" and has a CharacterController
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerColorScript = player.GetComponent<Color_Change>(); // Get reference to the player color change script
            playerController = player.GetComponent<CharacterController>(); // Get reference to the CharacterController
            player_Jump = player.GetComponent<Player_Jump>();
        }
    }

    // void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("Player") && playerColorScript != null && playerController != null)
    //     {
    //         lowGravityZone = true;
    //         // Check if the player's color is green
    //         if (playerColorScript.isViolet) // Check if player's color is green
    //         {
    //             Debug.Log("Player is in violet vent zone. Gravity Low!");
    //             player_Jump.gravity = lowGravity;
    //             // Apply continuous upward push force by modifying the player's movement
    //             // Vector3 pushDirection = Vector3.up * pushForce * Time.deltaTime; // Apply force every frame
    //             // playerController.Move(pushDirection); // Apply the push force through the CharacterController
    //         }
    //     }
    // }
    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player") && playerColorScript != null && playerController != null)
    //     {
    //         lowgravi
    //     }
    // }
}
