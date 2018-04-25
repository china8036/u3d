using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

public class Net : MonoBehaviour
{

    //net gameobject name
    const string NET_OB_NAME = "Net";

    //服务端套字节
    Socket socket;

    const int BUFFER_SIZE = 1024;

    //host
    const String HOST = "127.0.0.1";

    //port
    const int PORT = 8888;

    //每次处理的msg最大长度
    const int ONCE_MSG_DEAL_LEN = 10;


    byte[] readBuffer = new byte[BUFFER_SIZE];



    //消息监控
    private  ArrayList al = new ArrayList();


    public void Connect()
    {

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(HOST, PORT);
        Log("客服端地址" + socket.LocalEndPoint.ToString());
        HeartBeat();
        socket.BeginReceive(readBuffer, 0, readBuffer.Length, SocketFlags.None, ReceiveCb, null);
    }



    void Start() {
        Connect();
    }

    void Update()
    {
        int dealMsgCount = 0;//归零 每次最多处理 ONCE_MSG_DEAL_LEN
        while (dealMsgCount < ONCE_MSG_DEAL_LEN && Protocol.msgQueue.Count >0)
        {
            string msg = (string)Protocol.msgQueue.Dequeue();
            //Debug.Log("recv msg :" + msg);
            foreach (NetListener nl in al)
            {
                 nl.DealMsg(msg);
            }
            dealMsgCount++;
        }
    }

    //获取Net组件功能
    public static Net GetNetWork() {
        return GameObject.Find(NET_OB_NAME).GetComponent<Net>();
        
    }


    //注册消息监听
    public void AddMsgListener(NetListener nl) {
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

            byte[] tmpBuffer = new byte[count];
            Array.Copy(readBuffer, tmpBuffer, count);
            Protocol.DealRevBuffer(tmpBuffer);
            Array.Clear(readBuffer,0, BUFFER_SIZE);
            //继续接收    
            socket.BeginReceive(readBuffer, 0, readBuffer.Length , SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception e)
        {
           // e.ToString;
            Log("连接已断开:" + e.ToString());
            socket.Close();
        }
    }








    public void Send(string msg)
        {
            if (msg != "heartbeat") {
                msg = Net.GetRandomString(0, true, true, true, false, "");
            }
            
            Int32 len = (Int32) msg.Length;
            byte[] length = BitConverter.GetBytes(len);
            byte[] byteData = System.Text.Encoding.Default.GetBytes(msg);
            byte[] sendbuff  = length.Concat(byteData).ToArray();
        try
            {
                Debug.Log("Send:" + msg);
                socket.Send(sendbuff);
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
            {
                Debug.Log(ex.Message);

           }
        }


    //执行心跳
     void HeartBeat() {
        System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒；
        t.Elapsed += new System.Timers.ElapsedEventHandler(OnHeartBeat);//到达时间的时候执行事件；
        t.AutoReset = true;// true;//设置是执行一次（false）还是一直执行(true)；
        t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
    }

    void OnHeartBeat(object source, System.Timers.ElapsedEventArgs e)
    {
        Send("heartbeat");
    }


    protected void Log(String msg) {
        Debug.Log(msg);
    }


    public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
    {
        if (length == 0) {
            length = new System.Random().Next(1, 2000);
        }
        byte[] b = new byte[4];
        new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
        System.Random r = new System.Random(BitConverter.ToInt32(b, 0));
        string s = null, str = custom;
        if (useNum == true) { str += "0123456789"; }
        if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
        if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
        for (int i = 0; i < length; i++)
        {
            s += str.Substring(r.Next(0, str.Length - 1), 1);
        }
        return s;
    }


}
