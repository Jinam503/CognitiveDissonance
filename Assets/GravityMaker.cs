using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravityMaker : MonoBehaviour
{
    private PlayerInteract playerInteract;


    [Header("CREATE GRAVITY ZONE")]
    [SerializeField] private GameObject gravityZonePrefab;
    private Vector3 initialGravityFieldPosition;
    private Vector3 initialGravityFieldNormal;
    private Vector3 gravityZoneVector;
    private Transform childGravityZoneTransform;
    private Transform gravityZoneTransform;

    //[Header("MODIFY GRAVITY ZONE")]
    private GravityZone modifyingGravityZone;
    private Transform modifyingGravityZoneTransform;

    private void Awake()
    {
        playerInteract = GetComponent<PlayerInteract>();

        childGravityZoneTransform = null;
        gravityZoneTransform = null;
    }

    public void OnMouseLeftButtonDown()
    {
        (initialGravityFieldPosition, initialGravityFieldNormal) = Mouse3D.GetMousePositionAndNormal();
        gravityZoneTransform = Instantiate(gravityZonePrefab, initialGravityFieldPosition, Quaternion.LookRotation(initialGravityFieldNormal)).transform;
        childGravityZoneTransform = gravityZoneTransform.GetChild(0);

        gravityZoneVector = gravityZoneTransform.position;
        gravityZoneTransform.localScale = new Vector3(1, 1, 0.0001f);
    }
    public void OnMouseLeftButtonStay()
    {
        if (gravityZoneTransform == null) return;

        Vector3 curPoint = Mouse3D.GetMousePosition();
        Vector3 distanceVector = curPoint - gravityZoneVector;

        float scaleX = Vector3.Dot(distanceVector, gravityZoneTransform.right);
        float scaleY = Vector3.Dot(distanceVector, gravityZoneTransform.up);
        float scaleZ = 0.01f;

        Vector3 gravityZoneLocalScale = new Vector3(scaleX, scaleY, scaleZ);
        gravityZoneTransform.localScale = gravityZoneLocalScale;

        childGravityZoneTransform.localScale = ConvertVectorToOne(gravityZoneLocalScale);
    }
    public void OnMouseLeftButtonUp()
    {
        if(gravityZoneTransform == null) return;

        GravityZone gravityZone = childGravityZoneTransform.AddComponent<GravityZone>();

        BoxCollider boxCollider = childGravityZoneTransform.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    public void OnMouseRightButtonDown()
    {
        modifyingGravityZone = playerInteract.GetInteractiveGravityZone();
    }
    public void OnMouseRightButtonStay()
    {
        if (modifyingGravityZone == null) return;

        Vector3 curPoint = Mouse3D.GetMousePosition();
        Vector3 distanceVector = curPoint - gravityZoneVector;

        float scaleX = Vector3.Dot(distanceVector, gravityZoneTransform.right);
        float scaleY = Vector3.Dot(distanceVector, gravityZoneTransform.up);
        float scaleZ = 0.01f;

        Vector3 gravityZoneLocalScale = new Vector3(scaleX, scaleY, scaleZ);
        modifyingGravityZoneTransform.localScale = gravityZoneLocalScale;

        childGravityZoneTransform.localScale = ConvertVectorToOne(gravityZoneLocalScale);
    }
    public void OnMouseRightButtonUp()
    {

    }
    Vector3 ConvertVectorToOne(Vector3 inputVector)
    {
        return new Vector3(
            Mathf.Sign(inputVector.x),
            Mathf.Sign(inputVector.y),
            Mathf.Sign(inputVector.z)
        );
    }

    
}
