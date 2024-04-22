using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoveAmount(float moveAmount)
    {
        animator.SetFloat("MoveAmount", moveAmount);
    }
}
