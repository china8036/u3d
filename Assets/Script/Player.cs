using UnityEngine;
using Message.Requ;
using Core;

public class Player : MonoBehaviour, NetListener
{

    private Net netWork;


    private float forceScale = 100f;


    /**
     * 主玩家 发起者 物理计算的核心成员
     */
    public bool isMainPlay = false;


    // public GameObject netPlayer;


    // Use this for initialization
    void Start()
    {
        name = Net.sid;
        netWork = Net.GetNetWork();
        netWork.AddMsgListener(this);

    }

    public string GetListenCtr()
    {
        return "";
    }


    public void DealMsg(QueueMsg msg)
    {
        //Debug.Log("player have deal the msg:" + msg);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 go = new Vector3();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            go = new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0.0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            go = new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            go = new Vector3(0.0f, 0.0f, -1.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            go = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            go = new Vector3(1.0f, 0.0f, 0.0f);
        }
        if (go.x == 0f && go.y == 0f && go.z == 0f)
        {//无屏蔽操作行为
            return;
        }
        Vector3 force = go * forceScale;

        if (isMainPlay)
        {
            //如果是主玩家 则物理数据以此为准 其他上报到此处处理
            GetComponent<Rigidbody>().AddForce(force);
            netWork.SendMsg(new ClientOperateRequ());//请求其他客服端操作
        }
        else
        {//如果不是主机 则上报操作给主机
            OperateRequ operateRequ = new OperateRequ();
            operateRequ.x = force.x;
            operateRequ.y = force.y;
            operateRequ.z = force.z;
            Net.GetNetWork().SendMsg(operateRequ);

        }


        //GetComponent<Transform> ().position += go * Time.deltaTime * 2f;

    }

    void LateUpdate()
    {
        if (isMainPlay)
        {
            SendPosition();//发送位置信息
        }
    }


    //发送位置
    void SendPosition()
    {
        PositionRequ pmsg = new PositionRequ();
        pmsg.x = GetComponent<Transform>().position.x;
        pmsg.y = GetComponent<Transform>().position.y;
        pmsg.z = GetComponent<Transform>().position.z;
        netWork.SendMsg(pmsg);
    }



}
