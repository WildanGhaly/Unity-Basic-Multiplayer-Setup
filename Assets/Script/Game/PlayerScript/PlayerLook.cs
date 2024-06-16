using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;
    
    public Camera cam;

    private float tilt = 0f;

    public void ProcessLook(Vector2 input)
    {
        float vertical = ySensitivity * input.y * Time.deltaTime;

        tilt -= vertical;
        tilt = Mathf.Clamp(tilt, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);

        CmdHorizontalLook(input.x);
    }

    [Command]
    private void CmdHorizontalLook(float x)
    {
        float horizontal = xSensitivity * x * Time.deltaTime;
        transform.Rotate(Vector3.up * horizontal);
    }
}
