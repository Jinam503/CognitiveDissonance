using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [Header("GRAVITY GUN")]
    [SerializeField] private LayerMask gravityZoneLayer;
    
    [Header("MOUSE INTERACT")]
    [SerializeField] private float interactRange;
    [SerializeField] private Transform grabPosition;

    private GrabableObject grabbedObject;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        input.MouseRightEvent += CheckForInteractableObject;
        input.MouseLeftUpEvent += PutDownGrabbedObject;
        input.MouseLeftDownEvent += CheckForGrabableObject;
    }

    private void Update()
    {
        if (grabbedObject != null)
        {
            grabbedObject.gameObject.transform.position = grabPosition.position;
        }   
    }

    private void CheckForGrabableObject()
    {
        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        Ray ray = new Ray(mainCamera.transform.position, dir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out IGrabable grabableObject))
            {
                grabbedObject = grabableObject.Grab();
            }
        }
    }

    private void PutDownGrabbedObject()
    {
        if (grabbedObject == null) return;

        grabbedObject.rigidbody.useGravity = true;
        grabbedObject.collider.enabled = true;
        grabbedObject = null;
    }
    private void CheckForInteractableObject()
    {
        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        Ray ray = new Ray(mainCamera.transform.position, dir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
            {
                interactableObject.Interact();
            }
        }
    }
    public GravityZone GetInteractiveGravityZone()
    {
        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, dir, 15f, gravityZoneLayer);

        if (hits.Length <= 0) return null;
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.TryGetComponent(out GravityZone gravityZone))
            {
                if (!gravityZone.isGravityFieldCreated)
                {
                    return gravityZone;
                }
            }
        }

        return null;
    }
}
