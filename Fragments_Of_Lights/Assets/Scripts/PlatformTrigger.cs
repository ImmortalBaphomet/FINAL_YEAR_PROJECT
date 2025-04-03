using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public GameObject[] platforms; // Array of platforms to enable
    private bool hasTriggered = false; // Ensure activation happens only once

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Prevent multiple activations
            ActivatePlatforms();
        }
    }

    private void ActivatePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                platform.SetActive(true); // Enable the platform
                Debug.Log("Platform activated: " + platform.name);
            }
        }
    }
}
