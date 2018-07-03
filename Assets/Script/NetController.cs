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
    }

    public string GetListenCtr()
    {
        return "Clients";
    }

    public void DealMsg(QueueMsg msg)
    {
        //Debug.Log("netController rev msg:" + msg.msg);
		if (GameObject.Find (Net.sid).GetComponent<Player> ().isMainPlay) {
			dealOpera(msg);
			
		} 

		dealPositon(msg);
        
        
    }

    private void dealOpera(QueueMsg msg)
    {
        if (GameObject.Find(Net.sid).GetComponent<Player>().isMainPlay)
        {//主机
            OperateMsg operaMsg = JsonUtility.FromJson<OperateMsg>(msg.msg);
            if (string.IsNullOrEmpty(operaMsg.data.operateId))
            {
                return;
            }
            //Debug.Log("netController rev operate msg:" + msg.msg);
            GameObject tPlayer = GameObject.Find(operaMsg.data.operateId);
            if (tPlayer == null)
            {
				return;
                //tPlayer = Instantiate(netPlayer, new Vector3(-4f,1f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                //tPlayer.GetComponent<NetPlayer>().SetName(operaMsg.data.operateId);
            }
            tPlayer.GetComponent<NetPlayer>().setForce(new Vector3(operaMsg.data.x, operaMsg.data.y, operaMsg.data.z));

        }
    }

    private void dealPositon(QueueMsg msg)
    {
        PositionMsg pmsg = JsonUtility.FromJson<PositionMsg>(msg.msg);
        if (string.IsNullOrEmpty(pmsg.data.id))
        {
            return;
        }
		//if (pmsg.data.id == Net.sid && GameObject.Find (Net.sid).GetComponent<Player> ().isMainPlay) {
		//	return;		
		//}
        //Debug.Log("netController rev position msg:" + msg.msg);
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
        else
        {
            tPlayer.GetComponent<NetPlayer>().setPosition(mainPlayer);
        }
    }
}
