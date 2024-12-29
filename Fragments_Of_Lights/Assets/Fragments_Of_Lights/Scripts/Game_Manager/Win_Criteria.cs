using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Criteria : MonoBehaviour
{
    public GameObject WinMenuUI;
    public GameObject FailMenuUI;
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Win_Area"))
        {
            WinMenuUI.SetActive(true);
        }

        if(other.gameObject.CompareTag("Fail_Area"))
        {
            FailMenuUI.SetActive(true);
        }

    }
}
