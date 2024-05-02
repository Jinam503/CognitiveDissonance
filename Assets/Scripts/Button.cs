using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{ 
    protected Coroutine pushButtonCoroutine;
    protected bool isPushed;

    protected void PushButton()
    {
        if (isPushed) return;
        
        if (pushButtonCoroutine != null)
        {
            StopCoroutine(pushButtonCoroutine);
        }
        pushButtonCoroutine = StartCoroutine(CPushButton());
        Debug.Log("GameStart");
    }

    private IEnumerator CPushButton()
    {
        //  Hard Coding.....................
        isPushed = true;
        transform.localPosition = new Vector3(0, 0, 0.01f);
        yield return new WaitForSeconds(1f);
        isPushed = false;
        transform.localPosition = new Vector3(0, 0, 0.03f);
    }
}
