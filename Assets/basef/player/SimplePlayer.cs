using cambrian.common;
using platform.spotred;

namespace basef.player
{
    public class SimplePlayer : StatusAble
    {
        /** 状态：在线 */
        public const int STATUS_ONLINE = 1;
        /** 状态：房卡场游戏中 */
        public const  int STATUS_FKC_GAMEING = 1 << 1;
        /** 状态：金币场游戏中 */
        public const  int STATUS_JBC_GAMEING = 1 << 2;
        /** 状态：房卡场限制 */
        public const int STATUS_FKC_LIMIT = 1 << 3;

        /** 用户ID */
        public long userid;
        /** 用户昵称 */
        public string name;
        /** 用户头像 */
        public string head;
        /** 用户主机地址（IP） */
        public string host;
        /** 用户性别，1为男性，2为女性 */
        public int sex;
        /** 权限*/
        public int permission;

        /// <summary>
        /// 新增限制组中使用
        /// </summary>
        public bool isSelect;

        public SimplePlayer()
        {

        }

        public void offline()
        {
            this.setStatus(STATUS_ONLINE, true);
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.name = data.readUTF();
            this.head = data.readUTF();
            this.sex = data.readInt();
            this.status = data.readInt();
            this.host = data.readUTF();

            if (this.head != null && StringKit.isInt(this.head))
            {
                this.head = "file://"+this.head;
                return;
            }

            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }
            this.head += "?" + MathKit.random(1, 10000);
        }

        public virtual void bytesRead1(ByteBuffer data)
        {

        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(userid);
            data.writeUTF(name);
            data.writeUTF(head);
            data.writeInt(sex);
            data.writeInt(status);
            data.writeUTF(host);
        }

        public void byteRead(long userid,ByteBuffer data)
        {
            this.userid = userid;
            this.name = data.readUTF();
            this.head = data.readUTF();
            this.sex = data.readInt();
            this.status = data.readInt();
            this.host = data.readUTF();

            if (this.head != null && StringKit.isInt(this.head))
            {
                this.head = "file://" + this.head;
                return;
            }
            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }
            this.head += "?" + MathKit.random(1, 10000);
        }

    }
}
