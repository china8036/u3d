using System;
using System.Net;
using System.Net.Sockets;
/// <summary>
/// Summary description for Class1
/// </summary>
public class Serv
{

    public Socket listenfd;

    public Conn[] conns;

    public int maxConn = 50;


    //获取连接池索引, 返回负数表示获取失败
    public int NewIndex() {
        if (conns == null) { //conns未初始化
            return -1;
        }
        for (int i = 0; i < conns.Length; i++) {
            if (conns[i] == null)//还没有被使用过
            {
                conns[i] = new Conn();
                return i;
            }
            else if (conns[i].isUse == false) {//已无人使用
                return i;
            }

        }
        return -1;
    }

    //开启服务器
    public void Start(string host, int port) {

        conns = new Conn[maxConn];
        for (int i = 0; i < maxConn; i++) {
            conns[i] = new Conn();
        }
        //Socket
        listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //bind
        IPAddress ipAdr = IPAddress.Parse(host);
        IPEndPoint ipEp = new IPEndPoint(ipAdr, port);
        listenfd.Bind(ipEp);
        //listen
        listenfd.Listen(maxConn);
        //Accept
        listenfd.BeginAccept(AcceptCb, null);
        Console.WriteLine("[服务器]启动成功");
    }


    //Accept 回调
    private void AcceptCb(IAsyncResult ar) {

        try
        {
            Socket socket = listenfd.EndAccept(ar);
            int index = NewIndex();
            if (index < 0)
            {
                socket.Close();
                Console.WriteLine("[警告]连接已满");
            }
            else {
                Conn conn = conns[index];
                conn.Init(socket);
                string adr = conn.GetAdress();
                Console.WriteLine("客服端连接[" + adr + "] conn 池 ID:" + index);
                conn.socket.BeginReceive();
            }
        }
        catch(Exception e) {

        }
    }
}
