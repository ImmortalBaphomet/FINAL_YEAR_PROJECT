using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSwitch : MonoBehaviour
{
    public KeyCode activateKey = KeyCode.F;
    public LazerBeam laserToActivate;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(activateKey))
        {
            if (laserToActivate != null)
            {
                laserToActivate.ActivateLaser();
                Debug.Log("Laser activated!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered switch zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left switch zone.");
        }
    }
}
