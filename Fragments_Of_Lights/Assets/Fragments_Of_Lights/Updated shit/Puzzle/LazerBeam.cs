using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : MonoBehaviour
{
    [Header("Laser Settings")]
    public LineRenderer lineRenderer;
    public float laserLength = 10f; // Max length of the laser
    public string collisionTag = "Reflector"; // Tag of objects the laser can collide with

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        // Start of the laser beam
        Vector3 laserStart = transform.position;
        // End of the laser beam (default max length)
        Vector3 laserEnd = laserStart + transform.forward * laserLength;

        // Perform a raycast to check for collisions
        RaycastHit hit;
        if (Physics.Raycast(laserStart, transform.forward, out hit, laserLength))
        {
            // Check if the object has the specified tag
            if (hit.collider.CompareTag(collisionTag))
            {
                // If the object matches the tag, set the laser end to the hit point
                laserEnd = hit.point;

                // Optional: Handle collision logic
                Debug.Log($"Laser collided with object tagged '{collisionTag}': {hit.collider.name}");
            }
        }

        // Update the LineRenderer positions
        lineRenderer.SetPosition(0, laserStart); // Start point
        lineRenderer.SetPosition(1, laserEnd);   // End point
    }
}
