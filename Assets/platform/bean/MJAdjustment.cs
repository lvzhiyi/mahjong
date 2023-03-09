using cambrian.common;
using platform.mahjong;

namespace platform
{
    public class MJAdjustment:BytesSerializable
    {
        /// <summary>
        /// 人数
        /// </summary>
        public int playerCount;
        /// <summary>
        /// 房数
        /// </summary>
        public int cardBits;

        public int getCardTypeCount()
        {
            int c = 0;
            if ((cardBits & MJCard.DOT)==MJCard.DOT)
                c++;
            if ((cardBits & MJCard.BAM) == MJCard.BAM)
                c++;
            if ((cardBits & MJCard.CHA) == MJCard.CHA)
                c++;

            return c;
        }

        public override void bytesRead(ByteBuffer data)
        {
            playerCount = data.readInt();
            cardBits = data.readInt();
        }
    }
}
