using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private InputReader input;

    public TextMeshProUGUI speedText;
    [SerializeField] private Transform orientation; 
    private Rigidbody rb;

    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed;
    [SerializeField]private Vector3 moveDirection;
    private float moveInputX;
    private float moveInputY;
    
    [Header("JUMPING")] 
    public bool isJumping;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float airMultiplier;

    [Header("GRAVITY")]
    public bool isGrounded;
    [SerializeField] private float gravityForce;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
    }

    private void Update()
    {
        HandleGravityAndDrags();
        HandleLimitingSpeed();
        speedText.text =rb.velocity.magnitude.ToString();
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * moveInputY;
        moveDirection += orientation.right * moveInputX;
        moveDirection.Normalize();
        moveDirection *= moveSpeed;

        rb.AddForce(moveDirection, ForceMode.Force);
    }

    private void HandleMove(Vector2 moveInputs)
    {
        moveInputX = moveInputs.x;
        moveInputY = moveInputs.y;
    }

    private void HandleJump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            Jump();
            Invoke(nameof(ResetJumping), jumpCoolDown);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJumping()
    {
        isJumping = false;
    }

    private void HandleGravityAndDrags()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.2f, groundLayerMask);

        if (!isGrounded)
        {
            rb.AddForce(-orientation.up * gravityForce);
        }
        
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void HandleLimitingSpeed()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
