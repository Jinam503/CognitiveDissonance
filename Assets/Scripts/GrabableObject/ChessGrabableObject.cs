using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessGrabableObject : GrabableObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TeleportFloor t))
        {
            transform.position += Vector3.up * 15f;
        }
    }
}
