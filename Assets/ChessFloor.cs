using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class ChessFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out GrabableObject grabableObject))
        {
            if (grabableObject.name == "Chess" && !grabableObject.isGrabbed)
            {
                gameObject.layer = LayerMask.NameToLayer("Map");
                Debug.Log("enter");
            }
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.transform.TryGetComponent(out GrabableObject grabableObject))
        {
            if (grabableObject.name == "Chess")
            {
                Debug.Log("sttay");
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.TryGetComponent(out GrabableObject grabableObject))
        {
            if (grabableObject.name == "Chess")
            {
                gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
                Debug.Log("exut");
            }
        }
    }
}
