using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

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
        initialGravityFieldPosition = SnapVectorToVectex(initialGravityFieldPosition);

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

        Vector3 gravityZoneLocalScale = SnapVectorToVectex(new Vector3(scaleX, scaleY, 0), 0.02f);
        gravityZoneTransform.localScale = gravityZoneLocalScale;

        childGravityZoneTransform.localScale = ConvertVectorToOne(gravityZoneLocalScale);
    }
    public void OnMouseLeftButtonUp()
    {
        if(gravityZoneTransform == null) return;

        GravityZone gravityZone = childGravityZoneTransform.AddComponent<GravityZone>();
        gravityZone.parentTransform = gravityZoneTransform;

        BoxCollider boxCollider = childGravityZoneTransform.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    public void OnMouseRightButtonDown()
    {

    }
    public void OnMouseRightButtonStay()
    {
        modifyingGravityZone = playerInteract.GetInteractiveGravityZone();
    }
    public void OnMouseRightButtonUp()
    {
        if (modifyingGravityZone == null) return;
        modifyingGravityZone.ApplyGravity(GravityType.OneDirectional); //  Hard Coding
    }

    Vector3 ConvertVectorToOne(Vector3 inputVector)
    {
        return new Vector3(
            Mathf.Sign(inputVector.x),
            Mathf.Sign(inputVector.y),
            Mathf.Sign(inputVector.z)
        );
    }
    Vector3 SnapVectorToVectex(Vector3 inputVector, float offset = 0f)
    {
        Vector3 originVector = inputVector;

        float roundedX = Mathf.Round(originVector.x * 2) / 2;
        roundedX += roundedX == 0 ? inputVector.x >= 0 ? offset : -offset : 0;
        float roundedY = Mathf.Round(originVector.y * 2) / 2;
        roundedY += roundedY == 0 ? inputVector.y >= 0 ? offset : -offset : 0;
        float roundedZ = Mathf.Round(originVector.z * 2) / 2;
        roundedZ += roundedZ == 0 ? inputVector.z >= 0 ? offset : -offset : 0;

        return new Vector3(roundedX, roundedY, roundedZ);
    }
}
