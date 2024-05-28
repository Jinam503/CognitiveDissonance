using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = transform.localScale;
        
        // Get the local size of the object
        Vector3 size = GetComponent<Renderer>().bounds.size;
        
        // Calculate the real size by multiplying the local scale with the local size
        Vector3 realSize = new Vector3(size.x * scale.x, size.y * scale.y, size.z * scale.z);
        
        // Print or use the real size as needed
        Debug.Log("Real Size: " + realSize);

    }
}
