using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody rb;

    [Header("MOVEMENT")]
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveSpeed;

    [Header("CAMERA")]
    [SerializeField] private float mouseSpeed;
    [SerializeField] private float cameraVerticalAngle;
    [SerializeField] private float cameraHorizontalAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private Transform cameraPivot;

    [Header("GRAVITY")]
    public bool isGrounded;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float gravityForce;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private LayerMask groundLayerMask;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        ApplyGravity();
        HandleMovement();
        HandleRotationCamera();
    }
    private void HandleMovement()
    {
        moveDirection = cameraPivot.forward * inputManager.verticalInput;
        moveDirection += cameraPivot.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
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

    float inAirTimer;
    private void ApplyGravity()
    {
        isGrounded = Physics.SphereCast(groundChecker.position, 0.2f, -groundChecker.up, out RaycastHit hit, 0.2f, groundLayerMask);

        if (!isGrounded)
        {
            inAirTimer += Time.deltaTime;
            rb.AddForce(-playerTransform.up * gravityForce * gravityMultiplier * inAirTimer);
        }
        else
        {
            inAirTimer = 0;
        }
    }
}
