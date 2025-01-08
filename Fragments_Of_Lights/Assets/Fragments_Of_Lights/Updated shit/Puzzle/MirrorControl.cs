using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorControl : MonoBehaviour
{
    [Header("Mirror Settings")]
    public float rotationSpeed = 100f; // Speed of rotation
    [SerializeField] private bool isSelected = false;   // check for mirror is selected
    public KeyCode rotateClockwiseKey = KeyCode.E; 
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q; 
    [SerializeField] private float maxRotAngle; // not using clamping, just 
    [SerializeField] private float minRotAngle;
    private float targetRotationY; // Target rotation angle

    void Start()
    {
        targetRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        if (isSelected)
        {
            
            HandleInput();
            RotateToTarget();
        }
    }

    // Called to select this mirror
    public void SelectMirror(bool select)
    {
        isSelected = select;
    }

    void HandleInput()
    {
        if (Input.GetKey(rotateClockwiseKey))
        {
            targetRotationY += rotationSpeed * Time.deltaTime; // Increment target rotation
        }
        // Rotate counterclockwise
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            targetRotationY -= rotationSpeed * Time.deltaTime; // Decrement target rotation
        }

        // Clamp target rotation to stay within specified limits
        targetRotationY = Mathf.Clamp(targetRotationY, minRotAngle, maxRotAngle);
    }
    void RotateToTarget()
    {
        // Smoothly rotate to the target angle
        float currentY = transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetRotationY, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
    }
}
