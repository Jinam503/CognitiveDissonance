using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public Transform parentTransform;
    public bool isGravityFieldCreated;
    public LayerMask defaultLayer;

    public void ApplyGravity(GravityType gravityType)
    {
        switch (gravityType)
        {
            case GravityType.OneDirectional:
                StartCoroutine("MakeOneDirectionalGravity");
                break;
            case GravityType.Central:
                break;
            case GravityType.Repulsive:
                break;
        }
    }

    private IEnumerator MakeOneDirectionalGravity()
    {
        Vector3 dir = parentTransform.forward;

        Vector3 originScale = parentTransform.localScale;

        float distance = -1f;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, 999f, defaultLayer))
        {
            distance = hit.distance;

        }
        isGravityFieldCreated = true;
        while (originScale.z < distance)
        {
            originScale.z += 30 * Time.deltaTime;
            parentTransform.localScale = originScale;
            yield return null;
        }

        originScale.z = distance;
        parentTransform.localScale = originScale;
    }
}

public enum GravityType
{
    OneDirectional,
    Central,
    Repulsive,
}