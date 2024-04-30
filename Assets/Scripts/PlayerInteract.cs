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

    [Header("GRAB OBJECT")]
    [SerializeField] private Transform grabPosition;
    [SerializeField] private float rotationSensitivity;
    private GrabableObject grabbedObject;
    private bool isRotatingX;
    private bool isRotatingY;
    
    
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

        input.StartRotatingGrabObjectByXaxis +=  () => {
            if (grabbedObject != null) {
                isRotatingX = true;
            }
        };
        input.StopRotatingGrabObjectByXaxis +=  () => {
            if (grabbedObject != null) {
                isRotatingX = false;
            }
        };
        input.StartRotatingGrabObjectByYaxis +=  () => {
            if (grabbedObject != null) {
                isRotatingY = true;
            }
        };
        input.StopRotatingGrabObjectByYaxis +=  () => {
            if (grabbedObject != null) {
                isRotatingY = false;
            }
        };
    }
    private void Update()
    {
        HoldingGrabObject();
    }
    
    private void HoldingGrabObject()
    {
        if (grabbedObject != null)
        {
            RotateGrabbedObject();
        }
    }

    #region GRAB OBJECT

    private void CheckForGrabableObject()
    {
        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        Ray ray = new Ray(mainCamera.transform.position, dir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out GrabableObject grabableObject))
            {
                grabPosition.localRotation = Quaternion.identity;
                
                grabbedObject = grabableObject;
                grabbedObject.Grab(grabPosition);
            }
        }
    }
    private void PutDownGrabbedObject()
    {
        if (grabbedObject == null) return;

        
        grabbedObject.Drop();
        
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
    private void RotateGrabbedObject()
    {
        if (isRotatingX)
        {
            grabPosition.Rotate(Vector3.down, rotationSensitivity);
        }
        if (isRotatingY)
        {
            grabPosition.Rotate(Vector3.right, rotationSensitivity);
        }
    }
    
    #endregion

    #region GRAVITY ZONE

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

    #endregion
    
}
