using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
public class LoginPanel : BasePanel
{

    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    public override void InjectPanelThings()
    {

       transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(OnBackButtonClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginButtonClick);

        usernameIF = transform.Find("UserIDLable/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("PwdLable/InputField").GetComponent<InputField>();

        loginRequest = GetComponent<LoginRequest>();




    }
    public override void OnEnter()
    {
        base.OnEnter();
        this.gameObject.SetActive(true); 

    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
    }

    private   void  OnBackButtonClick()
    {PlayClickSound();
        uiMng.PopPanel();  
    }
    private void OnLoginButtonClick()
    {PlayClickSound();
        string msg = "";
        if(string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if(string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if(msg!="")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        loginRequest.SendRequest(usernameIF.text,passwordIF.text);
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);

    }
    public void OnLoginResponse(ReturnCode returntCode)
    {

        if (returntCode == ReturnCode.Success)
        {
         uiMng.PushPanelSync(UIPanelType.RoomList);
            
        }
     else
        {
            uiMng.ShowMessageSync("用户名或密码错误，请重新输入");
        }
    }
}
