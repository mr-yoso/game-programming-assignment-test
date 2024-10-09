using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Vector3 playerVelocity;
    Vector3 move;
    Vector3 startingPosition;

    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 2;
    public int maxJumpCount = 1;
    public int jumpCount = 0;
    public float gravity = -9.18f;
    public bool isGrounded;
    public bool isRunning;

    private float powerUpTimer = 0f;
    private float doubleJumpDuration = 30.0f;

    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
    }


    void ProcessMovement()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Turns the player towards the direction he is heading 
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Are you pressing SHIFT to run
        isRunning = Input.GetButton("Run");

        // Handle JUMPING mechanic TODO modify to see what it does.
        HandleJumping();

        // Moves the player
        controller.Move(move * Time.deltaTime * ((isRunning) ? runSpeed : walkSpeed));
    }

    void HandleJumping()
    {
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            jumpCount++;
        }

        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                DisableDoubleJump();
            }
        }
    }

    public void EnableDoubleJump()
    {
        maxJumpCount = 2;
        powerUpTimer = doubleJumpDuration;
        Debug.Log("Double Jump Activated");
    }

    public void DisableDoubleJump()
    {
        maxJumpCount = 1;
        powerUpTimer = 0;
        Debug.Log("Double Jump Deactivated");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            EnableDoubleJump();

            DoubleJumpPowerUpController powerUp = other.GetComponent<DoubleJumpPowerUpController>();

            if (powerUp != null)
            {
                powerUp.StartRespawn();
            }
        }
        else if (other.CompareTag("Collectible"))
        {
            PointsController points = other.GetComponent<PointsController>();

            if (points != null)
            {
                points.Collect();
            }

        }
        else if (other.CompareTag("DeathPlane") || other.CompareTag("TrapPlane"))
        {
            GameManager.instance.ReloadCurrentScene();
            controller.enabled = false;  // Disable the CharacterController to change position
            transform.position = startingPosition;  // Reset position
            controller.enabled = true;   // Re-enable the CharacterController after resetting
        }
    }

    // DONT MODIFY -------------------------------------------------------
    public void Update()
    {
        isGrounded = controller.isGrounded;

        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
        }

        ProcessGravity();
    }

    public void ProcessGravity()
    {

        // Since there is no physics applied on character controller we have this applies to reapply gravity
        if (isGrounded)
        {
            if (playerVelocity.y < 0.0f) // we want to make sure the players stays grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = playerVelocity.y * Time.deltaTime;

        controller.Move(velocity);
    }

    public float GetAnimationSpeed()
    {
        if (isRunning && (move != Vector3.zero))// Left shift
        {
            return 1.0f;
        }
        else if (move != Vector3.zero)
        {
            return 0.5f;
        }
        else
        {
            return 0f;
        }
    }
    // DONT MODIFY -------------------------------------------------------

}
