using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [Header("GRAVITY GUN")]
    [SerializeField] private LayerMask gravityZoneLayer;

    [Header("MOUSE INTERACT")] 
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Image cursorIcon;
    [SerializeField] private Sprite[] cursorSprites;
    private bool isPointingGrabableOrInteractableObject;

    private Vector3 dir;
    private Ray ray;
    private RaycastHit hitInfo;

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
        input.MouseRightEvent += GrabOrDropObject;

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
    private void FixedUpdate()
    {
        HoldingGrabObject();
    }
    private void Update()
    {
        CheckForInteractableOrGrabableObject();
    }

    private void HoldingGrabObject()
    {
        if (grabbedObject)
        {
            RotateGrabbedObject();
        }
    }

    #region INTERACT OBJECT

    private void CheckForInteractableOrGrabableObject()
    {
        dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        ray = new Ray(mainCamera.transform.position, dir);
        isPointingGrabableOrInteractableObject = Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer);
    }
    private void GrabOrDropObject()
    {
        if (grabbedObject)
        {
            if (!grabbedObject.canDrop)
            {
                Debug.Log("여기 못 놓음..");
                return;
            }
            grabbedObject.Drop();
            grabbedObject = null;
            
            return;
        }
        
        dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        ray = new Ray(mainCamera.transform.position, dir);
        if (Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out GrabableObject grabableObject))
            {
                grabPosition.rotation = grabableObject.transform.rotation;
                
                grabbedObject = grabableObject;
                grabbedObject.Grab(grabPosition);
            }
            else if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
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
