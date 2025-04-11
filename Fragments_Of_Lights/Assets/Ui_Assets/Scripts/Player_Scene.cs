using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Player_Scene : MonoBehaviour
{
    public bool Start_Button = false;
    public bool Credit_Button = false;
    public bool Quit_BUtton = false;
    public bool Glass_Area = false;
    public bool Gate_Area = false;

    [Header("Video Settings")]
    public VideoPlayer videoPlayer; // Assign your VideoPlayer here
    private bool isVideoPlaying = false;

    private void Update()
    {
        if (Start_Button && Input.GetKeyDown(KeyCode.Return) && !isVideoPlaying)
        {
            if (videoPlayer != null)
            {
                isVideoPlaying = true;
                videoPlayer.gameObject.SetActive(true); // Just in case it's disabled
                videoPlayer.Play();
                videoPlayer.loopPointReached += OnVideoFinished;
            }
            else
            {
                SceneManager.LoadScene(1); // Fallback in case video isn't assigned
            }
        }

        if (Credit_Button && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Credit_scene");
        }

        if (Quit_BUtton && Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
            Debug.Log("Quit");
        }

        if (Gate_Area)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        SceneManager.LoadScene(1); // Load your game scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass")) Glass_Area = true;
        if (other.CompareTag("Gate")) Gate_Area = true;
        if (other.CompareTag("Start")) Start_Button = true;
        if (other.CompareTag("Credits")) Credit_Button = true;
        if (other.CompareTag("Quit")) Quit_BUtton = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Glass")) Glass_Area = false;
        if (other.CompareTag("Gate")) Gate_Area = false;
        if (other.CompareTag("Start")) Start_Button = false;
        if (other.CompareTag("Credits")) Credit_Button = false;
        if (other.CompareTag("Quit")) Quit_BUtton = false;
    }
}
