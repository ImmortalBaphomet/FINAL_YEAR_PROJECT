using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Criteria : MonoBehaviour
{
    public GameObject WinMenuUI;
    public GameObject FailMenuUI;


    public GameObject Main_Camera;
    public GameObject Decision_Cam;

    public GameObject Happy_Boy;
    public GameObject Sad_Boy;

    public bool isPaused;

    private void Start()
    {
        Sad_Boy.SetActive(false);
        Happy_Boy.SetActive(false);

        Decision_Cam.SetActive(false);
        Main_Camera.SetActive(true);
        isPaused = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Win_Area"))
        {
            Happy_Boy.SetActive(true);
            WinMenuUI.SetActive(true);
            Main_Camera.SetActive(false);
            Decision_Cam.SetActive(true);
        }

        if(other.gameObject.CompareTag("Fail_Area"))
        {
            Sad_Boy.SetActive(true);
            FailMenuUI.SetActive(true);
            Decision_Cam.SetActive(true);
            Main_Camera.SetActive(false);
        }

    }
}
