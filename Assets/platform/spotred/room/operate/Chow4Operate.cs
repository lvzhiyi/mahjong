using cambrian.common;

/// <summary>
/// 吃成4根
/// </summary>
namespace platform.spotred.room
{
    public class Chow4Operate : BaseOperate
    {
        public int selfIndex;

        public int card;

        public int[] disableCard;
        public Chow4Operate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui,bool isRelay=false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            card = data.readInt();
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
            return base.getWaittime();
        }
    }
}
