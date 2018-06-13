

namespace Core
{
    public class QueueMsg
    {
        public RecvMsg recvMsg;


        public string msg;


        public QueueMsg(RecvMsg recvMsg, string msg) {
            this.recvMsg = recvMsg;
            this.msg = msg;
        }
    }
}
