using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rgd;

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    public int speed = 5;

    private void FixedUpdate()
    {
        rgd.MovePosition(transform.position+transform.forward* speed * Time.deltaTime);
    }


}