using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPlayer : MonoBehaviour, NetListener
{

    private Vector3 reP;


    private Vector3 mainPlayer = new Vector3();

	// Use this for initialization
	void Start () {
        reP = GetComponent<Transform>().position - GameObject.Find("Player").GetComponent<Transform>().position;
        Net.GetNetWork().AddMsgListener(this);//监听msg

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = mainPlayer + reP;
    }


    public void DealMsg(string msg)
    {
        string[] args = msg.Split(' ');
        if (args.Length != 4 || args[0] != "pos") {
            return;
        }
        mainPlayer.x = float.Parse(args[1]);
        mainPlayer.y = float.Parse(args[2]);
        mainPlayer.z = float.Parse(args[3]);
        Debug.Log("net player recv pos msg:" + msg);
    }

}
