using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKAnYueMOverOperate : PKBaseOperate
    {
        /// <summary> 炸弹分数 </summary>
        public int[] bombScores { get; private set; }

        /// <summary> 春天倍数 </summary>
        public int[] springPoint { get; private set; }

        /// <summary> 单局总分 </summary>
        public long[] scores { get; private set; }

        /// <summary> 剩余牌数 </summary>
        public int[] cardsLength { get; private set; }


        public int[][] playerCard;
        /// <summary>
        /// 每个人打出的牌
        /// </summary>
        public int[][] playCards;

        /// <summary>
        /// 剩余的牌
        /// </summary>
        public int[] surplusCards;

        public PDKAnYueMOverOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            scores = new long[len];
            playerCard = new int[len][];
            bombScores = new int[len];
            cardsLength = new int[len];
            springPoint = new int[len];
            playerCard = new int[len][];
            playCards = new int[len][];
            for (int i = 0; i < len; i++)
            {
                scores[i] = data.readLong();
                springPoint[i] = data.readInt();
                bombScores[i] = data.readInt();
                cardsLength[i] = data.readInt();
                playerCard[i] = new int[cardsLength[i]];
                for (int j = 0; j < cardsLength[i]; j++)
                {
                    playerCard[i][j] = data.readInt();
                }

                playCards[i] = data.readInts();
            }

            surplusCards = data.readInts();

        }
    }
}
