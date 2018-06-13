
using Core;

namespace Message.Requ
{



    [System.Serializable]
    public class PositionRequ : RequBase
    {

        public float x;

        public float y;

        public float z;

        public PositionRequ():base() {
            this.ctr = "Position";
        }

       

    }
}
