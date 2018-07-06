using UnityEngine;
using Message.Requ;
using Core;
using System;

public class SlavePlayer : MonoBehaviour, NetListener
{

	private Net netWork;


	private float forceScale = 100f;




	// public GameObject netPlayer;


	// Use this for initialization
	void Start ()
	{
		name = Net.sid;
		netWork = Net.GetNetWork ();
		netWork.AddMsgListener (this);
		Destroy (GetComponent<Rigidbody> ());//从客服端暂时去除物理引擎

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
		GetComponent<Transform> ().position = GetComponent<NetObject> ().getPosition();//更新为服务器上位置
		GetComponent<Transform> ().rotation = GetComponent<NetObject> ().getQuaternion();
		Vector3 go = new Vector3 ();

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			go = new Vector3 (touchDeltaPosition.x, 0f, touchDeltaPosition.y);
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

       
		OperateRequ operateRequ = new OperateRequ ();
		operateRequ.x = force.x;
		operateRequ.y = force.y;
		operateRequ.z = force.z;
		operateRequ.t = (DateTime.Now - new DateTime (1970, 1, 1)).TotalMilliseconds;
		Net.GetNetWork ().SendRequ (operateRequ);//发送操作

	}





}
