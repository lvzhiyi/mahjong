using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 摸牌操作
    /// </summary>
    public class DrawOperate:BaseOperate
    {
        public int selfIndex;

        public int[] cards;

        public int[] disableCard;
        public DrawOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt(); //玩家索引
            int length = data.readInt(); //牌的长度
            cards = new int[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = data.readInt();
            }

            disableCard = new int[0]; //自己不能出的牌
            if (!isRelay&&StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                int len = data.readInt();
                disableCard = new int[len];
                for (int i = 0; i < len; i++)
                {
                    disableCard[i] = data.readInt();
                }
            }
        }

        public override long getWaittime()
        {
            return 400;
        }
    }
}
