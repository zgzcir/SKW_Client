using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private Text localPlayerUserName;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;

    private Text enemyPlayerUserName;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;


    private UserData ud;
    private UserData ud1;
    private UserData ud2;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;
    private bool isPopPanel = false;

    public override void InjectPanelThings()
    {
        localPlayerUserName = transform.Find("BluePanel/UserName").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();
        enemyPlayerUserName = transform.Find("RedPanel/UserName").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();

        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitClick);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
    }

    public void SetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }

    public void SetAllPlayerResSync(UserData ud1, UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }


    public void SetLocalPlayerRes(string userName, string totalCount, string winCount)
    {
        print("SetLocalPlayerRes");
        localPlayerUserName.text = userName;
        localPlayerTotalCount.text = "Total:" + totalCount;
        localPlayerWinCount.text = "Win:" + winCount;
    }

    public void ClearEnemyPlayerRes()
    {
        print("ClearEnemyPlayerRes");
        enemyPlayerUserName.text = "";
        enemyPlayerTotalCount.text = "";
        enemyPlayerWinCount.text = "等待玩家加入...";
    }

    private void SetEnemyPlayerRes(string userName, string totalCount, string winCount)
    {
        print("SetEnemyPlayerRes");
        enemyPlayerUserName.text = userName;
        enemyPlayerTotalCount.text = "Total:" + totalCount;
        enemyPlayerWinCount.text = "Win:" + winCount;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (ud != null)
        {
            SetLocalPlayerRes(ud.Username, ud.TotalCount.ToString(), ud.WinCount.ToString());
            ClearEnemyPlayerRes();
            ud = null;
        }

        if (ud1 != null || ud2 != null)
        {
            SetLocalPlayerRes(ud1.Username, ud1.TotalCount.ToString(), ud1.WinCount.ToString());
            if (ud2 != null)
            {
                SetEnemyPlayerRes(ud2.Username, ud2.TotalCount.ToString(), ud2.WinCount.ToString());
            }
            else if (ud2 == null)
            {
                ClearEnemyPlayerRes();
            }

            ud1 = null;
            ud2 = null;
        }

        if (isPopPanel)
        {
            uiMng.PopPanel();
            isPopPanel = false;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    private void OnStartClick()
    {
        startGameRequest.SendRequest();
    }

    public void OnStartResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Fail)
        {
            uiMng.ShowMessageSync("您不是房主，无法开始游戏");
        }
        else
        {
            uiMng.PushPanelSync(UIPanelType.Game);
        }
    }

    public override void OnPause()
    {
        base.OnPause(); 
        gameObject.SetActive(false);

    }

    private void OnExitClick()
    {
        quitRoomRequest.SendRequest();
    }

    public void OnExitResponse()
    {
        isPopPanel = true;
    }
}