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

		public float rx;

		public float ry;


		public float rz;

		public  float rw;

        public string positionId;
    }
}
