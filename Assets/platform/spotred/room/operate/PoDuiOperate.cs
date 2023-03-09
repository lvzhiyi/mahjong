using cambrian.common;

namespace platform.spotred.room
{
    public class PoDuiOperate:BaseOperate
    {
        public int selfIndex;
        /// <summary>
        /// 不可出的牌
        /// </summary>
        public int card;

        int[] disableCard=new int[0];

        public PoDuiOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            card = data.readInt();
            if (!isRelay && StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                int len = data.readInt();
                disableCard = new int[len];
                for (int i = 0; i < len; i++)
                {
                    disableCard[i] = data.readInt();
                }
            }
        }
    }
}
