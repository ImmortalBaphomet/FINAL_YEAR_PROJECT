using System.Collections;
using UnityEngine;

public class HKcam : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The player or object the camera follows

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // Speed of the camera follow (lower = slower)
    public Vector3 offset; // Optional offset to position the camera slightly above or below

    [Header("Dead Zone Settings")]
    public bool useDeadZone = true;
    public Vector2 deadZoneSize = new Vector2(2f, 1.5f); // Width and height of the dead zone

    [Header("Camera Boundaries")]
    public bool useBoundaries = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        if (useDeadZone)
        {
            Vector3 delta = desiredPosition - transform.position;

            if (Mathf.Abs(delta.x) > deadZoneSize.x / 2)
                desiredPosition.x = transform.position.x + delta.x - Mathf.Sign(delta.x) * (deadZoneSize.x / 2);

            if (Mathf.Abs(delta.y) > deadZoneSize.y / 2)
                desiredPosition.y = transform.position.y + delta.y - Mathf.Sign(delta.y) * (deadZoneSize.y / 2);
            else
                desiredPosition.y = transform.position.y; // Prevents unnecessary vertical movement
        }

        // Smooth camera movement
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Apply camera boundaries if enabled
        if (useBoundaries)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        }

        // Set the camera's position
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
