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

    private void Awake()
    {
        inputAction = new PlayerInput();
        onFoot = inputAction.OnFoot;

        motor = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        onFoot.Enable();
    }

    public override void OnStartLocalPlayer()
    {
        onFoot.Jump.performed += ctx => motor.CmdJump();

        onFoot.Sprint.performed += ctx => motor.CmdStartSprint();
        onFoot.Sprint.canceled += ctx => motor.CmdStopSprint();

        onFoot.Crouch.performed += ctx => motor.CmdTriggerCrouch();

        Debug.Log("InputManager started and input actions initialized.");

        cameraHolder.SetActive(true);
        weapon.SetActive(false);
        // playerModel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        motor.CmdProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer) return;

        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
