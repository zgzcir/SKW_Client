using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    
    private Vector3 offset=new Vector3(0,10.31f,-8.84f);

    private float smoothing = 2;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position=Vector3.Lerp(transform.position, targetPosition,smoothing*Time.deltaTime);
        transform.LookAt(target);    }

}
