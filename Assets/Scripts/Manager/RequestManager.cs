﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class RequestManager : BaseManager
{
    public RequestManager(GameFacade facade) : base(facade) { }


    private Dictionary<ActionCode, BaseRequest> requestDic = new Dictionary<ActionCode, BaseRequest>();
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDic.Add(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDic.Remove(actionCode);
    }

    
    public void HandleResponse(ActionCode actionCode, string data)
    {          

        BaseRequest request = requestDic.TryGet(actionCode);
        if (request == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类");return;
        }
        request.OnResPonse(data);
    }
}
