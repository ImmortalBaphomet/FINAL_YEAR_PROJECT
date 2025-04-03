using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    public bool isPressed = false; // Check if the button is activated

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player steps on the button
        {
            isPressed = true;
            Debug.Log("Button Activated: " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player leaves the button
        {
            isPressed = false;
            Debug.Log("Button Deactivated: " + gameObject.name);
        }
    }
}
