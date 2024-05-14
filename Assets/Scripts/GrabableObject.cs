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
            transform.position = Vector3.Lerp(transform.position,grabTransform.position, 20f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 5f * Time.deltaTime);
        }

        outline.enabled = isPointing || grabTransform;
    }

    public void Grab(Transform _grabTransformP)
    {
        grabTransform = _grabTransformP;

        rigidbody.isKinematic = true;
    }
    public void Drop()
    {
        rigidbody.isKinematic = false;
        
        grabTransform = null;
    }
}
