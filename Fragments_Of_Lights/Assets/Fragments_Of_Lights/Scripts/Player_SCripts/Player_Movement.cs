using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [SerializeField] private float defSpeed;

    [SerializeField] private PlayerGrab pGrab;
    public float rotationSpeed = 10f;
    private float refFloat;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Quaternion targetRotation;

    public Animator playerAnim;
    private /* ParticleSystem */ TrailRenderer footTrail;
    private Vector3 lastPos;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        pGrab = GetComponent<PlayerGrab>();
        targetRotation = transform.rotation; // Set the initial rotation
        footTrail = GetComponentInChildren<TrailRenderer>();
        lastPos = transform.position;
        defSpeed = moveSpeed;
    }

    private void Update()
    {
        if (pGrab.isGrabbing)
        {
            HandleRotation(); // Allow rotation while grabbing
            return; // Prevent movement while grabbing
        }
        
        HandleMove();
    }

    void HandleMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;

            targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0f, verticalInput));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            playerAnim.SetBool("Run", true);
        }
        else
        {
            moveDirection = Vector3.zero;
            playerAnim.SetBool("Run", false);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleRotation()
    {
        float rotationInput = 0;

        if (Input.GetKey(KeyCode.E))
        {
            rotationInput = rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = -rotationSpeed * Time.deltaTime;
        }

        transform.Rotate(Vector3.up * rotationInput);
    }
}
