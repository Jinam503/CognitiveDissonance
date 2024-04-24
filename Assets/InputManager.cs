using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControl playerControl;

    private GravityMaker gravityMaker;

    [Header("MOVEMENT")]
    public float verticalInput;
    public float horizontalInput;
    private Vector2 movementInput;

    [Header("CAMERA")]
    public float cameraHorizontalInput;
    public float cameraVerticalInput;
    private Vector2 cameraInput;

    [Header("ACTIONS")]
    public bool isMouseLeftButtonDown;
    public bool isMouseRightButtonDown;

    private void Awake()
    {
        gravityMaker = GetComponent<GravityMaker>();
    }
    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControl();

            playerControl.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControl.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControl.PlayerAction.MouseLeft.performed += i => OnMouseLeftButtonDown();
            playerControl.PlayerAction.MouseLeft.canceled += i => OnMouseLeftButtonUp();

            playerControl.PlayerAction.MouseRight.performed += i => OnMouseRightButtonDown();
            playerControl.PlayerAction.MouseRight.canceled += i => OnMouseRightButtonUp();
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
        HandleMouseInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
    private void HandleMouseInput()
    {
        if (isMouseLeftButtonDown)
        {
            gravityMaker.OnMouseLeftButtonStay();   
        }
        if (isMouseRightButtonDown)
        {
            gravityMaker.OnMouseRightButtonStay();
        }
    }

    private void OnMouseLeftButtonDown()
    {
        isMouseLeftButtonDown = true;
        gravityMaker.OnMouseLeftButtonDown();
    }
    private void OnMouseLeftButtonUp()
    {
        isMouseLeftButtonDown = false;
        gravityMaker.OnMouseLeftButtonUp();
    }

    private void OnMouseRightButtonDown()
    {
        isMouseRightButtonDown = true;
        gravityMaker.OnMouseRightButtonDown();
    }
    private void OnMouseRightButtonUp()
    {
        isMouseRightButtonDown = false;
        gravityMaker.OnMouseRightButtonUp();
    }
}
