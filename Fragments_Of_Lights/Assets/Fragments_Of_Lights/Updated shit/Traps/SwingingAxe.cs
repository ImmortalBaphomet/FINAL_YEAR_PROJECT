using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingAngle = 45f;   // Maximum angle the axe swings to (on either side)
    public float swingSpeed = 2f;    // Speed of the swinging motion

    [Header("Damage Settings")]
    public int damageAmount = 25;    // Damage dealt to the player
    public string playerTag = "Player"; // Tag assigned to the player object

    private float time;

    void Update()
    {
        AxeSwing();
    }

    void AxeSwing()
    {
        // Increment time for smooth swinging motion
        time += Time.deltaTime * swingSpeed;

        // Calculate rotation using a sine wave
        float angle = Mathf.Sin(time) * swingAngle;

        // Apply rotation to swing around the Z-axis while keeping forward as +X
        transform.rotation = Quaternion.Euler(0, 90, angle);
        AudioManager.instance.PlayClip(AudioManager.instance.axeSwingAudio, false, 1f);
    }
}




