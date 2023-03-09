using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收炸牌消息
    /// </summary>
    public class ZhaPaiOperate:BaseOperate
    {
        /// <summary>
        /// 是否移除手牌
        /// </summary>
        public bool isRemoveHandCard;
        /// <summary>
        /// 牌
        /// </summary>
        public int card;
        /// <summary>
        /// 自己的索引
        /// </summary>
        public int selfIndex;

        public ZhaPaiOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.index = data.readInt();
            this.isRemoveHandCard = data.readBoolean();
            this.card = data.readInt();
        }
    }
}
