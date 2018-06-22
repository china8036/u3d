using Core;
using Message.Recv;
using Message.Requ;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour, NetListener
{


    public GameObject netPlayer;


    //消息监控
    //private Hashtable al = new Hashtable();


    private Dictionary<string, Vector3> netPlayes = new Dictionary<string, Vector3>();


    // Use this for initialization
    void Start()
    {
        Net.GetNetWork().AddMsgListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        Net.GetNetWork().SendMsg(new ClientRequ());

        if (GameObject.Find(Net.sid).GetComponent<Player>().isMainPlay)
        {
            Net.GetNetWork().SendMsg(new ClientOperateRequ());
        }
        

        // foreach (string clientId in netPlayes.Keys)
        // {
        //     GameObject tPlayer = GameObject.Find(clientId);
        //     if (tPlayer == null)
        //    {
        //        Debug.Log("new:" + clientId + "  position:" + netPlayes[clientId]);
        //        tPlayer = Instantiate(netPlayer, netPlayes[clientId], new Quaternion(0f, 0f, 0f, 0f));
        //        tPlayer.GetComponent<NetPlayer>().SetName(clientId);
        //     }
        //     else
        //    {
        //        Debug.Log("update:" + tPlayer.name + " position:" + netPlayes[clientId]);
        //        tPlayer.GetComponent<NetPlayer>().setPosition(netPlayes[clientId]);
        //        //tPlayer.GetComponent<Transform>().position = netPlayes[clientId];
        //        Debug.Log("After Update Position:" + tPlayer.GetComponent<Transform>().position);
        //    }
        // }
    }

    public string GetListenCtr()
    {
        return "Clients";
    }

    public void DealMsg(QueueMsg msg)
    {
        if (GameObject.Find(Net.sid).GetComponent<Player>().isMainPlay)
        {//主机
            OperateMsg operaMsg = JsonUtility.FromJson<OperateMsg>(msg.msg);
            if (string.IsNullOrEmpty(operaMsg.data.operateId))
            {
                return;
            }
            GameObject tPlayer = GameObject.Find(operaMsg.data.operateId);
            if (tPlayer == null)
            {
                tPlayer = Instantiate(netPlayer, new Vector3(), new Quaternion(0f, 0f, 0f, 0f));
                tPlayer.GetComponent<NetPlayer>().SetName(operaMsg.data.operateId);
                return;
            }
            tPlayer.GetComponent<Rigidbody>().AddForce(new Vector3(operaMsg.data.x, operaMsg.data.y, operaMsg.data.z));

        }
        else
        {

            PositionMsg pmsg = JsonUtility.FromJson<PositionMsg>(msg.msg);
            if (string.IsNullOrEmpty(pmsg.data.id))
            {
                return;
            }
            GameObject tPlayer = GameObject.Find(pmsg.data.id);
            Vector3 mainPlayer = new Vector3();
            mainPlayer.x = pmsg.data.x;
            mainPlayer.y = pmsg.data.y;
            mainPlayer.z = pmsg.data.z;
            if (tPlayer == null)
            {
                tPlayer = Instantiate(netPlayer, mainPlayer, new Quaternion(0f, 0f, 0f, 0f));
                tPlayer.GetComponent<NetPlayer>().SetName(pmsg.data.id);
            }
            else {
                tPlayer.GetComponent<NetPlayer>().setPosition(mainPlayer);
            }
           // if (netPlayes.ContainsKey(pmsg.data.id))
            //{
            //    netPlayes[pmsg.data.id] = mainPlayer;
            //}
            //else
            //{
           //     netPlayes.Add(pmsg.data.id, mainPlayer);

            //}

            //Debug.Log("net controller recv pos msg:" + msg);
        }
    }
}
