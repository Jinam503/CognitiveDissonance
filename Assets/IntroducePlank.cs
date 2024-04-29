using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroducePlank : MonoBehaviour, IGrabable
{
    private new Collider collider;
    private new Rigidbody rigidbody;
    private void Awake()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public GrabableObject Grab()
    {
        GrabableObject grabableObject = new GrabableObject("Introduce Plank", gameObject, collider, rigidbody);
        collider.enabled = false;
        rigidbody.useGravity = false;   
        
        return grabableObject;
    }
}
