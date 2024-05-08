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
    private CharacterController cC;
    [SerializeField] private InputReader input;
    
    [Header("SPHERE CAST")]
    [SerializeField] private float sphereCastRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask groundLayer;
    
    
    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed;
    private float moveInputX;
    private float moveInputY;
    
    [Header("JUMPING")] 
    public bool jumpFlag;
    [SerializeField] private float jumpForce;

    [Header("GRAVITY")]
    [SerializeField] private float gravityMultiplier;
    
    public Vector3 velocity;
    public Vector3 lastFixedPosition;
    public Vector3 nextFixedPosition;
    private void Awake()
    {
        cC = GetComponent<CharacterController>();
    }
    private void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;

        velocity.y = 0f;
    }
    private void Update()
    {
        Debug.Log(IsGrounded());
        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Vector3 dir = Vector3.Lerp(lastFixedPosition, nextFixedPosition, interpolationAlpha) - transform.position;
        
        cC.Move(dir);
    }
    private void FixedUpdate()
    {
        lastFixedPosition = nextFixedPosition;

        Vector3 planeVelocity = GetXZVelocity(moveInputX, moveInputY);
        float yVelocity = GetYVelocity();
        velocity = new Vector3(planeVelocity.x, yVelocity, planeVelocity.z);
        
        nextFixedPosition += velocity * Time.fixedDeltaTime;
    }
    private void HandleMove(Vector2 moveInputs)
    {
        moveInputX = moveInputs.x;
        moveInputY = moveInputs.y;
    }
    private void HandleJump()
    {
        jumpFlag = true;
    }
    private Vector3 GetXZVelocity(float horizontalInput, float verticalInput)
    {
        Vector3 moveVelocity = Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput;
        Vector3 moveDirection = moveVelocity.normalized;
        float _moveSpeed = Mathf.Min(moveVelocity.magnitude, 1.0f) * moveSpeed;

        return moveDirection * _moveSpeed;
    }
    private float GetYVelocity()
    {
        if (!IsGrounded())
        {
            return velocity.y - gravityMultiplier * Time.fixedDeltaTime;
        }

        if (jumpFlag)
        {
            Debug.Log("jump");
            jumpFlag = false;
            return velocity.y + jumpForce;
        }
        return Mathf.Max(0.0f, velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics.SphereCast(transform.position, sphereCastRadius,-transform.up, out RaycastHit hit, maxDistance, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position - transform.up * maxDistance, sphereCastRadius);
    }
}
