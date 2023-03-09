using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKMSystemDealCardOperate : PKBaseOperate
    {
        public int[][] cards;

        public PDKMSystemDealCardOperate(OperateData data) : base(data)
        {

        }


        public override long getWaittime()
        {
            return 500;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            int cardslen = 0;
            cards = new int[len][];
            for (int i = 0; i < len; i++)
            {
                cardslen = data.readInt();
                cards[i] = new int[cardslen];
                for (int j = 0; j < cardslen; j++)
                {
                    cards[i][j] = data.readInt();
                }
            }
        }
    }
}
