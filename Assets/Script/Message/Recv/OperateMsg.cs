using Core;
using System;

namespace Message.Recv
{



    [System.Serializable]
    public class OperateMsg : RecvMsg
    {
        public Operate data;
    }

    [System.Serializable]
    public class Operate
    {

        public float x;

        public float y;

        public float z;

		public double t;

        public string operateId;
    }
}
