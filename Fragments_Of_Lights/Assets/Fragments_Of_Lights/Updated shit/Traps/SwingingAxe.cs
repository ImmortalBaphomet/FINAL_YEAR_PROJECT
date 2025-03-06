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
        // Increment time for smooth swinging motion
        time += Time.deltaTime * swingSpeed;

        // Calculate rotation using a sine wave
        float angle = Mathf.Sin(time) * swingAngle;

        // Apply rotation to swing around the Z-axis while keeping forward as +X
        transform.rotation = Quaternion.Euler(0, 90, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag(playerTag))
        {
            // Example damage logic
            //var playerHealth = other.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damageAmount);
            // }
        }
    }
}

/*
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    

}

*/


