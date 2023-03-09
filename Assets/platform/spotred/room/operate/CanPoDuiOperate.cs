using cambrian.common;

namespace platform.spotred.room
{
    public class CanPoDuiOperate:BaseOperate
    {
        public int selfIndex;
        /// <summary>
        /// 可以破对的牌
        /// </summary>
        public int[] cards;

        public CanPoDuiOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();

            int len = data.readInt();
            cards=new int[len];
            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }
            
            if (!isRelay && StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                len = data.readInt();
                for (int i = 0; i < len; i++)
                {
                    data.readInt();
                }
            }
        }
    }
}
