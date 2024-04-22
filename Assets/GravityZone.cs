using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    private E_GravityType gravityType;
    public Transform parentTransform;
    public bool isGravityFieldCreated;
    public LayerMask defaultLayer;

    private void OnTriggerStay(Collider other)
    {
        if(!isGravityFieldCreated) return;

        if (other.TryGetComponent(out Apple apple))
        {
            Rigidbody rb = apple.GetComponent<Rigidbody>();
            switch (gravityType)
            {
                case E_GravityType.OneDirectional:
                    Vector3 dir = parentTransform.forward;
                    rb.velocity = (rb.velocity + dir).normalized;
                    
                    break;
                case E_GravityType.Central:
                    break;
                case E_GravityType.Repulsive:
                    break;
                default:
                    break;
            }
        }
    }

    public void ApplyGravity(E_GravityType gravityType)
    {
        switch (gravityType)
        {
            case E_GravityType.OneDirectional:
                StartCoroutine("MakeOneDirectionalGravity");
                break;
            case E_GravityType.Central:
                break;
            case E_GravityType.Repulsive:
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
        gravityType = E_GravityType.OneDirectional;
        parentTransform.localScale = originScale;
    }
}

public enum E_GravityType
{
    OneDirectional,
    Central,
    Repulsive,
}