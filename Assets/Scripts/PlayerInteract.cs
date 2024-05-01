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
    [SerializeField] private LayerMask interactableLayer;

    private Vector3 dir;
    private Ray ray;
    private RaycastHit hitInfo;
    private Outline pointingObjectOutline;

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
        if (Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer)) // 레이캐스트를 쏘고
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Outline outline)) // 만약 OutLine이 있다면
            {
                if (outline.enabled) return;//OutLine이 켜져있으면 Return하고
                
                outline.enabled = true;//꺼져있다면 켜준다.
                pointingObjectOutline = outline; // 나중에 꺼주기 위해Outline저장
            }
            else                               //OutLine이 없다면 
            {
                if (pointingObjectOutline) // 꺼야할 outLine이 있다면
                {
                    pointingObjectOutline.enabled = false; //꺼주고
                    pointingObjectOutline = null;//null로 변경
                }
            }
        }
        else // 레이캐스트를 쐈는데 아무것도 없다면 
        {
            if (pointingObjectOutline) // 꺼야할 OutLine이 있다면
            {
                pointingObjectOutline.enabled = false;
                pointingObjectOutline = null;
            }

        }
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
        {if (hitInfo.collider.gameObject.TryGetComponent(out GrabableObject grabableObject))
            {
                grabPosition.localRotation = Quaternion.identity;
                
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
