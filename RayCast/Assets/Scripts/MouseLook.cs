using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float Sensivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] Joystick lookJoystick;
    float xRotation = 0f;

    private void Update()
    {
        float MouseX = lookJoystick.Horizontal * Sensivity * Time.deltaTime;

        float MouseY = lookJoystick.Vertical * Sensivity * Time.deltaTime;

        xRotation -= MouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);
    }
}
