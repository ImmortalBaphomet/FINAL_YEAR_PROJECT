using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float gravity = -20f, lowGravity = -5f, highGravity = -20f;
    public bool lowGravityZone = false, HighGravityZone = false;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private Color_Change color_Change;
    private Animator playerAnim;
    private Vector3 velocity;
    private bool isGrounded, isJumping, isFalling;
    private const string violetArea = "VioletArea", redArea = "RedArea";



    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        color_Change = GetComponent<Color_Change>();
        
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
            if (!isJumping)
            {
                SetAnimationStates(true, false, false); // Grounded, not jumping, not falling
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            isFalling = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            SetAnimationStates(false, true, false); // Not grounded, jumping, not falling
        }

        // if (!isGrounded && (isJumping || !isJumping))
        // {
        //     isFalling = true;
        //     isJumping = false;
        //     SetAnimationStates(false, false, true); // Not grounded, not jumping, falling
        // }
        if (!isGrounded && velocity.y < 0) // Falling only if moving downward
        {
            isFalling = true;
            isJumping = false;
            SetAnimationStates(false, false, true);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded && !(lowGravityZone || HighGravityZone))
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(lowGravityZone)
        {
            velocity.y += lowGravity * Time.deltaTime;
        }
        else if(HighGravityZone)
        {
            velocity.y += highGravity * Time.deltaTime;
        }
    }

    private void SetAnimationStates(bool grounded, bool jumping, bool falling)
    {
        playerAnim.SetBool("Is_Grounded", grounded);
        playerAnim.SetBool("Is_Jumping", jumping);
        playerAnim.SetBool("Is_Falling", falling);
    }

    //Below Logic needs to be improved. make an flow chart to check bugs.
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered gravity zone: " + other.gameObject.tag);

        if (other.CompareTag(violetArea) && color_Change.isViolet)
        {
            lowGravityZone = true;
            gravity = lowGravity;
            Debug.Log("Gravity set to LOW: " + gravity);
        }
        else if (other.CompareTag(redArea) && color_Change.isRed)
        {
            HighGravityZone = true;
            gravity = highGravity;
            Debug.Log("Gravity set to HIGH: " + gravity);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("In gravity zone: " + other.gameObject.tag);

        if (other.CompareTag(violetArea) && color_Change.isViolet)
        {
            gravity = lowGravity;
            Debug.Log("Gravity still LOW: " + gravity);
        }
        else if (other.CompareTag(redArea) && color_Change.isRed)
        {
            gravity = highGravity;
            Debug.Log("Gravity still HIGH: " + gravity);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Left gravity zone: " + other.gameObject.tag);
    
        if (other.CompareTag(violetArea) || other.CompareTag(redArea))
        {
            lowGravityZone = false;
            HighGravityZone = false;

            // Reset gravity ONLY if not in another gravity zone
            if (!lowGravityZone && !HighGravityZone)
            {
                gravity = -20f; // Reset to default
                Debug.Log("Gravity reset to NORMAL: " + gravity);
            }
        }
    }

    
}
