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
    private new Collider collider;

    public bool isPointing;
    
    protected Transform grabTransform;
    protected Outline outline;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (grabTransform)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, 5f * Time.deltaTime);
        }

        outline.enabled = isPointing || grabTransform;
    }

    public void Grab(Transform getGrabTransform)
    {
        grabTransform = getGrabTransform;
        transform.parent = getGrabTransform;
        
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }
    public void Drop()
    {
        transform.parent = null;
        
        rigidbody.isKinematic = false;
        collider.enabled = true;
        
        grabTransform = null;
    }
}
