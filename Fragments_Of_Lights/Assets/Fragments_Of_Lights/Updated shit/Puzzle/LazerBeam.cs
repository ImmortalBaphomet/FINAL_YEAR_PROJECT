using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : MonoBehaviour
{
    [Header("Laser Settings")]
    public LineRenderer lineRenderer;
    public float laserLength = 10f; 
    public int maxReflections = 10;
    public string reflectorTag = "Reflector";
    public string refractorTag = "Refractor";

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

        for (int i = 0; i < maxReflections; i++)
        {
            Ray ray = new Ray(laserStart, laserDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, laserLength))
            {
                points.Add(hit.point);

                if (hit.collider.CompareTag(reflectorTag))
                {
                    // Reflect the laser
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                    laserStart = hit.point;
                }
                else if (hit.collider.CompareTag(refractorTag))
                {
                    // Refract the laser
                    RefractionObj refractiveObject = hit.collider.GetComponent<RefractionObj>();
                    if (refractiveObject != null)
                    {
                        Vector3 refractedDirection;
                        if (refractiveObject.RefractLaser(laserDirection, hit.normal, out refractedDirection))
                        {
                            laserDirection = refractedDirection; // Apply new refracted direction
                        }
                        else
                        {
                            // If Total Internal Reflection, reflect instead
                            laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                        }
                        laserStart = hit.point;
                    }
                }
                else
                {
                    break; // Stop if the laser hits a non-reflective/refractive surface
                }
            }
            else
            {
                points.Add(laserStart + laserDirection * laserLength);
                break;
            }
        }

        // Update LineRenderer with laser path
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    
    }



}
/*
 
}
*/
