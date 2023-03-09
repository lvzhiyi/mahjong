using cambrian.common;

namespace basef.arena
{
    public class TicketLevel:BytesSerializable
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

        /// <summary>
        /// 收取固定金豆值
        /// </summary>
        public long goldvalue;

        public TicketLevel()
        {
            minScore = 0;
            maxScore = -1;
            value = 300;
            goldvalue = 0;
        }

        public TicketLevel(long value)
        {
            minScore = 0;
            maxScore = -1;
            this.value = value;
        }
        

        public TicketLevel(long minScore,long maxScore,long value)
        {
            this.maxScore = maxScore;
            this.minScore = minScore;
            this.value = value;
        }
        public TicketLevel(long minScore, long maxScore, long value, long goldvalue)
        {
            this.maxScore = maxScore;
            this.minScore = minScore;
            this.value = value;
            this.goldvalue = goldvalue;
        }


        public void setData(long minScore,long maxScore,long value,long guding)
        {
            this.minScore = minScore;
            this.maxScore = maxScore;
            this.value = value;
            this.goldvalue = guding;
        }
        public void setData(long minScore, long maxScore, long value)
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

        public double getMinScore()
        {
            return MathKit.getRound2Double(minScore);
        }

        public void setMinScore(long minScore)
        {
            this.minScore = minScore;
        }

        public double getMaxScore()
        {
            return MathKit.getRound2Double(maxScore);
        }

        public void setMaxScore(long maxScore)
        {
            this.maxScore = maxScore;
        }

        public double getValue()
        {
            return MathKit.getRound2Double(value);
        }

        public void setValue(long value)
        {
            this.value = value;
        }
        /// <summary>
        /// 获取固定金豆值
        /// </summary>
        /// <returns></returns>
        public double getGoldValue()
        {
            return MathKit.getRound2Double(goldvalue);
        }

        public void setGoldValue(long goldvalue)
        {
            this.goldvalue = goldvalue;
        }
        public override void bytesRead(ByteBuffer data)
        {
            this.minScore = data.readLong();
            this.maxScore = data.readLong();
            this.value = data.readLong();
            this.goldvalue = data.readLong();
        }
        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(minScore);
            data.writeLong(maxScore);
            data.writeLong(value);
            data.writeLong(goldvalue);
        }
    }
}
