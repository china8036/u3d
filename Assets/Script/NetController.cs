using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour,NetListener {


    public GameObject netPlayer;


    //消息监控
    private Hashtable al = new Hashtable();


    private Dictionary<string, GameObject> netPlayes = new Dictionary<string, GameObject>();


    // Use this for initialization
    void Start () {
        Net.GetNetWork().AddMsgListener(this);
	}
	
	// Update is called once per frame
	void Update () {
        foreach (string k in al.Keys) {
            Vector3 tmpv3 = (Vector3)al[k];
            if (!netPlayes.ContainsKey(k))
            {
                GameObject tmpNetPlayer = Instantiate(netPlayer, tmpv3, new Quaternion(0f, 0f, 0f, 0f));
                tmpNetPlayer.GetComponent<NetPlayer>().SetName(k);
                netPlayes.Add(k, tmpNetPlayer);
            }
        }

    }


    void NetListener.DealMsg(string msg)
    {
        string[] args = msg.Split(' ');
        if (args.Length != 5 || args[0] != "pos")
        {
            return;
        }
        Vector3 mainPlayer = new Vector3();
        mainPlayer.x = float.Parse(args[2]);
        mainPlayer.y = float.Parse(args[3]);
        mainPlayer.z = float.Parse(args[4]);
        if (!al.Contains(args[1]))
        {
            //Debug.Log("new:[" + args[1] + "]" + mainPlayer.ToString());
            al.Add(args[1], mainPlayer);
        }
      
        //Debug.Log("net controller recv pos msg:" + msg);
    }
}
