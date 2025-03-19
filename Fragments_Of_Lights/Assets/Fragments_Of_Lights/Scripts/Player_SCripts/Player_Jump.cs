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
    public bool isJumping;
    public bool isFalling;


    

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
