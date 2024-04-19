using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControl playerControl;

    private AnimatorManager animatorManager;

    private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    private float moveAmount;

    private Vector2 cameraInput;
    public float cameraHorizontalInput;
    public float cameraVerticalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }
    private void OnEnable()
    {
        if(playerControl == null)
        {
            playerControl = new PlayerControl();

            playerControl.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControl.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));
        animatorManager.SetMoveAmount(moveAmount);
    }
}
