using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripSensor : MonoBehaviour
{
    [SerializeField] private List<TrapMaster> traps;
    [SerializeField] private float detectionRadius = 2.0f;

   void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player is near the trip sensor!");
                foreach (TrapMaster trap in traps)
                {
                    trap.Activate();
                }
                break; // Activate traps once
            }
        }
    }
}
