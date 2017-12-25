using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour,NetListener
{

    private Net netWork;

    private string name;


    public GameObject netPlayer;


	// Use this for initialization
	void Start ()
	{
        
        name = Random.Range(1f,100f).ToString();
        netWork = Net.GetNetWork();
        //netWork.AddMsgListener(this);

    }

    public void DealMsg(string msg) {
        //Debug.Log("player have deal the msg:" + msg);
    }
	
	// Update is called once per frame
	void Update ()
	{
 
        Vector3 go = new Vector3();
		if (Input.GetKey (KeyCode.UpArrow)) {
			go = new Vector3 (0.0f, 1.0f, 0.0f);
		}  
		if (Input.GetKey (KeyCode.DownArrow)) {
			go = new Vector3 (0.0f, -1.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {

			go = new Vector3 (-1.0f, 0.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			go = new Vector3 (1.0f, 0.0f, 0.0f);
		}
		GetComponent<Transform> ().position += go * Time.deltaTime * 2f;
        
	}

    void LateUpdate()
    {
        SendPosition();//发送位置信息
    }


    //发送位置
    void SendPosition() {
        netWork.Send("pos "  +  name + " " + GetComponent<Transform>().position.x + " " + GetComponent<Transform>().position.y + " " + GetComponent<Transform>().position.z);
    }



}
