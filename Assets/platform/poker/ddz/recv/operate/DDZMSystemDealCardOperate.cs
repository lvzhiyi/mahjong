using cambrian.common;
using platform.bean;

namespace platform.poker
{
    /// <summary> 系统发牌 </summary>
    public class DDZMSystemDealCardOperate : PKBaseOperate
    {
        public int[][] cards;

        public DDZMSystemDealCardOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            cards = new int[data.readInt()][];
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