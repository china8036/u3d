using Core;
using Message.Requ;
using UnityEngine;
using System.Collections.Generic;

public class NetPlayer : Player, NetListener
{


	// Use this for initialization
	void Start ()
	{
		nowPosition = GetComponent<NetObject> ().getPosition();
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameObject.Find (Net.sid).GetComponent<MasterPlayer> ()) {//master更新受力
			Vector3 force = GetComponent<NetObject> ().getForce ();
			Debug.Log (this.name + ": add force " + force.ToString ());
			GetComponent<Rigidbody> ().AddForce (force);
		
		} else {//slave 更新位置
			GetComponent<Transform> ().position = GetComponent<NetObject> ().getPosition();
		}
        

        
		//Debug.Log("[" + name + "]:" + GetComponent<Transform>().position.ToString());
	}

	public string GetListenCtr ()
	{
		return "";
	}

	public void DealMsg (QueueMsg msg)
	{

	}





	void LateUpdate ()
	{
		if (GameObject.Find (Net.sid).GetComponent<MasterPlayer> ()) {//上报位置信息
			PositionRequ pmsg = new PositionRequ ();
			pmsg.x = GetComponent<Transform> ().position.x;
			pmsg.y = GetComponent<Transform> ().position.y;
			pmsg.z = GetComponent<Transform> ().position.z;
			pmsg.positionId = this.name;
			Net.GetNetWork ().SendRequ (pmsg);
		}
	}


}
