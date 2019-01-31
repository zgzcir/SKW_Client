using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Experimental.Playables;

public class GamePanel : BasePanel
{
    private Text timer;
    private int time = -1;
    public override void InjectPanelThings()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
        }
    }

    public void ShowTimeSync(int time)
    {
        this.time = time;

    }
    

    public void ShowTime(int time)
    {timer.gameObject.SetActive(true);
        Debug.Log(time);
        facade.PlayNormalSound(AudioManager.Sound_Alert);
        timer.text = time.ToString();
        timer.transform.localScale=Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.transform.DOScale(1.7f, 0.4f).SetDelay(0.3f).OnComplete(()=>
        {
            timer.gameObject.SetActive(false);
        });
        timer.DOFade(0, 0.3f).SetDelay(0.3f);
      }
}
