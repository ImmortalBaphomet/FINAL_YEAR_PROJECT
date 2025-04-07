using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrip : MonoBehaviour
{
    public TrapWalls trapWalls;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            trapWalls.wallActivate = true;  
        }
    }

    
}
