using cambrian.common;
using platform.bean;

namespace platform.poker
{
    /// <summary> 结算阶段 </summary>
    public class DDZMOverOperate : PKBaseOperate
    {
        public int[][] playerCard { get; private set; }

        /// <summary> 使用的牌数组 </summary>
        public int[][] handcardsuse { get; private set; }

        /// <summary> 未发牌数组 </summary>
        public int[] sendcards { get; private set; }

        public long[] socre { get; private set; }

        public int[] userstatus { get; private set; }

        public int playerCount { get; private set; }

        public int[] status { get; private set; }

        public int chuntian { get; private set; }

        public DDZMOverOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            playerCount = data.readInt();
            socre = new long[playerCount];
            userstatus = new int[playerCount];
            playerCard = new int[playerCount][];
            status = new int[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                socre[i] = data.readLong();
                userstatus[i] = data.readInt();
                int cardLenght = data.readInt();
                playerCard[i] = new int[cardLenght];
                for (int j = 0; j < cardLenght; j++)
                {
                    playerCard[i][j] = data.readInt();
                }
                status[i] = userstatus[i];
            }
            DDZMatch.match.multipleBean.bytesRead(data);
            chuntian = DDZMatch.match.multipleBean.chunPoint;

            if (data.length == 0)
            {
                handcardsuse = new int[playerCount][];
                for (int i = 0; i < playerCount; i++)
                {
                    handcardsuse[i] = new int[0];
                }
                sendcards = new int[0];
                return;
            }

            handcardsuse = new int[playerCount][];
            for (int i = 0; i < playerCount; i++)
            {
                handcardsuse[i] = data.readInts();
            }
            sendcards = data.readInts();
        }
    }
}
