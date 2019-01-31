using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RoleData
{
    public RoleType RoleType { get; private set; }
    public GameObject RolePrefab { get; private set; }
    public GameObject ArrowPrefab { get; private set; }
    public Vector3 SpawnPosition { get; private set;}

    public RoleData(RoleType roleType, string rolePath, string arrowPath,Vector3 spawnPos)
    {
        RoleType = roleType;
        RolePrefab = Resources.Load<GameObject>(rolePath);
        ArrowPrefab = Resources.Load<GameObject>(arrowPath);
        this.SpawnPosition = spawnPos;
    }
}