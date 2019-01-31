using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RoomListPanel : BasePanel
{
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    private ListRoomRequest listRoomRequest;

    private CreatRoomRequest creatRoomRequest;
    private JoinRoomRequest joinRoomRequest;
    private List<UserData> udList = null;

    private UserData ud1 = null;
    private UserData ud2 = null;

    public override void InjectPanelThings()
    {
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseBUttonCLick);
        transform.Find("CreatRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateButtonClick);
        transform.Find("ReFreshButton").GetComponent<Button>().onClick.AddListener(OnReFreshButtonClick);

//        transform.Find("CreatRoomButton").GetComponent<Button>().onClick.AddListener(LoadRoomItem);
        listRoomRequest = GetComponent<ListRoomRequest>();

        creatRoomRequest = GetComponent<CreatRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
    }

    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }

        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = uiMng.PushPanel(UIPanelType.Room); 
            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            ud1 = null;
            ud2 = null;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();

        gameObject.SetActive(true);

        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }

        listRoomRequest.SendRequest();
    }

    public override void OnPause()
    {
        base.OnPause();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
        listRoomRequest.SendRequest();
    }

    private void OnCloseBUttonCLick()
    {
        PlayClickSound();
        uiMng.PopPanel();
        uiMng.PopPanel();
    }

    private void OnCreateButtonClick()
    {
        BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
        creatRoomRequest.SetPanel(panel);
        creatRoomRequest.SendRequest();
    }

    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数\n" + ud.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜场\n" + ud.WinCount.ToString();
    }

    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray = roomLayout.GetComponentsInChildren<RoomItem>();

        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }

        int count = udList.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id, ud.Username, ud.TotalCount, ud.WinCount, this);
        }
    }

    public void OnJoinButtonClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }

    public void OnJoinResponse(ReturnCode returnCode, UserData ud1, UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                uiMng.ShowMessageSync("房间已销毁，无法加入");
                break;
            case ReturnCode.Fail:
                uiMng.ShowMessageSync("房间已满，无法加入");
                break;
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;

                break;
        }
    }

    public void OnReFreshButtonClick()
    {
        listRoomRequest.SendRequest();
    }
}