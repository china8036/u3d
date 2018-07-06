
using Core;

namespace Message.Requ
{


    //上报位置
    [System.Serializable]
    public class PositionRequ : RequBase
    {

        public float x;

        public float y;

        public float z;


		public string positionId;

        public PositionRequ():base() {
            this.ctr = "Position";
        }

       

    }
}
