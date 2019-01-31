using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Net.Sockets;
public class ClientManager : BaseManager
{

    //public ClientManager(GameFacade facade) : base(facade) { }
    public ClientManager(GameFacade facade) : base(facade) { }
    private const string IP = "127.0.0.1";//47.106.254.223
    private int PORT = 6688;
    private Socket clientSocket;
    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);

            Start();
        }
        catch (Exception e)
        {
            Debug.Log("Wrong:无法连接到服务器端，请检查您的网络.." + e);

        }

    }
    private Message msg = new Message();
    private void Start()
    {if (clientSocket == null || clientSocket.Connected == false)
        {
            return;
        }
        clientSocket.BeginReceive(msg.Data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReciveCallBack, null);
    }
    private void ReciveCallBack(IAsyncResult ar)
    {
        Debug.Log("收到了服务器发来的消息");
        try
        {
            if (clientSocket == null || clientSocket.Connected == false)
            {
                return;
                
            }
            int count =  clientSocket.EndReceive(ar);
            msg.ReadMessage(count,OnProcessDataCallBack);
            Start();
        }
        catch (Exception e)     
        {
            Debug.Log(e);
        }
 
    }
    private void OnProcessDataCallBack(ActionCode actionCode, string data)
    {                Debug.Log(data);

       facade.HandleResponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.Log("无法关闭与服务器的连接" + e);
        }
    }
}
