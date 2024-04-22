using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMaker : MonoBehaviour
{
    private InputManager inputManager;

    private Vector3 initialGravityFieldPosition;
    private Vector3 initialGravityFieldNormal;
    private Vector3 gravityZoneVector;
    private Transform gravityZone;

    [SerializeField] private GameObject gravityZonePrefab;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.mouseLeft)
        {
            if(initialGravityFieldPosition == Vector3.zero)
            {
                (initialGravityFieldPosition, initialGravityFieldNormal) = Mouse3D.GetMousePositionAndNormal();
                gravityZone = Instantiate(gravityZonePrefab, initialGravityFieldPosition, Quaternion.LookRotation(initialGravityFieldNormal)).transform;

                gravityZoneVector = gravityZone.position;
                gravityZone.localScale = new Vector3(1, 1, 0.0001f);
            }

            Vector3 curPoint = Mouse3D.GetMousePosition();

            Vector3 distanceVector = curPoint - gravityZoneVector;
            float adjustedDistance = Vector3.Dot(distanceVector, gravityZone.up);

            float scaleSize = adjustedDistance * 0.5f * Mathf.Sqrt(2);
            Vector3 gravityZoneLocalScale = Vector3.one * scaleSize;
            gravityZoneLocalScale.z = 0.01f;
            gravityZone.localScale = gravityZoneLocalScale;
        }
        else
        {
            initialGravityFieldPosition = Vector3.zero;
        }
    }
}
