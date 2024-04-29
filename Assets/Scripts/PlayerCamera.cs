using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [SerializeField] private Transform orientation;

    [SerializeField] private float mouseSensitivityX;
    [SerializeField] private float mouseSensitivityY;

    private float xRotation;
    private float yRotation;

    private float cameraMoveInputY;
    private float cameraMoveInputX;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input.CameraRotateEvent += HandleCamera;
    }

    private void Update()
    {
        float mouseX = cameraMoveInputX * Time.deltaTime * mouseSensitivityX;
        float mouseY = cameraMoveInputY * Time.deltaTime * mouseSensitivityY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void HandleCamera(Vector2 cameraMoveInputs)
    {
        cameraMoveInputX = cameraMoveInputs.x;
        cameraMoveInputY = cameraMoveInputs.y;
    }
}
