using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStartButton : Button, IInteractable
{
    [SerializeField] private MeshCollider floor;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject introduceText;
    public void Interact()
    {
        PushButton();

        floor.convex = true;
        floor.gameObject.layer = LayerMask.NameToLayer("Default");
        floor.isTrigger = true;
        startText.SetActive(false);
        introduceText.SetActive(false);
        gameObject.SetActive(false);
    }
}
