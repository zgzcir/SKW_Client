using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Text;
using System.Linq;

public class Message
{
    public int startIndex = 0;
    private byte[] data = new byte[1024];
    public byte[] Data
    {
        get
        {
            return data;
        }
    }
    public int StartIndex { get; } = 0;


    
    public int RemainSize
    {
        get
        {
            return data.Length - startIndex;
        }
    }
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCAllBack)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if (startIndex - 4 >= count)
            {
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCAllBack(actionCode, s);//将服务器发来的数据拆为actioncode与data
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= count + 4;
            }
            else
            {
                return;
            }
        }
    }
 
    public static
  byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requstCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        int dataAmount = requstCodeBytes.Length + actionCodeBytes.Length + dataBytes.Length;

        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

        //byte[] newBytes = dataAmountBytes.Concat(requstCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        return dataAmountBytes.Concat(requstCodeBytes).ToArray<byte>()
              .Concat(actionCodeBytes).ToArray<byte>().
              Concat(dataBytes).ToArray<byte>();

    }
}
