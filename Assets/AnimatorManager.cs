using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;

    public float moveAmount;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    public void SetMoveAmount(float moveAmount)
    {
        animator.SetFloat("MoveAmount", moveAmount);
    }
}
