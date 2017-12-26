﻿

using System;
using System.Collections.Generic;

public class Protocol {

    //是不是新消息
    static Boolean isNewMsg = true;

    //本次消息完整长度
    static Int32 tMsgLen = 0;

    //本次消息还缺少多长
    static Int32 tMsgLackLen = 0;

    //本次消息字节数组
    static byte[] tMsgByte;

    //协议中 数据长度存储的字节大小
    const int lenBytesLength = 4;


    //处理完成的 msg记录
    public static Queue<string> msgQueue = new Queue<string>();





    
    //处理收到的数据 主要处理 粘包 分包问题 客服端按单线程处理
    public static void DealRevBuffer(byte[] byteArray) {
        int len = byteArray.Length;
        if (isNewMsg)
        {//新的消息
            tMsgLen = BitConverter.ToInt32(byteArray, 0);//前四个字节为msg长度
            if ((tMsgLen + 4) <= len)
            { //此byteArray 含有msg的完整记录
                isNewMsg = true;//递归仍然按新msg处理
                msgQueue.Enqueue(System.Text.Encoding.UTF8.GetString(byteArray, 4, tMsgLen));//此消息加入队列
                if ((tMsgLen + 4) == len) {//正好相等程序处理结束
                    return;
                }
                byte[] tmpArray = new byte[len - tMsgLen - 4];
                for (int i = tMsgLen + 4; i < len; i++)
                {
                    tmpArray[i - tMsgLen - 4] = byteArray[i];
                }
                
                DealRevBuffer(tmpArray);//递归处理剩余的记录
            }
            else
            {//长度小于msgLen 则此消息不完整
                isNewMsg = false;
                tMsgByte = new byte[tMsgLen];//生成本次的数组
                tMsgLackLen = tMsgLen - len + 4;//剩余长度
                for (int i = 4; i < len; i++)
                {
                    tMsgByte[i-4] = byteArray[i];// 赋值给tMsgByte 等下次消息继续拼接
                }
                
            }
        }
        else
        {//尚未完成拼接的msg

            if (tMsgLackLen > len)
            {//本次仍然不够长度
                for (int i = 0; i < len; i++)
                {
                    tMsgByte[tMsgLen - tMsgLackLen + i] = byteArray[i];// 赋值给tMsgByte 等下次消息继续拼接
                }
                tMsgLackLen -= len;//剩余的字节数减去本次收到的字节数 并等下次消息继续拼接

            }
            else 
            {//本次长度可以拼接完成msg
                isNewMsg = true;//下次按newMsg处理
                for (int i = 0; i < tMsgLackLen; i++)
                {
                    tMsgByte[tMsgLen - tMsgLackLen + i] = byteArray[i];// 赋值给tMsgByte 等下次消息继续拼接
                }
                msgQueue.Enqueue(System.Text.Encoding.UTF8.GetString(tMsgByte));//完成拼接 并把此消息加入队列
                if (tMsgLackLen == len) {
                    tMsgLen = tMsgLackLen = 0;
                    return;//完成拼接
                }
                byte[] tmpArray = new byte[len-tMsgLackLen];//生成本次的数组
                for (int i = tMsgLackLen; i < len; i++)
                {
                    tmpArray[i - tMsgLackLen] = byteArray[i];
                }

                DealRevBuffer(tmpArray);//递归处理剩余的记录


            }
            

        }
    }



  
}
