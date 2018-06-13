
namespace Core
{

    public interface NetListener
    {

        string GetListenCtr();
        void DealMsg(QueueMsg msg);
    }
}