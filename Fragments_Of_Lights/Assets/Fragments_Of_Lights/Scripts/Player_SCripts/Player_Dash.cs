using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 1f; // Cooldown time between dashes
    private float dashTime; // Time remaining for dash
    private float lastDashTime; // Time of the last dash
    private bool isDashing = false; // Whether the player is currently dashing
    private CharacterController characterController;
    private Vector3 dashDirection; // Direction of the dash

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        dashTime = 0f;
        lastDashTime = -dashCooldown; // Start with the ability to dash
    }

    void Update()
    {
        // Dash input check (e.g., pressing Shift key)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }

        // If the dash is active, move the player
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);

            // End dash after the set duration
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
    }

    void StartDash()
    {
        // Set the dash direction to where the player is facing
        dashDirection = transform.forward;

        // Start the dash
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time; // Record the time of the dash

        // Optional: Play a dash animation or effect here (e.g., particle effects, sound)
        Debug.Log("Dashing!");
    }
}
