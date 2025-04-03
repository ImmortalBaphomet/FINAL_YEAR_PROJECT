using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Glow : MonoBehaviour
{
    public Button_Script buttonTriggerScript; // Reference to the button script
    public Color startColor = Color.black; // Default dark color
    public Color targetColor = Color.white; // Glowing color
    public float transitionSpeed = 2f; // Smooth transition speed

    private Renderer objectRenderer;
    private Material material;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            material = objectRenderer.material;
            material.color = startColor; // Set initial color
        }
    }

    void Update()
    {
        if (material == null || buttonTriggerScript == null) return;

        // Check the bool from the button script
        bool isPlayerInside = buttonTriggerScript.isPressed;

        // Smoothly transition the color
        Color currentColor = material.color;
        Color newColor = isPlayerInside ? targetColor : startColor;
        material.color = Color.Lerp(currentColor, newColor, transitionSpeed * Time.deltaTime);
    }
}
