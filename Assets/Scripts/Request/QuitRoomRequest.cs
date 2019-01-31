﻿using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class QuitRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    
    private void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitRoom;
        roomPanel = GetComponent<RoomPanel>();    
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResPonse(string data)
    {
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        if(returnCode==ReturnCode.Success)
        {
           roomPanel.OnExitResponse();
        }
        
     }
}
