using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    private Vector3 lerpPos = new Vector3(1000.0f, 1000.0f, 1000.0f);

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, lerpPos, 0.2f);
    }

    public void SetLerpPos(Vector3 newPos)
    {
        if (!float.IsNaN(newPos.x) && !float.IsNaN(newPos.y) && !float.IsNaN(newPos.z))
        {
            lerpPos = newPos;
        }
        else
        {
            lerpPos = new Vector3(1000.0f, 1000.0f, 1000.0f);
        }
    }
}
