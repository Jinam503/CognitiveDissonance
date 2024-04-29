using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroducePlank : MonoBehaviour, IGrabable
{
    private Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public GameObject Grab()
    {
        collider.enabled = false;
        
        return gameObject;
    }
}
