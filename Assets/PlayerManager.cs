using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private InputManager inputManager;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
    }
    void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
}
