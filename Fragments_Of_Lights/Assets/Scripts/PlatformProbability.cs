using UnityEngine;

public class PlatformProbability : MonoBehaviour
{
    [Range(0f, 1f)]
    public float triggerProbability = 0.5f; // Probability of spikes disappearing

    public GameObject spikes; // Assign the spikes manually in the Inspector

    private bool isInitialized = false; // Prevents re-initialization on player respawn

    private void Start()
    {
        InitializePlatform();
    }

    private void InitializePlatform()
    {
        if (isInitialized) return; // Prevents re-initialization

        float randomValue = Random.value; // Generate a random value between 0 and 1
        Debug.Log($"Platform {gameObject.name} Random Value: {randomValue}, Probability Threshold: {triggerProbability}");

        if (spikes != null) 
        {
            if (randomValue <= triggerProbability)
            {
                spikes.SetActive(false); // Deactivate spikes
                Debug.Log($"{gameObject.name} - Spikes disabled!");
            }
            else
            {
                spikes.SetActive(true); // Keep spikes active
                Debug.Log($"{gameObject.name} - Spikes active!");
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} - No spikes assigned in the Inspector!");
        }

        isInitialized = true; // Mark as initialized
    }
}
