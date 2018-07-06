using Core;
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

        public float x;

        public float y;

        public float z;

        public string positionId;
    }
}
