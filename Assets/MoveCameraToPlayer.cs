using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveCameraToPlayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private void Update()
    {
        transform.position = cameraTransform.position;
    }
}
