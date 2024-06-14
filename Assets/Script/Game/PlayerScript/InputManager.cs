using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InputManager : NetworkBehaviour
{
    private PlayerInput inputAction;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMovement motor;
    private PlayerLook look;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private GameObject playerModel;

    public GameObject weapon;

    public override void OnStartLocalPlayer()
    {
        inputAction = new PlayerInput();
        onFoot = inputAction.OnFoot;

        motor = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Sprint.performed += ctx => motor.StartSprint();
        onFoot.Sprint.canceled += ctx => motor.StopSprint();

        onFoot.Crouch.performed += ctx => motor.TriggerCrouch();

        Debug.Log("InputManager started and input actions initialized.");

        cameraHolder.SetActive(true);
        weapon.SetActive(false);
        playerModel.SetActive(false);

        onFoot.Enable();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer) return;

        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
