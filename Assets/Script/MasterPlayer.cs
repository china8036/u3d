using UnityEngine;
using Message.Requ;
using Core;
using System;

public class MasterPlayer : MonoBehaviour, NetListener
{

	private Net netWork;


	private float forceScale = 100f;



	// public GameObject netPlayer;


	// Use this for initialization
	void Start ()
	{
		netWork = Net.GetNetWork ();
		netWork.AddMsgListener (this);
		SendPosition ();

	}

	public string GetListenCtr ()
	{
		return "";
	}


	public void DealMsg (QueueMsg msg)
	{
		//Debug.Log("player have deal the msg:" + msg);
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 go = new Vector3 ();

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			go = new Vector3 (touchDeltaPosition.x, touchDeltaPosition.y, 0.0f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			go = new Vector3 (0.0f, 0.0f, 1.0f);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			go = new Vector3 (0.0f, 0.0f, -1.0f);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {

			go = new Vector3 (-1.0f, 0.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			go = new Vector3 (1.0f, 0.0f, 0.0f);
		}
		if (go.x == 0f && go.y == 0f && go.z == 0f) {//无屏蔽操作行为
			return;
		}
		Vector3 force = go * forceScale;

		//如果是主玩家 则物理数据以此为准 其他上报到此处处理
		GetComponent<Rigidbody> ().AddForce (force);

	}

	void LateUpdate ()
	{
		SendPosition ();//发送位置信息
	}


	//发送位置
	void SendPosition ()
	{
		PositionRequ pmsg = new PositionRequ ();
		pmsg.x = GetComponent<Transform> ().position.x;
		pmsg.y = GetComponent<Transform> ().position.y;
		pmsg.z = GetComponent<Transform> ().position.z;
		netWork.SendRequ (pmsg);
	}



}
