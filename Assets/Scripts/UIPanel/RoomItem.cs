using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text userName;
    public Text totalCount;    
    public Text winCount;
    public Button joinButton;
    private int id;

    private RoomListPanel roomListPanel;

    private void Start()
    {
        if (joinButton != null)
        {        
            joinButton.onClick.AddListener(OnJoinButtonClick);
        }

    }

    public  void SetRoomInfo(int id,string userName, int totalCount, int winCount,RoomListPanel panel)
    {
        SetRoomInfo(id, userName, totalCount.ToString(), winCount.ToString(), panel);
    }
    public  void SetRoomInfo(int id,string userName, string totalCount, string winCount,RoomListPanel panel)
    {
        this.id = id;
        this.userName.text = userName;
        this.totalCount.text = "Total:" + totalCount;
        this.winCount.text = "Win:" + winCount;
        roomListPanel = panel;
    }


    private void OnJoinButtonClick()
    {   
        roomListPanel.OnJoinButtonClick(id);
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}