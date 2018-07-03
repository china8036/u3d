using Core;
using Message.Requ;
using UnityEngine;
using System.Collections.Generic;

public class NetPlayer : MonoBehaviour, NetListener
{

    private Vector3 reP;


    //private new string name = "";


    private Vector3 nowPosition;

    private Vector3 nowForce;

	private Queue<Vector3> forceQueue = new Queue<Vector3>(); 

    // Use this for initialization
    void Start() {
        nowPosition = this.GetComponent<Transform>().position;
        //Net.GetNetWork().AddMsgListener(this);
    }

    // Update is called once per frame
    void Update() {
		if (GameObject.Find (Net.sid).GetComponent<Player> ().isMainPlay) {
			if (this.forceQueue.Count > 0) {//一次取出一个
				Vector3 force = this.forceQueue.Dequeue();
				Debug.Log(this.name + ": add force " + force.ToString());
				GetComponent<Rigidbody>().AddForce(force);
			}
		
		} else {
			GetComponent<Transform>().position = nowPosition;
		}
        

        
        //Debug.Log("[" + name + "]:" + GetComponent<Transform>().position.ToString());
    }

    public string GetListenCtr() {
        return "";
    }

    public void DealMsg(QueueMsg msg)
    {

    }

    public void setPosition(Vector3 position) {
        this.nowPosition = position;
    }

    //设置受力
    public void setForce(Vector3 force)
    {
		this.forceQueue.Enqueue (force);
    }

    public void SetName(string name) {
        this.name = name;
    }



    public string GetName() {
        return this.name;
    }

    void LateUpdate()
    {
        if (GameObject.Find(Net.sid).GetComponent<Player>().isMainPlay)
        {//上报位置信息
            PositionRequ pmsg = new PositionRequ();
            pmsg.x = GetComponent<Transform>().position.x;
            pmsg.y = GetComponent<Transform>().position.y;
            pmsg.z = GetComponent<Transform>().position.z;
            pmsg.sid = this.name;
            Net.GetNetWork().SendMsg(pmsg);
        }
    }


}
