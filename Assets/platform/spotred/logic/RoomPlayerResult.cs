using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 玩家分数累计
    /// </summary>
    public class RoomPlayerResult: BytesSerializable
    {
        public ArrayList<SpotRedCount> counts;

        public void addSpotRedCounts(SpotRedCount[] count)
        {
            if(counts==null)
                counts=new ArrayList<SpotRedCount>();
            counts.clear();
            counts.add(count);
        }

        public SpotRedCount getIndexCount(int index)
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
            this.counts = new ArrayList<SpotRedCount>();
            for (int i=0;i<size;i++)
            {
                SpotRedCount count = new SpotRedCount();
                count.bytesRead(data);
                this.counts.add(count);
            }
        }
    }

    /// <summary>
    /// 总的结算信息
    /// </summary>
    public class SpotRedCount:BytesSerializable
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