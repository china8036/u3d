

using System;
using System.Collections.Generic;

public class Protocol {

    //是不是新消息
    static Boolean isNewMsg = true;

    //本次接收的消息长度
    static Int32 tMsgLen = 0;

    //本次消息还缺少多长
    static Int32 tMsglackLen = 0;

    //协议中 数据长度存储的字节大小
    const int lenBytesLength = 4;

    static Queue<string> msgQueue = new Queue<string>();


    static Queue<byte[]> msgBufferQueue = new Queue<byte[]>();



    public static void DealRevBuffer(byte[] byteArray) {
        int len = byteArray.Length;
        if (isNewMsg)
        {
            isNewMsg = false;
            tMsgLen = BitConverter.ToInt32(byteArray, 0);
            if ((tMsgLen + 4) < len)
            { //此byteArray 含有词条的完整记录
                msgQueue.Enqueue(System.Text.Encoding.UTF8.GetString(byteArray, 4, tMsgLen));//此消息加入队列
                byte[] tmpArray = new byte[len - tMsgLen - 4];
                for (int i = tMsgLen + 4; i < len; i++)
                {
                    tmpArray[i - tMsgLen - 4] = byteArray[i];
                }
                isNewMsg = true;//递归仍然按新纪录处理
                DealRevBuffer(tmpArray);//递归处理此条记录
            }
            else {
                tMsglackLen = tMsgLen - len - 4;//剩余长度
            }
        }
        else {


        }
    }

}
