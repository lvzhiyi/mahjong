using cambrian.common;

namespace basef.rank
{
    public class TeaRank:BytesSerializable
    {
        public long clubid;

        public string masterhead;

        public string clubname;

        /// <summary>
        /// 比赛次数
        /// </summary>
        public long match;
        /// <summary>
        /// 更新时间
        /// </summary>
        public long updatetime;

        public override void bytesRead(ByteBuffer data)
        {
            this.clubid = data.readLong();
            this.masterhead = data.readUTF();
            this.clubname = data.readUTF();
            this.match = data.readLong();
            this.updatetime = data.readLong();

            if (this.masterhead == null || this.masterhead.Length < 2) return;
            if (this.masterhead[this.masterhead.Length - 1] == '0')
            {
                if (this.masterhead[this.masterhead.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.masterhead, 0, this.masterhead.Length - 2);
                    buf.append("/64");
                    this.masterhead = buf.getString();
                }
            }

            this.masterhead += "?" + MathKit.random(1, 10000);
        }
    }
}
