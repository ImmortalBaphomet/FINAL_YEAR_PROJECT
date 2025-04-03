using UnityEngine;

public class Color_Change : MonoBehaviour
{
    public Material playerMaterial; // Reference to the player's material
    public Light playerLight; // Reference to the point light
    public Material outlineMaterial; // Reference to the outline shader material

    [Header("Color Settings")]
    private Color whiteColor; // Default color (white)
    private Color redColor; // Red color (hex)
    private Color violetColor; // Green color (hex)
    private Color outlineRedColor; // Outline color for red
    private Color outlineVioletColor; // Outline color for green
    private Color outlineWhiteColor; // Outline color for white
    private Color currentColor; // Current color of the player

    [Header("Timer Settings")]
    public float colorChangeDuration = 3f; // Time in seconds before the color reverts to default
    private float colorChangeTimer = 0f; // Timer to track color change duration

    public bool isRed = false; // Boolean to check if player color is red
    public bool isViolet = false; // Boolean to check if player color is green
    private bool canSwitchColor = true; // Flag to control when the player can switch colors

    private Prism_Collection prismScript; // Reference to the Prism_Collection script

    void Start()
    {
        // Convert Hex color to Unity Color
        whiteColor = HexToColor("#FFFFFF"); // White color
        redColor = HexToColor("#A0153E"); // Red color
        violetColor = HexToColor("#7F00FF"); // Violet Color
        // Outline colors
        outlineWhiteColor = HexToColor("#FFF3D1"); // White outline color (hex)
        outlineRedColor = HexToColor("#FF2A68"); // Red outline color (hex)
        outlineVioletColor = HexToColor("#7F00FF"); // Violet outline color (hex)

        currentColor = whiteColor;
        playerMaterial.SetColor("_Color", currentColor); // Apply color to material
        playerLight.color = whiteColor; // Set light to default color (white)
        outlineMaterial.SetColor("_OutlineColor", outlineWhiteColor); // Set initial outline color to white

        prismScript = GetComponent<Prism_Collection>();
    }

    void Update()
    {
        // Only allow color change if the player has a prism and can switch colors
        if (prismScript != null && prismScript.HasPrism())
        {
            if (canSwitchColor)
            {
                
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    UsePrismAndChangeColor(redColor, outlineRedColor); 
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    UsePrismAndChangeColor(violetColor, outlineVioletColor); 
                }
            }
        }
        else if (prismScript != null && !prismScript.HasPrism())
        {
            Debug.Log("No Prism! Cannot change color.");
        }

        // Timer to restore the color to default after the set duration
        if (colorChangeTimer > 0)
        {
            colorChangeTimer -= Time.deltaTime;
        }
        else if (currentColor != whiteColor)
        {
            // Agar timeup hone ke baad bhi nhi hua toh uske liye 
            ResetToDefaultColor();
        }
    }

    void UsePrismAndChangeColor(Color newColor, Color newOutlineColor)
    {
        if (prismScript.ConsumePrism()) // Attempt to use a prism
        {
            ChangeColor(newColor, newOutlineColor);
        }
        else
        {
            Debug.Log("Failed to use prism.");
        }
    }

    void ChangeColor(Color newColor, Color newOutlineColor)
    {
        // Player ka color new color pe set krne ke liye
        currentColor = newColor;
        playerMaterial.SetColor("_Color", currentColor); 

        // Point light match krne ke liye player ke color se 
        if (playerLight != null)
        {
            playerLight.color = currentColor;
        }
        if (outlineMaterial != null)
        {
            outlineMaterial.SetColor("_OutlineColor", newOutlineColor); 
        }

        if (newColor == redColor)
        {
            isRed = true;
            isViolet = false;
        }
        else if (newColor == violetColor)
        {
            isViolet = true;
            isRed = false;
        }
        else
        {
            isRed = false;
            isViolet = false;
        }
        colorChangeTimer = colorChangeDuration;
        canSwitchColor = false;
    }

    void ResetToDefaultColor()
    {
        currentColor = whiteColor;
        playerMaterial.SetColor("_Color", currentColor);
        if (playerLight != null)
        {
            playerLight.color = whiteColor;
        }
        if (outlineMaterial != null)
        {
            outlineMaterial.SetColor("_OutlineColor", outlineWhiteColor);
        }

        isRed = false;
        isViolet = false;

        canSwitchColor = true;

        Debug.Log("Color reset to default. You can switch colors again.");
    }

    // idhar Hexa Decimal color co convert kr raha Unity color mai
    Color HexToColor(string hex)
    {
        hex = hex.Replace("#", ""); 

        // Convert hex string to Color
        float r = Mathf.Clamp01(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f);
        float g = Mathf.Clamp01(int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f);
        float b = Mathf.Clamp01(int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f);

        return new Color(r, g, b);
    }
}
