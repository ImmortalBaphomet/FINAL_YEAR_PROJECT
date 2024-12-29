using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] Transform reSpawnPt;
   void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.position = reSpawnPt.position;
        }
   }
}
