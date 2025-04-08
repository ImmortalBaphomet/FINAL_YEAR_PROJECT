using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator), typeof(PlayerGrab))]
public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [SerializeField] private float defSpeed;
    private float currentVelocity; // For smooth rotation

    public float rotationSpeed = 10f;

    private float horizontalInput;
    private float verticalInput;

    private CharacterController characterController;
    private Animator playerAnim;
    private PlayerGrab pGrab;
    private TrailRenderer footTrail;

    private Vector3 moveDirection;
    private Quaternion targetRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        pGrab = GetComponent<PlayerGrab>();
        footTrail = GetComponentInChildren<TrailRenderer>();

        targetRotation = transform.rotation;
        defSpeed = moveSpeed;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (pGrab.isGrabbing)
        {
            HandleRotation(); // Allow rotation while grabbing
            return; // Prevent movement while grabbing
        }

        HandleMove();
    }

    private void HandleMove()
    {
        Vector3 inputVector = new Vector3(horizontalInput, 0f, verticalInput);
        
        if (inputVector.sqrMagnitude > 0.01f)
        {
            moveDirection = inputVector.normalized * moveSpeed;

            // Get target angle in world space
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;

            // Smoothly interpolate the current angle toward the target
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.1f);

            // Apply rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Use speed-based animation blending
        playerAnim.SetFloat("speed", moveDirection.magnitude);

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            rotationInput = rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = -rotationSpeed * Time.deltaTime;
        }

        if (Mathf.Abs(rotationInput) > 0f)
        {
            transform.Rotate(Vector3.up * rotationInput);
        }
    }
}


//old code

/*
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [SerializeField] private float defSpeed;
    private float currentVelocity; // For smooth rotation


    public float rotationSpeed = 10f;

    private float horizontalInput;
    private float verticalInput;

    private CharacterController characterController;
    private Animator playerAnim;
    private PlayerGrab pGrab;
    private TrailRenderer footTrail;

    private Vector3 moveDirection;
    private Quaternion targetRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        pGrab = GetComponent<PlayerGrab>();
        footTrail = GetComponentInChildren<TrailRenderer>();

        targetRotation = transform.rotation;
        defSpeed = moveSpeed;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (pGrab.isGrabbing)
        {
            HandleRotation(); // Allow rotation while grabbing
            return; // Prevent movement while grabbing
        }

        HandleMove();
    }

    private void HandleMove()
    {
        Vector3 inputVector = new Vector3(horizontalInput, 0f, verticalInput);
        
        if (inputVector.sqrMagnitude > 0.01f)
        {
            moveDirection = inputVector.normalized * moveSpeed;

            // Get target angle in world space
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;

            // Smoothly interpolate the current angle toward the target
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.1f);

            // Apply rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            playerAnim.SetBool("Run", true);
        }
        else
        {
            moveDirection = Vector3.zero;
            playerAnim.SetBool("Run", false);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }


    private void HandleRotation()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            rotationInput = rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = -rotationSpeed * Time.deltaTime;
        }

        if (Mathf.Abs(rotationInput) > 0f)
        {
            transform.Rotate(Vector3.up * rotationInput);
        }
    }
*/