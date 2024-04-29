using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask ignoreMask = new LayerMask();

    private void Awake()
    {
        Instance = this;
    }

    public static (Vector3, Vector3) GetMousePositionAndNormal() => Instance.GetMousePositionAndNormal_Instance();

    private (Vector3, Vector3) GetMousePositionAndNormal_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, ignoreMask))
        {
            return (hitInfo.point, hitInfo.normal);
        }
        return (Vector3.zero, Vector3.zero);
    }

    public static Vector3 GetMousePosition() => Instance.GetMousePosition_Instance();

    private Vector3 GetMousePosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, ignoreMask))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
