using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;

    public override void Awake()
    {
        requestCode = RequestCode.User;

        actionCode = ActionCode.Login;

        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }

    private void Start()
    {
        requestCode = RequestCode.User;

        actionCode = ActionCode.Login;

        loginPanel = GetComponent<LoginPanel>();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + ',' + password;
        base.SendRequest(data);
    }

    public override void OnResPonse(string data)
    {
       
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode) int.Parse(strs[0]);
        loginPanel.OnLoginResponse(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            string userName = strs[1];
            int totalCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);
            UserData ud=new UserData(userName,totalCount,winCount);
            _facade.SetUserData(ud);
        }
    }
}