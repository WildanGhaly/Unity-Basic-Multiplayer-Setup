using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;

    private bool isGrounded;
    private Vector3 playerVelocity;
    private Transform t;
    private Animator playerAnimator;

    private bool
        isStrafingRight,
        isStrafingLeft,
        isWalkingBack,
        isWalkingForward,
        isIdle,
        isRunning,
        isRunningTriggered;

    private void Awake()
    {
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

        if (moveDirection.x < 0 && !isWalkingBack)
        {
            isStrafingRight = false;
            isStrafingLeft = false;
            isWalkingBack = true;
            isWalkingForward = false;
            isIdle = false;
            isRunning = false;
            isRunningTriggered = false;
            playerAnimator.SetTrigger("TriggerRunningBack");
        }
        else if (moveDirection.x == 0 && moveDirection.z > 0 && !isStrafingRight)
        {
            isStrafingRight = true;
            isStrafingLeft = false;
            isWalkingBack = false;
            isWalkingForward = false;
            isIdle = false;
            isRunning = false;
            isRunningTriggered = false;
            playerAnimator.SetTrigger("TriggerWalkingRight");
        }
        else if (moveDirection.x == 0 && moveDirection.z < 0 && !isStrafingLeft)
        {
            isStrafingRight = false;
            isStrafingLeft = true;
            isWalkingBack = false;
            isWalkingForward = false;
            isIdle = false;
            isRunning = false;
            isRunningTriggered = false;
            playerAnimator.SetTrigger("TriggerWalkingLeft");
        }
        else if (moveDirection.x > 0 && !isWalkingForward && !isRunning)
        {
            isStrafingRight = false;
            isStrafingLeft = false;
            isWalkingBack = false;
            isWalkingForward = true;
            isIdle = false;
            isRunning = false;
            isRunningTriggered = false;
            playerAnimator.SetTrigger("TriggerWalking");
        }
        else if (isRunning && !isRunningTriggered)
        {
            isStrafingRight = false;
            isStrafingLeft = false;
            isWalkingBack = false;
            isWalkingForward = false;
            isIdle = false;
            isRunning = false;
            isRunningTriggered = true;
            isRunningTriggered = false;
            playerAnimator.SetTrigger("TriggerRunning");
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

        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            if (isRunning) playerAnimator.SetTrigger("TriggerJumpRun");
            else playerAnimator.SetTrigger("TriggerJump");
        }
    }
}
