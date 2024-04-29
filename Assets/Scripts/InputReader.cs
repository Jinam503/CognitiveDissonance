using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions
{
    private GameInput gameInput;
    
    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            
            gameInput.GamePlay.SetCallbacks(this);

            SetGamePlay();
        }
    }

    public void SetGamePlay()
    {
        gameInput.Enable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;

    public event Action<Vector2> CameraRotateEvent;
    

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        CameraRotateEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }
}
