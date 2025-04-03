using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public KeyCode rotateClockwiseKey = KeyCode.E;
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q;
    public KeyCode grabKey = KeyCode.F; // Key to grab the object

    [SerializeField] private float maxRotAngle;
    [SerializeField] private float minRotAngle;
    [SerializeField] private float currRotAngle;

    private float targetRotationY;
    public bool isGrabbed = false; // Track if the player is grabbing the object
    public bool playerInrange = false;
    private Transform player;
    private Animator playerAnim; // Player's animator

    void Start()
    {
        targetRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        
        RotateToTarget();
    }

    void HandleInput()
    {
        if (Input.GetKey(rotateClockwiseKey))
        {
            currRotAngle += rotationSpeed * Time.deltaTime;
            currRotAngle = Mathf.Clamp(currRotAngle, minRotAngle, maxRotAngle);
        }
        else if (Input.GetKey(rotateCounterClockwiseKey))
        {
            currRotAngle -= rotationSpeed * Time.deltaTime;
            currRotAngle = Mathf.Clamp(currRotAngle, minRotAngle, maxRotAngle);
        }

        targetRotationY = currRotAngle;
    }

    void RotateToTarget()
    {
        float currentY = transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetRotationY, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.transform;
            playerAnim = player.GetComponentInChildren<Animator>(); // Get player's Animator
            playerInrange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(grabKey))
        {
            isGrabbed = !isGrabbed; // Toggle grabbing state
            HandleInput();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isGrabbed = false;
            if (playerAnim)
            {
                playerAnim.SetBool("isGrab", false);
            }
            player = null;
            playerInrange = false;
        }   
    }
}


