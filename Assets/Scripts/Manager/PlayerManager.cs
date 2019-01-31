using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using Common;
using UnityEngine.Experimental.Playables;

public class PlayerManager : BaseManager
{
    public PlayerManager(GameFacade facade) : base(facade)
    {
    }

    private UserData userData;
    private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    private Transform rolePositions;


    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }

    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue,
            new RoleData(RoleType.Blue, "Hunter_Blue", "Arrow_BLUE",
                rolePositions.Find("Position1").transform.position));
        roleDataDict.Add(RoleType.Red,
            new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED",
                rolePositions.Find("Position2").transform.position));
    }

    private void SpawnRoles()
    {
        foreach (RoleData rd in roleDataDict.Values)
        {
            GameObject.Instantiate(rd.RolePrefab,rd.SpawnPosition,Quaternion.identity);
        }
    }
}