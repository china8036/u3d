using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

public class Conn {

    //常量
    public const int BUFFER_SIZE = 1024;
    //Socket
    public Socket socket;

    //是否使用
    public bool isUse = false;

    //Buff

    public byte[] readBuff ;

    public int buffCount = 0;

    //构造函数
    public Conn() {

        readBuff = new byte[BUFFER_SIZE];


    }

    //初始化
    public void Init(Socket socket) {
        this.socket = socket;
        this.isUse = true;
        this.buffCount = 0;
    }


    //缓存区剩余的字节数
    public int BuffRemain() {
        return BUFFER_SIZE - buffCount;
    }


    //获取客服端的地址
    public string GetAdress() {
        if (!isUse) {
            return "无法获取地址";
        }
        return socket.RemoteEndPoint.ToString();
    }


    public void Close() {
        if (!isUse) {
            return;
            }
        Console.WriteLine("[断开连接]" + GetAdress());
        socket.Close();
        isUse = false;
    }
}
