using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;


public class Net : MonoBehaviour
{

    //net gameobject name
    const string NET_OB_NAME = "net";

    //服务端套字节
    Socket socket;

    const int BUFFER_SIZE = 1024;

    //host
    const String HOST = "127.0.0.1";

    //port
    const int PORT = 8888;


    byte[] readBuffer = new byte[BUFFER_SIZE];


    //消息监控
    private ArrayList al;


    public void Connect()
    {

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(HOST, PORT);
        Log("客服端地址" + socket.LocalEndPoint.ToString());
        //byte[] bytes = System.Text.Encoding.Default.GetBytes (str);
        //socket.Send (bytes);
        //socket.EndSend (syncresult);
        //socket.Close ();
        al = new ArrayList();
        socket.BeginReceive(readBuffer, 0, readBuffer.Length, SocketFlags.None, ReceiveCb, null);
        //socket.Close ();
    }



    void Start() {
        Connect();
    }

    //获取Net组件功能
    public static Net GetNetWork() {
        return GameObject.Find(NET_OB_NAME).GetComponent<Net>();
        
    }


    //注册消息监听
    void ListenMsg(NetListener nl) {
        if (al.Contains(nl)) {
            return;
        }
        al.Add(nl);
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
            if (al.Count > 0) {
                foreach(NetListener nl in al) {
                    nl.dealMsg(str);
                }
            }
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

    public void Send(string msg)
        {
            msg += "\n";
            byte[] byteData = System.Text.Encoding.Default.GetBytes(msg);
            try
            { 

                Debug.Log("Send:" + msg);
                socket.Send(byteData);
            }
            catch (SocketException ex)
            {
                Debug.Log(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (SocketException ex)
            { }
        }



    protected void Log(String msg) {
        Debug.Log(msg);
    }



}
