using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
    

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerLocomotion = GetComponent<PlayerLocomotion>();
    }
}
