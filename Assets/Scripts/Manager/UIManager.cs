using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIManager : BaseManager
{
    private Dictionary<UIPanelType, string> panelPathDict; //存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict; //保存所有实例化面板的游戏物体身上的BasePanel组件
    public Stack<BasePanel> panelStack;

    private UIPanelType panelTypeToPush = UIPanelType.None;
    
    /// 
    /// 单例模式的核心
    /// 1，定义一个静态的对象 在外界访问 在内部构造
    /// 2，构造方法私有化
    public UIManager(GameFacade facade) : base(facade)
    {
        ParseUIPanelTypeJson();
    }

    private MessagePanel messagePanel;
    private Transform canvasTransform;


    public override void OnInit()
    {
        base.OnInit();
   
        PushPanel(UIPanelType.Message);
        PushPanel(UIPanelType.Start);
        
        PreLoadPanel(UIPanelType.Game);


        
    }


    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }

            return canvasTransform;
        }
    }

 
    //public UIManager()
    //{
    //    ParseUIPanelTypeJson();
    //}

    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public BasePanel   PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);


//        Debug.Log(panel.name + "pushed");
        return panel;
    }

    public override void Update()
    {
        if (panelTypeToPush != UIPanelType.None)
        {
            PushPanel(panelTypeToPush);
            panelTypeToPush = UIPanelType.None;
        }
//        DictDisp();
    }

    public void PushPanelSync(UIPanelType panelType)
    {
        panelTypeToPush = panelType;

    }
    
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        if (panelStack.Count <= 0)
        {
            return;
        }

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        Debug.Log(topPanel.name + "depushed");
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

 
    
    public void PreLoadPanel(UIPanelType panelType)
    {  
        LoadPanel(panelType).gameObject.SetActive(false);
    }


    private BasePanel LoadPanel(UIPanelType panelType)
    {    string path = panelPathDict.TryGet(panelType);
        GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        instPanel.transform.SetParent(CanvasTransform, false);
        instPanel.GetComponent<BasePanel>().UIMng = this;
        instPanel.GetComponent<BasePanel>().InjectPanelThings();
        instPanel.GetComponent<BasePanel>().Facade = facade;
        panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
        return instPanel.GetComponent<BasePanel>();
    }
    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);
        BasePanel panel = panelDict.TryGet(panelType);
        if (panel == null)
        {
//            Debug.Log(panelType.ToString());
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            return LoadPanel(panelType);
        }
        else
        {
            return panel;
        }
    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    public void InjectMsgPanel(MessagePanel msgPanel)
    {
        this.messagePanel = msgPanel;
    }

    public void ShowMessage(string msg)
    {
        if (messagePanel == null)
        {
            Debug.LogWarning("无法显示信息，messagePanel为空");
            return;
        }

        messagePanel.ShowMessage(msg);
    }

    public void ShowMessageSync(string msg)
    {
        if (messagePanel == null)
        {
            Debug.LogWarning("无法显示信息，messagePanel为空");
            return;
        }

        messagePanel.ShowMessageSync(msg);
    }

//    public void DictDisp()
       //
       //    {
       //        if (Input.GetKeyDown(KeyCode.K))
       //        {
       //            foreach (var VARIABLE in panelDict)
       //            {
       //               Debug.Log(VARIABLE.Key+":"+VARIABLE.Value.ToString()); 
       //            }
       //        }
       //        
       //    }
}