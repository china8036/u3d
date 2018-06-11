using System;

namespace Message.Requ
{



    [System.Serializable]
    public class PositionMsg : RequMsg
    {

        public float x;

        public float y;

        public float z;

        public PositionMsg() {
            this.ctr = "Position";
        }

       

    }
}
