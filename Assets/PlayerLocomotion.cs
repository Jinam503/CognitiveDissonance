using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController characterController;

    [SerializeField] private float mouseSpeed;

    [SerializeField] private float cameraVerticalAngle;
    [SerializeField] private float cameraHorizontalAngle;

    [SerializeField] private float maxAngle;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotationCamera();
    }
    private void HandleMovement()
    {

    }
    private void HandleRotationCamera()
    {
        cameraHorizontalAngle += (inputManager.cameraHorizontalInput * mouseSpeed * Time.deltaTime);
        cameraVerticalAngle -= (inputManager.cameraVerticalInput * mouseSpeed * Time.deltaTime);
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -maxAngle, maxAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = cameraHorizontalAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation.x = cameraVerticalAngle;
        targetRotation = Quaternion.Euler(rotation);
        Camera.main.transform.rotation = targetRotation;
    }
}
