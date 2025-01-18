using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; 
    private Vector3 offset = new Vector3(0f, 2f, -20f);
    public float distanceDamp = 0.2f;

    public Vector3 velocity = Vector3.one;


    private void LateUpdate()
    {
        Vector3 toPos = target.position + (target.rotation * offset);
        Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDamp);
        transform.position = curPos;
        transform.LookAt(target, target.up);
    }

}
