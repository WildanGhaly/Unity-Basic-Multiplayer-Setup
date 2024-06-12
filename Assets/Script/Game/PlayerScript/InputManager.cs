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

    private void Awake()
    {

        inputAction = new PlayerInput();
        onFoot = inputAction.OnFoot;

        motor = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Sprint.performed += ctx => motor.StartSprint();
        onFoot.Sprint.canceled += ctx => motor.StopSprint();

        Debug.Log("InputManager started and input actions initialized.");
        
    }

    public override void OnStartLocalPlayer()
    {
        cameraHolder.SetActive(true);
        playerModel.SetActive(false);
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
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
