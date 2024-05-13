	using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
	
public class InputReader : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool grab;
	public bool interact;
	
	[Header("Movement Settings")]
	public bool analogMovement;
	
	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue value)
	{ 
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{ 
		if(cursorInputForLook)
		{ 
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnGrab(InputValue value)
	{
		GrabInput(value.isPressed);
	}

	public void OnInteract(InputValue value)
	{
		InteractInput(value.isPressed);
		Debug.Log("우클릭");
	}
#endif


	private void MoveInput(Vector2 newMoveDirection)
	{ 
		move = newMoveDirection;
	}

	private void LookInput(Vector2 newLookDirection)
	{ 
		look = newLookDirection;
	}

	private void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	private void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

	private void GrabInput(bool newGrabState)
	{
		grab = newGrabState;
	}

	private void InteractInput(bool newInteractState)
	{
		interact = newInteractState;
	}
	private void OnApplicationFocus(bool hasFocus)
	{ 
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
	
