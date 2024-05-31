using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerTeleportManager : MonoBehaviour
{
    public static PlayerTeleportManager instance;
    public FirstPersonController player;
    private void Awake()
    {
        instance = this;
    }

    public void MovePlayerToPosition(Vector3 position, Vector3 rotation)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = position;
        player.transform.rotation = Quaternion.Euler(rotation);
        
        player.GetComponent<CharacterController>().enabled = true;
    }
}
