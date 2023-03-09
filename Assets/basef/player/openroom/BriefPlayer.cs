using cambrian.common;

namespace basef.player
{
    public class BriefPlayer
    {
        /** 用户ID */
        public long userid;
        /** 用户昵称 */
        public string name;
        /** 用户头像 */
        public string headurl;
        /// <summary>
        /// 分数
        /// </summary>
        public long score;


        public void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.name = data.readUTF();
            this.headurl = data.readUTF();
            this.score = data.readLong();
            if (headurl != null && !"".Equals(headurl))
                this.headurl += "?" + MathKit.random(1, 10000);
        }

        public override string ToString()
        {
            return " BriefPlayer,userid:" + userid + ",name=" + name + ",headurl=" + headurl;
        }
    }
}
