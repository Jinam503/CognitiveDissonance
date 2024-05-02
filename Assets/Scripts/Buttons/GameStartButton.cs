using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : Button, IInteractable
{
    public void Interact()
    {
        PushButton();
        
        Debug.Log("GameStart");
    }
}
