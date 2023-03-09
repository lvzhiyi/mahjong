using cambrian.common;
using System;

namespace basef.rank
{
    public class RankPlayer
    {
        /** 玩家ID */
        long userid;
        /** 比赛局数 */
        long matchCount;
        /** 头像 */
        String head;
        /** 昵称 */
        String nickname;
        /** 数据更新时间 */
        long updateTime;


        public long getUserId()
        {
            return this.userid;
        }

        public long getMatchCount()
        {
            return this.matchCount;
        }

        public string getNickName()
        {
            return this.nickname;
        }

        public string getHead()
        {
            return this.head;
        }

        public void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.head = data.readUTF();
            this.nickname = data.readUTF();
            this.matchCount = data.readLong();
            this.updateTime = data.readLong();
            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf=new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }

            this.head += "?" + MathKit.random(1, 10000);
        }
    }
}
