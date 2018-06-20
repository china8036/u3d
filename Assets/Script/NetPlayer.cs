using Core;
using UnityEngine;

public class NetPlayer : MonoBehaviour, NetListener
{

    private Vector3 reP;


    //private new string name = "";


    private Vector3 nowPosition;

    // Use this for initialization
    void Start() {
        nowPosition = this.GetComponent<Transform>().position;
        //Net.GetNetWork().AddMsgListener(this);
    }

    // Update is called once per frame
    void Update() {
        GetComponent<Transform>().position = nowPosition;
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

    public void SetName(string name) {
        this.name = name;
    }



    public string GetName() {
        return this.name;
    }
}
