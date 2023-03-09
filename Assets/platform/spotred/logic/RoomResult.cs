using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 房间结果
    /// </summary>
    public class RoomResult: BytesSerializable
    {
        ArrayList<TotalScore> counts;
        /// <summary>
        /// 门票
        /// </summary>
        private long ticket;

        public RoomResult()
        {
            counts = new ArrayList<TotalScore>();
        }

        public ArrayList<TotalScore> getTotalScores()
        {
            return counts;
        }

        public long getTicket()
        {
            return ticket;
        }

        public void setTicket(long ticket)
        {
            this.ticket = ticket;
        }

       

        public void addSpotRedCounts(TotalScore[] count,long tikcet)
        {   
            counts.clear();
            counts.add(count);
            this.ticket = tikcet;
        }

        public TotalScore getIndexCount(int index)
        {
            return counts.get(index);
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(counts.count);
            for (int i=0;i<counts.count;i++)
            {
                counts.get(i).bytesWrite(data);
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            int size = data.readInt();
            this.counts = new ArrayList<TotalScore>();
            for (int i=0;i<size;i++)
            {
                TotalScore count = new TotalScore();
                count.bytesRead(data);
                this.counts.add(count);
            }
        }
    }

    /// <summary>
    /// 总的结算信息
    /// </summary>
    public class TotalScore:BytesSerializable
    {
        /** 总分数 */
        public long score;

        public override void bytesRead(ByteBuffer data)
        {
            this.score = data.readLong();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(this.score);
        }

        public void copy(SpotRedCount count)
        {
            this.score = count.score;
        }
    }
}