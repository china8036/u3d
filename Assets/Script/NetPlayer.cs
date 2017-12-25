using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPlayer : MonoBehaviour, NetListener
{

    private Vector3 reP;


    private new string name = "";


    private Vector3 nowPosition = new Vector3();

    // Use this for initialization
    void Start() {
        Net.GetNetWork().AddMsgListener(this);
    }

    // Update is called once per frame
    void Update() {
        GetComponent<Transform>().position = nowPosition;
        //Debug.Log("[" + name + "]:" + GetComponent<Transform>().position.ToString());
    }


    public void DealMsg(string msg)
    {
        string[] args = msg.Split(' ');
        if (args.Length != 5 || args[0] != "pos" || args[1] != name)
        {
            return;
        }
        Debug.Log("recv new postion:" + name);
        nowPosition.x = float.Parse(args[2]);
        nowPosition.y = float.Parse(args[3]);
        nowPosition.z = float.Parse(args[4]);


    }


    public void SetName(string name) {
        this.name = name;
    }



    public string GetName() {
        return this.name;
    }
}
