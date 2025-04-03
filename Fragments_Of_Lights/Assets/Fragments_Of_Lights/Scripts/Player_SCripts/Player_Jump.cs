using System.Collections;
using System.Collections.Generic;
//dddusing UnityEditor.Callbacks;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    private float gravityMod;
    [SerializeField] private float fallModifier;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    public bool isviolet;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
        
        characterController.Move(velocity * Time.deltaTime);
        
    }
    

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
            isFalling = false;
            SetAnimationStates(true, false, false); // Grounded, not jumping, not falling
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            isFalling = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityMod);
            SetAnimationStates(false, true, false); // Not grounded, jumping, not falling
        }

        if (!isGrounded && velocity.y < 0 && !isFalling) 
        {
            isFalling = true;
            isJumping = false;
            SetAnimationStates(false, false, true); // Not grounded, not jumping, falling
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravityMod * fallModifier *  Time.deltaTime;
    }

    private void SetAnimationStates(bool grounded, bool jumping, bool falling)
    {
        playerAnim.SetBool("Is_Grounded", grounded);
        playerAnim.SetBool("Is_Jumping", jumping);
        playerAnim.SetBool("Is_Falling", falling);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered gravity zone: " + other.gameObject.tag);

        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            lowGravityZone = true;
            gravityMod = lowGravity;
            Debug.Log("Gravity set to LOW: " + gravityMod + "Player color is " + color_Change.isViolet);
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            highGravityZone = true;
            gravityMod = highGravity;
            Debug.Log("Gravity set to HIGH: " + gravityMod);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            gravityMod = lowGravity;
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            gravityMod = highGravity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            lowGravityZone = false;
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            highGravityZone = false;
        }

        if (!lowGravityZone && !highGravityZone)
        {
            gravityMod = defaultGravity;
            Debug.Log("Gravity reset to NORMAL: " + gravityMod);
        }
    }

    
}

/*
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float gravity = -20f;

    public Animator playerAnim;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    

    [Header("Booleans to control animations")] 
    private bool isJumping;
    private bool isFalling;


    

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the player is grounded using a sphere at groundCheck's position
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset falling velocity if grounded, and check for landing animation
        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Slightly negative value to stay grounded
                if (!isJumping)
                {
                    // Trigger landing animation
                    if (!playerAnim.GetBool("Is_Grounded"))
                    {
                        playerAnim.SetBool("Is_Grounded", true); // Play landing animation
                        playerAnim.SetBool("Is_Falling", false); // Ensure falling animation stops
                    }
                }
            }
            else
            {
                // Stop falling animation when grounded
                if (isFalling)
                {
                    playerAnim.SetBool("Is_Falling", false);
                    isFalling = false;
                }
            }
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            isFalling = false;

            // Trigger jump animation
            playerAnim.SetBool("Is_Jumping", true);
            playerAnim.SetBool("Is_Falling", false);
            playerAnim.SetBool("Is_Grounded", false); // Stop landing animation

            // Apply the jump force
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Falling logic - if player is in the air, not jumping
        if (!isGrounded && isJumping )
        {
            if (!isFalling )
            {
                isJumping = false;
                isGrounded = false;
                isFalling = true;
                playerAnim.SetBool("Is_Falling", true); // Start falling animation
                playerAnim.SetBool("Is_Jumping", false);
            }
        }

        if (!isGrounded && !isJumping)
        {
            isFalling = true;
            playerAnim.SetBool("Is_Falling", true);
            playerAnim.SetBool("Is_Jumping", false);
            playerAnim.SetBool("Is_Grounded", false); // Ensure landing animation isn't playing
        }

        // Apply gravity for continuous falling
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Move the player
        characterController.Move(velocity * Time.deltaTime);


        
    }

    // This function is called after the player lands, to reset animation states
    private void OnLand()
    {
        isJumping = false;
        isFalling = false;
        playerAnim.SetBool("Is_Jumping", false);
        playerAnim.SetBool("Is_Falling", false);
        playerAnim.SetBool("Is_Grounded", true); // Trigger landing animation
    }

    


    
}
*/

/*
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
[Header("Jump Settings")]
    public float jumpForce = 8f;
    public float defaultGravity = -20f, lowGravity = -5f, highGravity = -30f;
    [SerializeField] private float gravityMod;

    public bool lowGravityZone = false, highGravityZone = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded, isJumping, isFalling;

    private const string VIOLET_AREA = "VioletArea";
    private const string RED_AREA = "RedArea";

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
        gravityMod = defaultGravity; // Set gravity to default at start
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        HandleJump();
        
        characterController.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
            isFalling = false;
            SetAnimationStates(true, false, false); // Grounded, not jumping, not falling
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            isFalling = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityMod);
            SetAnimationStates(false, true, false); // Not grounded, jumping, not falling
        }

        if (!isGrounded && velocity.y < 0 && !isFalling) 
        {
            isFalling = true;
            isJumping = false;
            SetAnimationStates(false, false, true); // Not grounded, not jumping, falling
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravityMod * Time.deltaTime;
    }

    private void SetAnimationStates(bool grounded, bool jumping, bool falling)
    {
        playerAnim.SetBool("Is_Grounded", grounded);
        playerAnim.SetBool("Is_Jumping", jumping);
        playerAnim.SetBool("Is_Falling", falling);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered gravity zone: " + other.gameObject.tag);

        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            lowGravityZone = true;
            gravityMod = lowGravity;
            Debug.Log("Gravity set to LOW: " + gravityMod);
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            highGravityZone = true;
            gravityMod = highGravity;
            Debug.Log("Gravity set to HIGH: " + gravityMod);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            gravityMod = lowGravity;
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            gravityMod = highGravity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            lowGravityZone = false;
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            highGravityZone = false;
        }

        if (!lowGravityZone && !highGravityZone)
        {
            gravityMod = defaultGravity;
            Debug.Log("Gravity reset to NORMAL: " + gravityMod);
        }
    }
*/
