using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject gravityZoneInteractUIContainer;
    [SerializeField] private PlayerInteract playerInteract;

    private void Update()
    {
        if(playerInteract.GetInteractiveGravityZone() != null)
        {
            ShowGravityZoneInteractUIContainer();
        }
        else
        {
            HideGravityZoneInteractUIContainer();
        }
    }

    private void HideGravityZoneInteractUIContainer()
    {
        gravityZoneInteractUIContainer.SetActive(false);
    }

    private void ShowGravityZoneInteractUIContainer()
    {
        gravityZoneInteractUIContainer.SetActive(true);
    }
}
