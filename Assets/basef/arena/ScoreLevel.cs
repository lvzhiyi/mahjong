using cambrian.common;

namespace basef.teahouse
{
    public class ScoreLevel:BytesSerializable
    {
  
        /// <summary>
        /// 收取门票自增步数
        /// </summary>
        public static int COLLECT_TICKET_STEP = 300;
        /// <summary>
        /// 10:每个阶段阶段金豆范围自增,-1:无上限
        /// </summary>
        public static int GOLD_STEP = 1000,NO_UPPER_LIMIT=-1;

        /** 最低分数 */
        public long minScore;
        /** 最大分数 */
        public long maxScore;
        /// <summary>
        /// 收取门票数
        /// </summary>
        public long value;

        public ScoreLevel()
        {
            minScore = 0;
            maxScore = -1;
            value = 300;
        }

        public void setData(long minScore,long maxScore,long value)
        {
            this.minScore = minScore;
            this.maxScore = maxScore;
            this.value = value;
        }

        public void setDatas(long minScore, long maxScore)
        {
            this.minScore = minScore;
            this.maxScore = maxScore;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.minScore = data.readLong();
            this.maxScore = data.readLong();
            this.value = data.readLong();
        }
        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(minScore);
            data.writeLong(maxScore);
            data.writeLong(value);
        }
    }
}
