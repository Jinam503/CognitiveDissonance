using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFloor : MonoBehaviour
{
    public Transform teleportTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerTeleportManager.instance.MovePlayerToPosition(teleportTransform.position, teleportTransform.eulerAngles);
        }
    }
}
