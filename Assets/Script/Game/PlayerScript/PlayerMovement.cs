using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    private float speed;
    
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float crouchSpeed = 3f;

    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;

    private bool isGrounded;
    private Vector3 playerVelocity;
    private Transform t;
    private Animator playerAnimator;

    private bool isRunning;
    private bool isCrouching;

    private void Awake()
    {
        speed = normalSpeed;
        t = transform;
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        if (!isLocalPlayer) return;

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(speed * Time.deltaTime * t.TransformDirection(moveDirection));

        if (isCrouching && moveDirection != Vector3.zero)
        {
            UpdateMovementAnimation(6);
        }
        else if (isRunning && moveDirection.z > 0)
        {
            UpdateMovementAnimation(2);
        }
        else if (moveDirection.z > 0)
        {
            UpdateMovementAnimation(1);
        }
        else if (moveDirection.z < 0)
        {
            UpdateMovementAnimation(5);
        }
        else if (moveDirection.x < 0)
        {
            UpdateMovementAnimation(3);
        }
        else if (moveDirection.x > 0)
        {
            UpdateMovementAnimation(4);
        }
        else
        {
            UpdateMovementAnimation(0);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (!isLocalPlayer) return;
        Debug.Log("Jump pressed");
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            if (isRunning) playerAnimator.SetTrigger("TriggerJumpRun");
            else playerAnimator.SetTrigger("TriggerJump");
        }
    }

    public void StartSprint()
    {
        if (!isLocalPlayer) return;

        isRunning = true;
        speed = sprintSpeed;
    }

    public void StopSprint()
    {
        if (!isLocalPlayer) return;

        isRunning = false;
        speed = normalSpeed;
    }

    public void TriggerCrouch()
    {
        if (!isLocalPlayer) return;

        // Disable crouch for now
        /*
        if (isCrouching) StopCrouch(); 
        else StartCrouch();
        */
    }

    private void StartCrouch()
    {
        isCrouching = true;
        speed = crouchSpeed;
        playerAnimator.SetTrigger("TriggerCrouch");
    }

    private void StopCrouch()
    {
        isCrouching = false;
        speed = normalSpeed;
        playerAnimator.SetTrigger("TriggerCrouchToStand");
    }

    private void UpdateMovementAnimation(int id)
    {
        playerAnimator.SetBool("IsWalkForward", false);
        playerAnimator.SetBool("IsSprinting", false);
        playerAnimator.SetBool("IsWalkLeft", false);
        playerAnimator.SetBool("IsWalkRight", false);
        playerAnimator.SetBool("IsWalkBackward", false);
        playerAnimator.SetBool("IsCrouchWalking", false);

        switch (id)
        {
            case 1:
                playerAnimator.SetBool("IsWalkForward", true);
                return;
            case 2:
                playerAnimator.SetBool("IsWalkForward", true);
                playerAnimator.SetBool("IsSprinting", true);
                return;
            case 3:
                playerAnimator.SetBool("IsWalkLeft", true);
                return;
            case 4:
                playerAnimator.SetBool("IsWalkRight", true);
                return;
            case 5:
                playerAnimator.SetBool("IsWalkBackward", true);
                return;
            case 6:
                playerAnimator.SetBool("IsCrouchWalking", true);
                return;
        }

    }
}
