using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f; 
    public float dashCooldown = 1f; 
    private float dashTime; 
    private float lastDashTime; 
    private bool isDashing = false; 
    private CharacterController characterController;
    private Vector3 dashDirection; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        dashTime = 0f;
        lastDashTime = -dashCooldown; 
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }

        
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
        
        dashDirection = transform.forward;

        
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time; 

        // add animation/dash trail here!!!!
        
        Debug.Log("Dashing!");
    }
}
