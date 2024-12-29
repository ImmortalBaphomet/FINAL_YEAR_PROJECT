using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Light_Control : MonoBehaviour
{
    #region Variables

    [Header("Intensity Settings")]
    public Material material; // Material with the shader for intensity control
    public string intensityPropertyName = "_Intensity"; // Name of the property for intensity in shader

    public float defaultIntensity = 2f; // Target intensity when moving
    public float maxIntensity = 6f; // Intensity for ultimate ability
    public float intensityDecreaseRate = 1f; // Rate of intensity decrease per second
    public float intensityIncreaseRate = 1f; // Rate of intensity increase per second
    public float ultimateIncreaseRate = 2f; // Rate of intensity increase during ultimate
    public float ultimateDecreaseRate = 0.5f; // Rate of intensity decrease after ultimate
    public float ultimateDuration = 10f; // Duration of ultimate ability
    public float idleDelay = 4f; // Delay in seconds before intensity starts decreasing

    private float intensityValue; // Current intensity value
    private float idleTimer = 0f; // Timer to track idle time
    private Vector3 lastPosition; // Player's position in the previous frame
    private bool isMoving = false; // Flag to detect movement
    private bool isUltimateActive = false; // Flag to check if ultimate ability is active
    private bool isPostUltimate = false; // Flag for post-ultimate transition
    private float ultimateTimer = 0f; // Timer to track ultimate ability duration

    #endregion

    public GameObject failarea;

   

    void Start()
    {
        

        // Set default intensity and initialize material property
        intensityValue = defaultIntensity;

        // Initialize the material property
        if (material != null)
        {
            material.SetFloat(intensityPropertyName, intensityValue);
        }
        else
        {
            Debug.LogError("Material not assigned. Please assign a material with the shader.");
        }

        // Store the initial position of the player
        lastPosition = transform.position;
    }


    void Update()
    {
        // Handle ultimate ability
        if (isUltimateActive)
        {
            HandleUltimate();
            return; // Skip normal logic during ultimate
        }

        // Handle post-ultimate transition
        if (isPostUltimate)
        {
            HandlePostUltimate();
            return;
        }

        // Checking if player is moving by comparing current and last positions
        isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f; // Small threshold to detect movement

        if (isMoving)
        {
            idleTimer = 0f; // Reset idle timer
            if (intensityValue < defaultIntensity )
            {
                // Smoothly increase intensity to the default value
                intensityValue = Mathf.Lerp(intensityValue, defaultIntensity, intensityIncreaseRate * Time.deltaTime);
                material.SetFloat(intensityPropertyName, intensityValue);
            }
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDelay && intensityValue > 0)
            {
                // Smoothly decrease intensity to zero
                intensityValue = Mathf.Lerp(intensityValue, 0, intensityDecreaseRate * Time.deltaTime);
                material.SetFloat(intensityPropertyName, intensityValue);

                if(intensityValue < 0.001)
                {
                    failarea.SetActive(true);
                    Debug.Log("Die");
                }
            }
        }

        // Update last position for the next frame
        lastPosition = transform.position;

        // Trigger ultimate ability on 'X' key press
        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateUltimate();
        }
    }

   

    #region Ultimate and Post-Ultimate Handling

    private void ActivateUltimate()
    {
        isUltimateActive = true;
        isPostUltimate = false;
        ultimateTimer = ultimateDuration;
        Debug.Log("Ultimate activated!");
    }

    private void HandleUltimate()
    {
        if (ultimateTimer > 0)
        {
            ultimateTimer -= Time.deltaTime;
            // Smoothly increase intensity to max during the ultimate
            intensityValue = Mathf.Lerp(intensityValue, maxIntensity, ultimateIncreaseRate * Time.deltaTime);
            material.SetFloat(intensityPropertyName, intensityValue);
        }
        else
        {
            isUltimateActive = false;
            isPostUltimate = true; // Start post-ultimate transition
            Debug.Log("Ultimate ended.");
        }
    }

    private void HandlePostUltimate()
    {
        // Smoothly decrease intensity to default
        intensityValue = Mathf.Lerp(intensityValue, defaultIntensity, ultimateDecreaseRate * Time.deltaTime);
        material.SetFloat(intensityPropertyName, intensityValue);

        // Once intensity reaches defaultIntensity, end the post-ultimate state
        if (Mathf.Abs(intensityValue - defaultIntensity) < 0.01f)
        {
            intensityValue = defaultIntensity; // Ensure it stays at default intensity
            isPostUltimate = false; // End post-ultimate transition
            idleTimer = 0f; // Reset idle timer to resume idle logic
        }
    }

    #endregion
}
