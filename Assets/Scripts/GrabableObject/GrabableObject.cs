using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public new string name;
    public bool isParallel;

    [HideInInspector] public bool isPointing;
    
    [Header("PHYSICS")]
    private new Rigidbody rigidbody;
    private new Collider collider;

    [Header("GRAB")]
    public bool isGrabbed;
    protected Transform grabTransform;
    protected Outline outline;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
    }

    protected virtual void Update()
    {
        if (isParallel)
        {
            Quaternion worldRotation = transform.rotation;

            Vector3 worldEulerAngles = worldRotation.eulerAngles;

            worldEulerAngles.x = 0;
            worldEulerAngles.z = 0;

            Quaternion targetRotation = Quaternion.Euler(worldEulerAngles);

            transform.rotation = targetRotation;
        }
        outline.enabled = isPointing || isGrabbed;
    }

    public void Grab(Transform getGrabTransform)
    {
        isGrabbed = true;
        
        grabTransform = getGrabTransform;
        transform.parent = getGrabTransform;

        rigidbody.useGravity = false;
        gameObject.layer = LayerMask.NameToLayer("HoldLayer");
    }
    public void Drop()
    {
        isGrabbed = false;
        
        grabTransform = null;
        transform.parent = null;
        
        rigidbody.useGravity = true;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
