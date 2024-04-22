using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControl playerControl;

    private AnimatorManager animatorManager;

    [Header("MOVEMENT")]
    public float verticalInput;
    public float horizontalInput;
    private Vector2 movementInput;
    private float moveAmount;
    
    [Header("CAMERA")]
    public float cameraHorizontalInput;
    public float cameraVerticalInput;
    private Vector2 cameraInput;

    [Header("ACTIONS")]
    public bool mouseLeft;


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

            playerControl.PlayerAction.MouseLeft.performed += i => mouseLeft = true;
            playerControl.PlayerAction.MouseLeft.canceled += i => mouseLeft = false;
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
