using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   public float rotationSpeed = 100f; // Speed of rotation
    public KeyCode rotateClockwiseKey = KeyCode.E; // Key to rotate clockwise
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q; // Key to rotate counterclockwise

    private float targetRotationY; // Target rotation angle

    void Start()
    {
        // Initialize the target rotation to the current rotation
        targetRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleInput();
        RotateToTarget();
    }

    void HandleInput()
    {
        // Rotate clockwise
        if (Input.GetKeyDown(rotateClockwiseKey))
        {
            targetRotationY += 90f;
        }
        // Rotate counterclockwise
        if (Input.GetKeyDown(rotateCounterClockwiseKey))
        {
            targetRotationY -= 90f;
        }

        // Clamp target rotation to stay within 0-360 degrees
        targetRotationY = Mathf.Repeat(targetRotationY, 360f);
    }

    void RotateToTarget()
    {
        // Smoothly rotate to the target angle
        float currentY = transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetRotationY, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
    }

    
}
