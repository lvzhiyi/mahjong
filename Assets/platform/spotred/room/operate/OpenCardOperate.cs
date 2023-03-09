using cambrian.common;

namespace platform.spotred.room
{
    public class OpenCardOperate:BaseOperate
    {
        public int selfIndex;

        public int card;

        public int[] disablecard;

        public OpenCardOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates,stage,round,paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();

            card=data.readInt();

            disablecard = new int[0];

            if (!isRelay&&StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                int len = data.readInt();
                disablecard = new int[len];
                for (int i = 0; i < len; i++)
                {
                    disablecard[i] = data.readInt();
                }
            }
        }

        public override long getWaittime()
        {
            return 1500;
        }
    }
}
