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
    protected Outline outline;
    

    protected Transform grabTransform;

    public bool canDrop;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        outline = GetComponent<Outline>();
    }
    private void Update()
    {
        if (!grabTransform)
        {
            outline.OutlineColor = Color.clear;
            return;
        }

        Vector3 newPosition = Vector3.Lerp(transform.position, grabTransform.position, Time.deltaTime * 30f);
        rigidbody.MovePosition(newPosition);

        Quaternion newRotation = Quaternion.Lerp(transform.rotation, grabTransform.rotation, Time.deltaTime * 30f);
        rigidbody.MoveRotation(newRotation);
        
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        outline.OutlineColor = canDrop ? Color.white : Color.red;
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
        grabTransform = grabTransformP;
        
        ChangeLayerRecursively(gameObject.transform, "HoldLayer");

        rigidbody.useGravity = false;
        collider.isTrigger = true;
        canDrop = true;
    }
    public void Drop()
    {
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
