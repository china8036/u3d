using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Linq;

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


    byte[] readBuffer = new byte[BUFFER_SIZE];


    //消息监控
    private ArrayList al = new ArrayList();


    public void Connect()
    {

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(HOST, PORT);
        Log("客服端地址" + socket.LocalEndPoint.ToString());
        HeartBeat();
        //byte[] bytes = System.Text.Encoding.Default.GetBytes (str);
        //socket.Send (bytes);
        //socket.EndSend (syncresult);
        //socket.Close ();
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
            //数据处理
            string str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, count);
            if (al.Count > 0) {
                foreach(NetListener nl in al) {
                    nl.DealMsg(str);
                }
            }
            Log("Recv:" + str);
            //if (recvStr.Length > 300) recvStr = "";
            //recvStr += str + "\n";
            //继续接收    
            socket.BeginReceive(readBuffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception e)
        {
            Log("连接已断开:" + e.StackTrace);
            socket.Close();
        }
    }

    public void Send(string msg)
        {
            msg += "\n";
            Int32 len = (Int32) msg.Length;
            byte[] length = BitConverter.GetBytes(len);
            byte[] byteData = System.Text.Encoding.Default.GetBytes(msg);
            byte[] sendbuff  = length.Concat(byteData).ToArray();
        try
            {
                Debug.Log("Send:" + sendbuff.ToString());
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
        t.AutoReset = false;// true;//设置是执行一次（false）还是一直执行(true)；
        t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
    }

    void OnHeartBeat(object source, System.Timers.ElapsedEventArgs e)
    {
        Send("heartbeat");
    }


    protected void Log(String msg) {
        Debug.Log(msg);
    }



}
