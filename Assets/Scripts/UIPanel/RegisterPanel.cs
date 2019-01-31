using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    
    private RegisterRequest registerRequest;

    private void PopPanelSync()
    {
        uiMng.PopPanel();
    }
//
//    private ReturnCode registerReturnCode=ReturnCode.Fail;
//    private void Update()
//    {
//        if (registerReturnCode == ReturnCode.Success)
//        {
//            PopPanelSync();
//            registerReturnCode = ReturnCode.Fail;
//        }
//    }

    public override void InjectPanelThings()
    {
        registerRequest = GetComponent<RegisterRequest>();
        transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(OnBackButtonClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterButtonClick);
        usernameIF = transform.Find("UserIDLable/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("PwdLable/InputField").GetComponent<InputField>();
        rePasswordIF = transform.Find("RepeatePwdLable/InputField").GetComponent<InputField>();
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {  
//        registerReturnCode = returnCode;
         if (returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("注册成功");
          
        }
        else
        {
            uiMng.ShowMessageSync("注册失败，用户名已存在，请重新输入");
        }
    }

    private void OnRegisterButtonClick()
    {PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }

        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "\n密码不能为空";
        }

        if (passwordIF.text != rePasswordIF.text)
        {
            msg += "\n密码不一致";
        }

        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }

        registerRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    private void OnBackButtonClick()
    {PlayClickSound();
        uiMng.PopPanel();
    }

    public override void OnExit()
    {
        base.OnExit();

        gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.gameObject.SetActive(true);
    }
}