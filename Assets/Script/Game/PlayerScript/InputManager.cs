using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputManager : NetworkBehaviour
{
    private PlayerInput inputAction;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMovement motor;
    private PlayerLook look;

    [SerializeField] private GameObject cameraHolder;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            cameraHolder.SetActive(false);
            return;
        }
        inputAction = new PlayerInput();
        onFoot = inputAction.OnFoot;

        motor = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        Debug.Log("InputManager started and input actions initialized.");

        onFoot.Enable();
    }

    public override void OnNetworkDespawn()
    {
        if (IsOwner)
        {
            onFoot.Disable();
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        if (!IsOwner) return;
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}