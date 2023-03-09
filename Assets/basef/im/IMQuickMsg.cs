using cambrian.common;

namespace basef.im
{
    public class IMQuickMsg
    {

        /* static fields */
        /// <summary>
        /// 消息类型:1-快捷文字消息,2-快捷图片消息
        /// </summary>
        public const int TYPE_TEXT = 1, TYPE_ICON = 2;

        /* static methods */

        /* fields */

        /// <summary>
        /// 发送人id </summary>
        public long userid;
        /// <summary>
        /// 快捷消息类型 </summary>
        public int type;
        /// <summary>
        /// 快捷消息 </summary>
        public int value;
        /// <summary>
        /// 发送时间 </summary>
        public long time;
        /// <summary>
        /// 性别
        /// </summary>
        public int sex;
        /// <summary>
        /// 当快捷方式是文字的时候，使用
        /// </summary>
        public string info;

        /* constructors */
        public IMQuickMsg()
        {
        }
        public IMQuickMsg(int type, int value)
        {
            this.type = type;
            this.value = value;
        }

        public IMQuickMsg(int type, int value,string info)
        {
            this.type = type;
            this.value = value;
            this.info = info;
        }

        /* properties */

        /* init start */

        /* methods */

        /* common methods */
        public void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.type = data.readInt();
            this.value = data.readInt();
            this.time = data.readLong();
        }
        public override string ToString()
        {
            return "IMQuickMsg [userid=" + userid + ", type=" + type + ", value=" + value + ", time=" + time + "]";
        }
        /* inner class */
    }
}
