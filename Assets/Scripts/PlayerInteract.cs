using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public InputReaders input;
    
    [Header("GRAVITY GUN")]
    [SerializeField] private LayerMask gravityZoneLayer;

    [Header("MOUSE INTERACT")] 
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Image cursorIcon;
    [SerializeField] private Sprite[] cursorSprites;
    public bool isPointingGrabableOrInteractableObject;
    
    private Vector3 dir;
    private Ray ray;
    private RaycastHit hitInfo;
    private Rigidbody rb;

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
        input.GrabItemEvent += GrabItem;
        input.DropItemEvent += DropItem;
    }
    private void FixedUpdate()
    {
        if (!grabbedObject) return;

        Resize();
        HoldingGrabObject();
    }
    private void Update()
    {
        CheckForInteractableOrGrabableObject();
    }

    #region INTERACT OBJECT

    public float originalDistance;
    public float eachScale;
    public Vector3 targetScale;
    public LayerMask ignoreLayerMask;   
    private void GrabItem()
    {
        if (!grabbedObject && isPointingGrabableOrInteractableObject &&
            hitInfo.collider.gameObject.TryGetComponent(out GrabableObject grabableObject))
        {
            grabbedObject = grabableObject;
            grabbedObject.Grab(grabPosition);
            
            originalDistance = Vector3.Distance(Camera.main.transform.position, grabbedObject.transform.position);
            
            eachScale = grabbedObject.transform.localScale.x;
            targetScale = grabbedObject.transform.localScale;
        }
    }

    private void DropItem()
    {
        if (!grabbedObject) return;
            grabbedObject.Drop();
            grabbedObject = null;
    }
    private void Interact()
    {
        if (isPointingGrabableOrInteractableObject && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
        {
            interactableObject.Interact();
        }
    }
    
    private void Resize()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit,
                Mathf.Infinity, ignoreLayerMask))
        {
            Vector3 availablePosition = hit.point;
            while (Physics.CheckSphere(availablePosition, grabbedObject.transform.localScale.x, ignoreLayerMask))
            {
                availablePosition -= Camera.main.transform.forward * Time.deltaTime;
                float z = Vector3.Distance(mainCamera.transform.position, availablePosition);
            
                Vector3 pos = grabbedObject.transform.localPosition;
                grabbedObject.transform.localPosition = new Vector3(
                    Mathf.Lerp(pos.x, 0, Time.deltaTime/7),
                    Mathf.Lerp(pos.y, 0, Time.deltaTime/7),
                    z
                );
            
                targetScale.x = targetScale.y = targetScale.z = z / originalDistance;
                grabbedObject.transform.localScale = targetScale * eachScale; 
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(grabbedObject) Gizmos.DrawSphere(grabbedObject.transform.position, 1f);
    }

    private void HoldingGrabObject()
    {
        if (grabbedObject)
        {
            RotateGrabbedObject();
        }
    }

    private GrabableObject lastPointingObject;
    private void CheckForInteractableOrGrabableObject()
    {
        dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        ray = new Ray(mainCamera.transform.position, dir);
        if (!grabbedObject)
        {
            isPointingGrabableOrInteractableObject = Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer);
        }
        
        if (isPointingGrabableOrInteractableObject && hitInfo.collider.gameObject.TryGetComponent(out GrabableObject pointingObject))
        {
            if (lastPointingObject) lastPointingObject.isPointing = false;
            lastPointingObject = pointingObject;
            pointingObject.isPointing = true;
        }
        else if(lastPointingObject)
        {
            lastPointingObject.isPointing = false;
            lastPointingObject = null;
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
