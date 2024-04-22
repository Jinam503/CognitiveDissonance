using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    private void Update()
    {
        transform.position = Mouse3D.GetMousePosition();
    }
}
