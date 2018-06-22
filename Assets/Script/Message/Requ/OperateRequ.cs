
using Core;

namespace Message.Requ
{


    //上报客服端操作
    [System.Serializable]
    public class OperateRequ : RequBase
    {

        public float x;

        public float y;

        public float z;

        public OperateRequ():base() {
            this.ctr = "Operate";
        }

       

    }
}
