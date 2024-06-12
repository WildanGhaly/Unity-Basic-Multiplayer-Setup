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

    private bool isRunning;

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

        if (isRunning)
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

    private void UpdateMovementAnimation(int id)
    {
        playerAnimator.SetBool("IsWalkForward", false);
        playerAnimator.SetBool("IsSprinting", false);
        playerAnimator.SetBool("IsWalkLeft", false);
        playerAnimator.SetBool("IsWalkRight", false);
        playerAnimator.SetBool("IsWalkBackward", false);

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
        }

    }
}
