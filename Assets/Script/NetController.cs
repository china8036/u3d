using Core;
using System;
using Message.Recv;
using Message.Requ;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour, NetListener
{


	public GameObject netPlayer;


	public GameObject  masterPlayer;


	public GameObject slavePlsyer;


	//消息监控
	//private Hashtable al = new Hashtable();


	private Dictionary<string, Vector3> netPlayes = new Dictionary<string, Vector3> ();


	// Use this for initialization
	void Start ()
	{
		Net.GetNetWork ().AddMsgListener (this);
	}

	// Update is called once per frame
	void Update ()
	{
		//Net.GetNetWork ().SendMsg (new ClientRequ ());
		//if (GameObject.Find (Net.sid).GetComponent<MasterPlayer> ()) {
		//	Net.GetNetWork ().SendMsg (new ClientOperateRequ ());
		//}
	}

	public string GetListenCtr ()
	{
		return "Clients";
	}

	public void DealMsg (QueueMsg msg)
	{
		dealInitPlayer (msg);

		if (GameObject.Find (Net.sid).GetComponent<MasterPlayer> ()) {
			dealOpera (msg);
			
		} 

		dealPositon (msg);
        
        
	}

	private void dealInitPlayer (QueueMsg msg)
	{
		InitPlayerMsg initPlayerMsg = JsonUtility.FromJson<InitPlayerMsg> (msg.msg);
		if (string.IsNullOrEmpty (initPlayerMsg.data.initPlayerId)) {
			return;
		}
		Vector3 playerV = new Vector3 ();
		playerV.x = initPlayerMsg.data.x;
		playerV.y = initPlayerMsg.data.y;
		playerV.z = initPlayerMsg.data.z;
		GameObject tPlayer = GameObject.Find (initPlayerMsg.data.initPlayerId);
		if (tPlayer != null) {
			Destroy (tPlayer);
		}
		if (initPlayerMsg.data.isMaster) {
			tPlayer = Instantiate (masterPlayer, playerV, new Quaternion (0f, 0f, 0f, 0f));
		} else {
			tPlayer = Instantiate (slavePlsyer, playerV, new Quaternion (0f, 0f, 0f, 0f));
		}

		tPlayer.GetComponent<NetObject> ().SetName (initPlayerMsg.data.initPlayerId);
		GameObject.Find (Follow.mainCameraName).GetComponent<Follow> ().setPlayer(tPlayer);
	    
	}


	private void dealOpera (QueueMsg msg)
	{
		OperateMsg operaMsg = JsonUtility.FromJson<OperateMsg> (msg.msg);
		if (string.IsNullOrEmpty (operaMsg.data.operateId)) {
			return;
		}
		Debug.Log ("opera:" + msg.msg);

		double now = (DateTime.Now - new DateTime (1970, 1, 1)).TotalMilliseconds;
		Debug.Log ("now time : " + now);
		Debug.Log ("use time : " + (now - operaMsg.data.t));
		GameObject tPlayer = GameObject.Find (operaMsg.data.operateId);
		if (tPlayer == null) {
			Debug.Log ("update opera fail :[" + operaMsg.data.operateId + "] not found");
		}
		tPlayer.GetComponent<NetObject> ().setForce (new Vector3 (operaMsg.data.x, operaMsg.data.y, operaMsg.data.z));
	}


	private void dealPositon (QueueMsg msg)
	{
		PositionMsg pmsg = JsonUtility.FromJson<PositionMsg> (msg.msg);
		if (string.IsNullOrEmpty (pmsg.data.id)) {
			return;
		}
		GameObject tPlayer = GameObject.Find (pmsg.data.id);
		Vector3 mainPlayer = new Vector3 ();
		mainPlayer.x = pmsg.data.x;
		mainPlayer.y = pmsg.data.y;
		mainPlayer.z = pmsg.data.z;
		if (tPlayer == null) {
			Debug.Log ("update positon fail :[" + pmsg.data.id + "] not found");
		} else {
			tPlayer.GetComponent<NetObject> ().setPosition (mainPlayer);
		}
	}
}
