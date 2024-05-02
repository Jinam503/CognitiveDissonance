using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStartButton : Button, IInteractable
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject introduceText;
    public void Interact()
    {
        PushButton();
        
        floor.SetActive(false);
        startText.SetActive(false);
        introduceText.SetActive(false);
        gameObject.SetActive(false);
    }
}
