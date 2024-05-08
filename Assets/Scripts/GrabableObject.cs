using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public new string name;

    private new Rigidbody rigidbody;
    protected new Collider collider;
    

    protected Transform grabTransform;

    public bool canDrop;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        canDrop = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canDrop = true;
    }

    public void Grab(Transform grabTransformP)
    {
        transform.parent = grabTransformP;
        grabTransform = grabTransformP;
        
        ChangeLayerRecursively(gameObject.transform, "HoldLayer");

        rigidbody.useGravity = false;
        collider.isTrigger = true;
        
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        canDrop = true;
    }
    public void Drop()
    {
        transform.parent = null;
        ChangeLayerRecursively(gameObject.transform, "Interactable");
        
        grabTransform = null;
        rigidbody.useGravity = true;
        collider.isTrigger = false;
    }
    private void ChangeLayerRecursively(Transform parent, string layerName)
    {
        parent.gameObject.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in parent)
        {
            ChangeLayerRecursively(child, layerName);
        }
    }
}
