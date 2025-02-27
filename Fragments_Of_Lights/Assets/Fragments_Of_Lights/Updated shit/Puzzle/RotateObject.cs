using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   public float rotationSpeed = 100f; 
    public KeyCode rotateClockwiseKey = KeyCode.E; 
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q; 
    [SerializeField] private float posRotAngle;// not using clamping, just 
    [SerializeField] private float negRotAngle;
    [SerializeField] private float currRotAngle;

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
        if (Input.GetKey(rotateClockwiseKey))
        {
            targetRotationY = Mathf.Clamp(currRotAngle,negRotAngle,posRotAngle);
        }
        // Rotate counterclockwise
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            targetRotationY = Mathf.Clamp(currRotAngle,negRotAngle,posRotAngle);
        }

        // Clamp target rotation to stay within 0-360 degrees
        //targetRotationY = Mathf.Repeat(targetRotationY, 360f);
    }

    void RotateToTarget()
    {
        // Smoothly rotate to the target angle
        float currentY = transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetRotationY, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
    }

    
}
