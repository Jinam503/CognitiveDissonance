using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float gravityForce;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("ACTIONS")]
    public bool isJumping;
    public float jumpForce;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleGravity();

        HandleRotationCamera();
        HandleMovement();

    }
    float inAirTimer;
    private void HandleGravity()
    {
        RaycastHit hit;

        if (!isGrounded)
        {
            Debug.Log("DDDDD");
            inAirTimer += Time.deltaTime;
            rb.AddForce(-transform.up * gravityForce * inAirTimer);
        }

        if(Physics.CheckSphere(groundChecker.position, 0.2f, groundLayerMask))
        {
            inAirTimer = 0f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void HandleMovement()
    {
        moveDirection = cameraPivot.forward * inputManager.verticalInput;
        moveDirection += cameraPivot.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.x = 0;
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

    public void HandleJumping()
    {
        if (isGrounded)
        {
            moveDirection.x = jumpForce;
            isJumping = true;
        }
    }
}
