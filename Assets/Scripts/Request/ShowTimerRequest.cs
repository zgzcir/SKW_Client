using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class ShowTimerRequest : BaseRequest
{
    private GamePanel gamePanel;
    
    private void Awake()
    {
        actionCode = ActionCode.ShowTimer;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
   
    }


    public override void OnResPonse(string data)
    {
        int time = int.Parse(data);
        gamePanel.ShowTimeSync(time);
    }
}
