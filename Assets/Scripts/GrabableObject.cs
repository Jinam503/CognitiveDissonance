using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public new string name;
    
    protected new Rigidbody rigidbody;
    protected new Collider collider;

    protected Transform grabTransform;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if (grabTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, grabTransform.position, 0.2f);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void Grab(Transform grabTransformP)
    {
        grabTransform = grabTransformP;
        
        gameObject.transform.parent = grabTransform;
        gameObject.transform.localRotation = grabTransform.localRotation;
        
        ChangeLayerRecursively(gameObject.transform, "HoldLayer");
        
        rigidbody.useGravity = false;
        collider.enabled = false;
    }
    public void Drop()
    {
        
        gameObject.transform.parent = null;
        
        ChangeLayerRecursively(gameObject.transform, "Default");
        
        rigidbody.useGravity = true;
        collider.enabled = true;
        
        
        grabTransform = null;
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
