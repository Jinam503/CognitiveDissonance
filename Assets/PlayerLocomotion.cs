using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody rb;

    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSpeed;

    [SerializeField] private float cameraVerticalAngle;
    [SerializeField] private float cameraHorizontalAngle;

    [SerializeField] private float maxAngle;
    [SerializeField] private Transform cameraPivot;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotationCamera();
    }
    private void HandleMovement()
    {
        moveDirection = cameraPivot.forward * inputManager.verticalInput;
        moveDirection += cameraPivot.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.x = 0;
        moveDirection *= moveSpeed  ;
        rb.velocity = moveDirection;
    }
    private void HandleRotationCamera()
    {
        cameraHorizontalAngle += (inputManager.cameraHorizontalInput * mouseSpeed * Time.deltaTime);
        cameraVerticalAngle -= (inputManager.cameraVerticalInput * mouseSpeed * Time.deltaTime);
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -maxAngle, maxAngle);

        Vector3 rotation = new Vector3(0, 0, -90);
        rotation.x = cameraHorizontalAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = new Vector3(0, 0, 0);
        rotation.x = cameraVerticalAngle;
        targetRotation = Quaternion.Euler(rotation);
        Camera.main.transform.localRotation = targetRotation;
    }
}
