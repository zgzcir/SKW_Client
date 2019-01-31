using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;


public class ListRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        base.Awake();

        roomListPanel = GetComponent<RoomListPanel>();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResPonse(string data)
    {
        string[] strs1 = data.Split(',');

        ReturnCode returnCode = (ReturnCode) int.Parse(strs1[0]);
//        Debug.Log(returnCode.ToString());
//        if (returnCode == ReturnCode.Success)
//        {
      
            List<UserData> udList = new List<UserData>();
        if (returnCode == ReturnCode.Success)
        {
            string[] udArray = strs1[1].Split('|');
            foreach (string ud in udArray)
            {
                string[] strs2 = ud.Split(':');
                udList.Add(new UserData(int.Parse(strs2[0]), strs2[1], int.Parse(strs2[2]), int.Parse(strs2[3])));
            }
        }

        roomListPanel.LoadRoomItemSync(udList);
//        }
//        else if (returnCode == ReturnCode.Fail)
//        {
//            Debug.LogWarning("ListRoom失败(房间列表可能为空)");
//        }
    }
}