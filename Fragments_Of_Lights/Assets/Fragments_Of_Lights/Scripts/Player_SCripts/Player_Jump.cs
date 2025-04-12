using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Ceiling Check")] 
    public Transform headCheck; 
    public float headCheckDistance = 0.3f;

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
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) //&& currentJumps < maxJumps)
        {
            Debug.Log($"Jump button pressed from: {context.control.device.displayName}");
            HandleJump();
            
        }
    }

    private void Update()
    {
        GroundCheck();
        CheckCeiling(); 
        ApplyGravity();
        
        
        characterController.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (!isJumping && !isFalling)
            {
                SetAnimationStates(true, false, false);
            }
            else if (isFalling) // just landed
            {
                isJumping = false;
                isFalling = false;
                SetAnimationStates(true, false, false);
            }
        }
        else
        {
            if (velocity.y < 0)
            {
                isFalling = true;
                isJumping = false;
                SetAnimationStates(false, false, true);
            }
        }
    }



    private void CheckCeiling()
    {
        if (isJumping)
        {
            RaycastHit hit;
            if (Physics.Raycast(headCheck.position, Vector3.up, out hit, headCheckDistance))
            {
                Debug.Log("Hit ceiling: " + hit.collider.name);
                velocity.y = 0f; // Stop upward movement
                isJumping = false;
                isFalling = true;
                SetAnimationStates(false, false, true); // Switch to falling
            }
        }
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            isJumping = true;
            isFalling = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityMod);
            SetAnimationStates(false, true, false); // Not grounded, jumping, not falling
        }

        // if (!isGrounded && velocity.y < 0 && !isFalling)
        // {
        //     isFalling = true;
        //     isJumping = false;
        //     SetAnimationStates(false, false, true); // Not grounded, not jumping, falling
        // }
    }


    private void ApplyGravity()
    {
        velocity.y += gravityMod * fallModifier * Time.deltaTime;
    }

   private void SetAnimationStates(bool grounded, bool jumping, bool falling)
    {
        Debug.Log($"Animation State -> Grounded: {grounded}, Jumping: {jumping}, Falling: {falling}");
        playerAnim.SetBool("Is_Grounded", grounded);
        playerAnim.SetBool("Is_Jumping", jumping);
        playerAnim.SetBool("Is_Falling", falling);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(VIOLET_AREA) && color_Change.isViolet)
        {
            lowGravityZone = true;
            gravityMod = lowGravity;
        }
        else if (other.CompareTag(RED_AREA) && color_Change.isRed)
        {
            highGravityZone = true;
            gravityMod = highGravity;
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
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }

        if (headCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(headCheck.position, Vector3.up * headCheckDistance); // Visualize ceiling check
        }
    }
}
