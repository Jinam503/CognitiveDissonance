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
    private Transform childGravityZone;
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
                childGravityZone = gravityZone.GetChild(0);

                gravityZoneVector = gravityZone.position;
                gravityZone.localScale = new Vector3(1, 1, 0.0001f);
            }

            Vector3 curPoint = Mouse3D.GetMousePosition();
            Vector3 distanceVector = curPoint - gravityZoneVector;

            float scaleX = Vector3.Dot(distanceVector, gravityZone.right);
            float scaleY = Vector3.Dot(distanceVector, gravityZone.up);
            float scaleZ = 0.01f;

            Vector3 gravityZoneLocalScale = new Vector3(scaleX, scaleY, scaleZ);
            gravityZone.localScale = gravityZoneLocalScale;

            childGravityZone.localScale = ConvertVectorToOne(gravityZoneLocalScale);
        }
        else
        {
            initialGravityFieldPosition = Vector3.zero;
        }
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
