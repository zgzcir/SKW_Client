using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RegisterRequest : BaseRequest
{
    private RegisterPanel registerPanel;

    private void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Awake();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + ',' + password;
        base.SendRequest(data);
    }

    public override void OnResPonse(string data)
    {
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        registerPanel.OnRegisterResponse(returnCode);
    }
}