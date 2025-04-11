using System.Collections;
using System.Collections.Generic;
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
            AudioManager.instance.PlayClip(AudioManager.instance.jumpAudio, false, 1f);
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

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

    
}

