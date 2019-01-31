using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class CreatRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode= ActionCode.CreateRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }

    public void SetPanel(BasePanel panel)
    {
        roomPanel=panel as RoomPanel;
        
    }

    public override void SendRequest()
    {
        base.SendRequest("r");//象征性传递
        
    }

    public override void OnResPonse(string data)
    {        
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.SetLocalPlayerResSync();
        }
    }
}
