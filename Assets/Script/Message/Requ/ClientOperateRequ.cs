
using Core;

namespace Message.Requ
{


    //请求其他客服端的操作 只有为主机时候才请求
    [System.Serializable]
    public class ClientOperateRequ : RequBase
    {
        
        public ClientOperateRequ():base() {
            this.ctr = "ClientOperate";
        }

       

    }
}
