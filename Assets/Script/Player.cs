using UnityEngine;
using Message.Requ;
using Core;

public class Player : MonoBehaviour,NetListener
{

    private Net netWork;


    public GameObject netPlayer;


	// Use this for initialization
	void Start ()
	{
        
        name = Random.Range(1f,100f).ToString();
        netWork = Net.GetNetWork();
        netWork.AddMsgListener(this);

    }

    public string GetListenCtr()
    {
        return "";
    }


    public void DealMsg(QueueMsg msg) {
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
        PositionRequ pmsg = new PositionRequ();
        pmsg.x = GetComponent<Transform>().position.x;
        pmsg.y = GetComponent<Transform>().position.y;
        pmsg.z = GetComponent<Transform>().position.z;
        netWork.SendMsg(pmsg);
    }



}
