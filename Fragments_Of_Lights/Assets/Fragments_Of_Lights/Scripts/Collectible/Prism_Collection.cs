using UnityEngine;
using TMPro; 
public class Prism_Collection : MonoBehaviour
{
    public int prismCount = 0; 
    public TextMeshProUGUI prismCountText; 
    private bool canUsePrism = true; 
    private float cooldownTimer = 0f; 
    private void Start()
    {
        UpdatePrismUI(); 
    }

    // to check if player has any prisms
    public bool HasPrism()
    {
        return prismCount > 0;
    }

    // to consume a prism
    public bool ConsumePrism()
    {
        if (HasPrism() && canUsePrism)
        {
            prismCount--; // Decrease the prism count
            Debug.Log("You Used a Prism! Remaining: " + prismCount);
            UpdatePrismUI(); // Update the UI

            // Start cooldown
            canUsePrism = false;
            cooldownTimer = 2f; // Set cooldown to 2 seconds
            return true; // Prism successfully used
        }
        else if (!canUsePrism)
        {
            Debug.Log("Cooldown active! Wait for 2 seconds.");
        }
        else
        {
            Debug.Log("No more prisms to use.");
        }

        return false; // Failed to use prism
    }

    private void Update()
    {
        // Handle the cooldown timer
        if (!canUsePrism)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canUsePrism = true; 
                Debug.Log("You can now use another prism!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prism")) 
        {
            prismCount++; 
            Debug.Log("Prism Collected! Total Prisms: " + prismCount);
            UpdatePrismUI(); 
            Destroy(other.gameObject);
        }
    }

    // Method to update the prism count UI
    private void UpdatePrismUI()
    {
        if (prismCountText != null)
        {
            prismCountText.text = prismCount .ToString();
        }
    }
}
