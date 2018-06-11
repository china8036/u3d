using System;

namespace Message.Recv
{



    [System.Serializable]
    public class PositionMsg : RecvMsg
    {
        public Position data;
    }

    [System.Serializable]
    public class Position
    {

        public Int32 x;

        public Int32 y;

        public Int32 z;
    }
}
