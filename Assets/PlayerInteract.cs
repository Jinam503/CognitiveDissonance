using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {

    }

    public GravityZone GetInteractiveGravityZone()
    {
        GravityZone returnGravityZone = null;

        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, dir, out hit, 20f))
        {
            if (hit.transform.TryGetComponent(out GravityZone gravityZone))
            {
                returnGravityZone = gravityZone;
            }
        }

        return returnGravityZone;
    }
}
