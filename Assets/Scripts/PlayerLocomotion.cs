using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterController cC;
    [SerializeField] private InputReader input;
    
    [SerializeField] private Transform orientation; 

    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    private float moveInputX;
    private float moveInputY;
    
    [Header("JUMPING")] 
    public bool isJumping;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float airMultiplier;

    [Header("GRAVITY")]
    public bool isGrounded;
    private readonly float gravity = -9.8f;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cC = GetComponent<CharacterController>();
    }
    private void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
    }
    private void Update()
    {
        ApplyGravity();
        Move();
    }

    private void HandleMove(Vector2 moveInputs)
    {
        moveInputX = moveInputs.x;
        moveInputY = moveInputs.y;
        
        moveDirection = orientation.forward * moveInputY;
        moveDirection += orientation.right * moveInputX;
        moveDirection.Normalize();
        moveDirection *= moveSpeed * Time.deltaTime;
    }

    private void ApplyGravity()
    {
        moveDirection.y = gravity * gravityMultiplier * Time.deltaTime;
    }
    private void Move()
    {
        cC.Move(moveDirection);
    }

    private void HandleJump()
    {
    }
    
}
