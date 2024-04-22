using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private LayerMask gravityZoneLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {

    }

    public GravityZone GetInteractiveGravityZone()
    {
        Vector3 dir = Mouse3D.GetMousePosition() - mainCamera.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, dir, 15f, gravityZoneLayer);

        if (hits.Length > 0)
        {
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
        }

        return null;
    }
}
