using UnityEngine;

public class Pull_Mechanics : MonoBehaviour
{
    public float pullForce = 10f; // How strong the pull effect is
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
            // Check if the player's color is red
            if (playerColorScript.isRed)
            {
                Debug.Log("Player is in red vent zone. Continuously pulling up!");
                // Apply continuous upward pull force by modifying the player's movement
                Vector3 pullDirection = Vector3.up * pullForce * Time.deltaTime; // Apply force every frame
                playerController.Move(pullDirection); // Apply the pull force through the CharacterController
            }
        }

    }
}
