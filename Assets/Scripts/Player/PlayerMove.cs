using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float spped = 3;


    private float dSCorrection = 1.1f;


    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded")==false)
        {
        return;}

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            transform.Translate(new Vector3(h, 0, v) * spped * Time.deltaTime * dSCorrection, Space.World);

            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));

            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));

            anim.SetFloat("Forward", res);
        }
    }
}