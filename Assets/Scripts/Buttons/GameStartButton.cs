using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : Button, IInteractable
{
    public void Interact()
    {
        if (isPushed) return;
        
        if (pushButtonCoroutine != null)
        {
            StopCoroutine(pushButtonCoroutine);
        }
        pushButtonCoroutine = StartCoroutine(PushButton());
        Debug.Log("GameStart");
    }
}
