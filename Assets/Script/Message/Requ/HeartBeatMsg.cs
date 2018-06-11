using System;

namespace Message.Requ
{

    [System.Serializable]
    public class HeartBeatMsg : RequMsg
    {
        public string data;

        public HeartBeatMsg()
        {
            this.data = "heart beat!";
            this.ctr = "HearBeat";
        }
    }
}
