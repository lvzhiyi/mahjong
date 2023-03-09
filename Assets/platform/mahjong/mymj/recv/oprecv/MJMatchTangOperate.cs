using cambrian.common;

namespace platform.mahjong
{
    public class MJMatchTangOperate:MJBaseOperate
    {
        /// <summary>
        /// 谁
        /// </summary>
        public int index;
        /// <summary>
        /// 躺的牌
        /// </summary>
        public int[] cards;

        public MJMatchTangOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            int len = data.readInt();
            cards = new int[len];
            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }
        }
    }
}
