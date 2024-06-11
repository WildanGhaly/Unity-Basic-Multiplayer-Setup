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

    private void Awake()
    {
        t = transform;
        controller = GetComponent<CharacterController>();
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
        }
    }
}
