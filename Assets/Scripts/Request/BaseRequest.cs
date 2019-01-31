using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade _facade;

    protected GameFacade facade
    {
        get
        {
            if (_facade == null)
            {
                _facade=GameFacade.Instance;
            }
            return _facade;

        }
    }
    
    
    public virtual void Awake()
    {

//        _facade = GameFacade.Instance;
        facade.AddRequest(actionCode, this);

    }
 
    public virtual void SendRequest()
    {

    }

    public virtual void OnResPonse(string data)
    {
        Debug.Log("request");
    }
    protected void SendRequest(string data)
    {
        facade.SendRequest(requestCode, actionCode, data);
    }
    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
