using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Scene : MonoBehaviour
{
    public bool Start_Button = false;
    public bool Credit_Button = false; 
    public bool Quit_BUtton = false; 
    public bool Glass_Area = false; 
    public bool Gate_Area = false; 


    private void Update()
    {
        if(Start_Button && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }

        if (Credit_Button && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Credit_scene");
        }

        if (Quit_BUtton && Input.GetKey(KeyCode.Return))
        {
           Application.Quit();
            Debug.Log("Quit");
        }

        if(Gate_Area)
        {
            SceneManager.LoadScene(0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass")) // Check if the player leaves the button
        {
            Glass_Area = true;

        }

        if (other.CompareTag("Gate")) // Check if the player leaves the button
        {
            Gate_Area = true;

        }

        if (other.CompareTag("Start")) 
        {
            Start_Button = true;
            
        }

        if (other.CompareTag("Credits"))
        {
            Credit_Button = true;

        }

        if (other.CompareTag("Quit"))
        {
            Quit_BUtton = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Glass")) // Check if the player leaves the button
        {
            Glass_Area = false;

        }

        if (other.CompareTag("Gate")) // Check if the player leaves the button
        {
            Gate_Area = false;

        }

        if (other.CompareTag("Start")) // Check if the player leaves the button
        {
            Start_Button = false;
            
        }

        if (other.CompareTag("Credits"))
        {
            Credit_Button = false;

        }

        if (other.CompareTag("Quit"))
        {
            Quit_BUtton = false;

        }
    }
}
