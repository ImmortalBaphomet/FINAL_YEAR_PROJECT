using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Title_Screen : MonoBehaviour
{
    public float delay = 3f;
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public GameObject Start_Screen;
    private bool isTransitioning = false;

    private void Start()
    {
        Start_Screen.SetActive(true);
        videoPlayer.playOnAwake = false; // Ensure the video doesn't play automatically
    }

    void Update()
    {
        if (Input.anyKeyDown && !isTransitioning)
        {
            Start_Screen.SetActive(false);
            videoPlayer.Play();
            StartCoroutine(PlayVideoAndChangeScene());
        }
    }

    private IEnumerator PlayVideoAndChangeScene()
    {
        isTransitioning = true; 
        yield return new WaitForSeconds(delay); 
        SceneManager.LoadScene(1); 
    }

}
