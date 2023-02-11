using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;
    
    private Vector3 offset;

    void Start()
    {   
        offset = (transform.position * smoothSpeed) - (target.position * smoothSpeed);
    }

    private void LateUpdate()
    {
        SmoothFollow();   
    }

    public void SmoothFollow()
    {   
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        target.position, smoothSpeed);
        transform.position = smoothFollow + offset;
        if((target.position - transform.position).magnitude < 0.001f) {
            transform.position = target.position + offset;
        }
        //transform.position -= new Vector3(0,0.25f * smoothSpeed,0);
    }
}
