using Core;

namespace Message.Requ
{

    [System.Serializable]
    public class HeartBeatRequ : RequBase
    {
        public string data;

        public HeartBeatRequ():base()
        {  
            this.data = "heart beat!";
            this.ctr = "HearBeat";
        }
    }
}
