using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 系统发牌操作
    /// </summary>
    public class MJMatchDealCardsOperate:MJBaseOperate
    {
        /// <summary>
        /// 手牌
        /// </summary>
        public int[][] cards;
        /// <summary>
        /// 庄家的摸的牌
        /// </summary>
        public int card;

        public MJMatchDealCardsOperate(int type, int selfIndex,bool isreplay = false) : base(type,selfIndex)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            card = data.readInt();

            cards = new int[data.readInt()][]; //每个人的牌
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = new int[data.readInt()];
                for (int j = 0; j < cards[i].Length; j++)
                {
                    cards[i][j] = data.readInt();
                }
            }
        }
    }
}
