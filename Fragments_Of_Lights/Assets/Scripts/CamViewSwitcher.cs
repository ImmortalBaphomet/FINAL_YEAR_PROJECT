using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamViewSwitcher : MonoBehaviour
{
    public Camera sideCam;
    public Camera isoCam;

    private bool usingSideCam = true;
    private void Awake()
    {
        sideCam = FindAnyObjectByType<Camera>();
    }
    public void ToggleCameras()
    {
        usingSideCam = !usingSideCam;

        sideCam.enabled = usingSideCam;
        isoCam.enabled = !usingSideCam;

        // Optional: Set tag to MainCamera for the active cam (if needed for camera-dependent scripts)
        if (usingSideCam)
        {
            sideCam.tag = "MainCamera";
            isoCam.tag = "Untagged";
        }
        else
        {
            isoCam.tag = "MainCamera";
            sideCam.tag = "Untagged";
        }
    }
}

