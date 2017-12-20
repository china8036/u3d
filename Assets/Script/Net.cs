using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Linq;
using System;

public class Net : MonoBehaviour
{

	//服务端套字节
	Socket socket;

	const int BUFFER_SIZE = 1024;

    //host
    const String HOST = "127.0.0.1";

    //port
    const int PORT = 8888;


	byte[] readBuffer = new byte[BUFFER_SIZE];

    public void Connect ()
	{

		socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect (HOST, PORT);
        Log("客服端地址" + socket.LocalEndPoint.ToString());
		//byte[] bytes = System.Text.Encoding.Default.GetBytes (str);
		//socket.Send (bytes);
		//socket.EndSend (syncresult);
		//socket.Close ();
		socket.BeginReceive (readBuffer, 0, readBuffer.Length, SocketFlags.None, ReceiveCb, null);
		//socket.Close ();
	}



    void Start() {
        Connect();
    }



    
     void Send(string msg) {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(msg);
        try {
            socket.Send(bytes);
        }
        catch (Exception e){
            Log("消息发送失败:" + e.Message);
        }

     }


    //接收回调
    private void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            //count是接收数据的大小
            int count = socket.EndReceive(ar);
            //数据处理
            string str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, count);
            Log(str);
            //if (recvStr.Length > 300) recvStr = "";
            //recvStr += str + "\n";
            //继续接收    
            socket.BeginReceive(readBuffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception e)
        {
            Log("连接已断开:" + e.Message);
            socket.Close();
        }
    }


    protected void Log(String msg) {
        Debug.Log(msg);
    }




}
