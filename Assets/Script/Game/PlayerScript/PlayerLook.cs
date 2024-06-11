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
        if (!isLocalPlayer) return;

        float vertical = ySensitivity * input.y * Time.deltaTime;
        float horizontal = xSensitivity * input.x * Time.deltaTime;

        tilt -= vertical;
        tilt = Mathf.Clamp(tilt, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);

        transform.Rotate(Vector3.up * horizontal);
    }
}
