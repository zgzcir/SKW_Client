using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager
{
    private GameObject cameraGo;
    private Animator cameraAnim;
    private FollowTarget followTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;


    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.K))
        {
            FollowTarget(GameObject.Find("Hunter_BLUE").transform);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            WalkThroughScene();
        }
    }

    public CameraManager(GameFacade facade) : base(facade)
    {
    }


    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        cameraAnim = cameraGo.GetComponent<Animator>();
        followTarget = cameraGo.GetComponent<FollowTarget>();
    }

    public void FollowTarget(Transform target)
    {
        cameraAnim.enabled = false;
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.rotation.eulerAngles;
        Quaternion lookRotation = Quaternion.LookRotation(target.position - cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(lookRotation, 0.7f).OnComplete(
            delegate
            {
                followTarget.target = target;
                followTarget.enabled = true;
            });
    }

    public void WalkThroughScene()    
    {
        followTarget.enabled = false;
        cameraGo.transform.DOMove(originalPosition, 1f);
        cameraGo.transform.DORotate(originalRotation, 1f).OnComplete(delegate { cameraAnim.enabled = true; });
    }
}