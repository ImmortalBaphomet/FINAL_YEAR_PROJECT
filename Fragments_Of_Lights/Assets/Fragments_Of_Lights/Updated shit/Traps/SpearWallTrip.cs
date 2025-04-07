using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearWallTrip : MonoBehaviour
{
    public WallSpear WallSpear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            WallSpear.wallSpearActivate = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger!");
            WallSpear.wallSpearActivate = false;
        }
    }
}
