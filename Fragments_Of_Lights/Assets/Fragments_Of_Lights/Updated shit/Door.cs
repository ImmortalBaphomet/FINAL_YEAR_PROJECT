using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door; // Assign the door GameObject here
    public Transform pivot; // Assign the pivot point as an empty GameObject
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public KeyCode interactKey = KeyCode.E;
    
    private bool isOpen = false;
    private bool playerNear = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = pivot.rotation;
        openRotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y + openAngle, pivot.eulerAngles.z);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }
        
        pivot.rotation = Quaternion.Lerp(pivot.rotation, isOpen ? openRotation : closedRotation, Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}

