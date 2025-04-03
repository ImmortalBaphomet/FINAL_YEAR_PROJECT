using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionObj : MonoBehaviour
{
    [Header("Refraction Settings")]
    [SerializeField] private float refractiveIndex = 1.5f; // Adjust this to simulate different materials

    public bool RefractLaser(Vector3 incidentDirection, Vector3 hitNormal, out Vector3 refractedDirection)
    {
        // Assume air has an index of 1.0
        float n1 = 1.0f; 
        float n2 = refractiveIndex;

        // Check if entering or exiting the material
        float cosTheta1 = Vector3.Dot(-incidentDirection, hitNormal);
        if (cosTheta1 < 0) // Ray exiting the material
        {
            float temp = n1;
            n1 = n2;
            n2 = temp;
            hitNormal = -hitNormal; // Invert normal when exiting
            cosTheta1 = Vector3.Dot(-incidentDirection, hitNormal);
        }

        // Applying Snell's Law: n1 * sin(theta1) = n2 * sin(theta2)
        float ratio = n1 / n2;
        float sinTheta2Sq = 1 - (ratio * ratio) * (1 - (cosTheta1 * cosTheta1));

        if (sinTheta2Sq < 0)
        {
            // Total Internal Reflection occurs
            refractedDirection = Vector3.Reflect(incidentDirection, hitNormal);
            return false; // Indicate reflection instead of refraction
        }
        else
        {
            // Calculate refracted direction
            float cosTheta2 = Mathf.Sqrt(sinTheta2Sq);
            refractedDirection = (ratio * incidentDirection) + (ratio * cosTheta1 - cosTheta2) * hitNormal;
            return true; // Indicate successful refraction
        }
    }
    
}

