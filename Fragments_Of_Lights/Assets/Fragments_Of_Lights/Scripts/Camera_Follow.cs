using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 0.125f; // The speed of the camera smoothing
    public Vector3 offset; // Offset to maintain from the player

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return; // If no target is assigned, do nothing

        // Desired position based on the player's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
