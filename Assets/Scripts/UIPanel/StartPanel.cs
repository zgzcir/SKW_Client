using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
gameObject.SetActive(true);
        this.gameObject.transform.localScale = Vector3.one;
    }

    public override void InjectPanelThings()
    {
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginButtonClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterButtonClick);
    }

    public override void OnPause()
    {
        base.OnPause();
        this.gameObject.transform.localScale = Vector3.zero;
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);

        this.gameObject.transform.localScale = Vector3.one;
    }


    private void OnLoginButtonClick()
    {
        PlayClickSound();

        uiMng.PushPanel(UIPanelType.Login);
    }

    private void OnRegisterButtonClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }
}