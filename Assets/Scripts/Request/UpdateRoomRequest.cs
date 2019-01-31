using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class UpdateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.UpdateRoom;
        roomPanel = GetComponent<RoomPanel>();

        base.Awake();
    }

    public override void OnResPonse(string data)
    {
        UserData ud1 = null;
        UserData ud2 = null;

        string[] udArray = data.Split('|');
        ud1 = new UserData(udArray[0]);
        if (udArray.Length > 1)
        {
            ud2 = new UserData(udArray[1]);
        }
        roomPanel.SetAllPlayerResSync(ud1, ud2);
    }
}