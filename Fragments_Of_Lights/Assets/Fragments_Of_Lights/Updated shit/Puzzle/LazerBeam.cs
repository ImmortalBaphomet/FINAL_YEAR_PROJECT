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
        
        Vector3 laserStart = transform.position;
        Vector3 laserDirection = transform.forward;

        List<Vector3> points = new List<Vector3> { laserStart };

        for (int i = 0; i < 10; i++) // Limit to 10 reflections to prevent infinite loops // put a variable to control how mnay reflections take place
        {
            Ray ray = new Ray(laserStart, laserDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, laserLength))
            {
                points.Add(hit.point);

                if (hit.collider.CompareTag(collisionTag))
                {
                    // Reflect the laser
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                    laserStart = hit.point;
                }
                else
                {
                    break; // Stop if it doesn't hit a reflector
                }
            }
            else
            {
                points.Add(laserStart + laserDirection * laserLength);
                break;
            }
        }

        // Update the LineRenderer positions
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
/*
// Start of the laser beam
    Vector3 laserStart = transform.position;
    Vector3 laserDirection = transform.forward;

    List<Vector3> points = new List<Vector3> { laserStart };

    for (int i = 0; i < 10; i++) // Limit to 10 reflections to prevent infinite loops
    {
        Ray ray = new Ray(laserStart, laserDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, laserLength))
        {
            points.Add(hit.point);

            if (hit.collider.CompareTag(collisionTag))
            {
                // Reflect the laser
                laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                laserStart = hit.point;
            }
            else
            {
                break; // Stop if it doesn't hit a reflector
            }
        }
        else
        {
            points.Add(laserStart + laserDirection * laserLength);
            break;
        }
    }

    // Update the LineRenderer positions
    lineRenderer.positionCount = points.Count;
    lineRenderer.SetPositions(points.ToArray());
}
*/
