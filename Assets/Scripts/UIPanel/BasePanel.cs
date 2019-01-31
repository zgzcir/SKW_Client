using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BasePanel : MonoBehaviour
{
    protected UIManager uiMng;

    protected GameFacade facade;

    public GameFacade Facade
    {
        set { facade = value; }
    }
    public UIManager UIMng
    {
        set
        {
            uiMng = value;
        }
    }

    public void PlayClickSound()
    {
        facade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
    }

    /// <summary>
    /// 界面暂停 你爱pause不pause
    /// </summary>
    public virtual void OnPause()
    {


    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {

    }


    public virtual void InjectPanelThings()
    {

    }
}
