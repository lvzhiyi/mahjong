using cambrian.common;

namespace basef.arena
{
    public class ArenaEvent : Event
    {
        /// <summary>
        /// 事件类型：报名比赛
        /// </summary>
        public const int TYPE_APPLY_MATCH = 101;
        /// <summary> 事件类型：玩家申请兑换奖章 </summary>
        public const int TYPE_APPLY_EXCHANGE_MEDAL = 102;

        /// <summary> 事件类型：推广员申请减少任务 </summary>
        public const int TYPE_APPLY_DECR_TASK = 103;


//        /// <summary> 事件类型：成员变动记录（保留） </summary>
//        public const int TYPE_UPDATE_MEMBER = 105;

        /// <summary> 事件类型：成员加入申请（保留） </summary>
        //public const int TYPE_CREATE_MEMBER = 106;




        //------------获取事件，或者处理事件的时候，需要的类型-----------------------
        /** 事件类型：下级成员事件 */
        public const int EVENT_TYPE_MEMBER = 1;
        /** 事件类型：下级代理事件 */
        public const int EVENT_TYPE_AGENT = 2;
        /** 事件类型：房间事件 */
        public const int EVENT_TYPE_ROOM = 3;

        /// <summary> 处理者id </summary>
        public long master;
        /// <summary>
        /// 单据来源，归属（例如：用户ID,休闲场ID等
        /// </summary>
        public long userid;
        /// <summary>
        /// 单据数值
        /// </summary>
        public long value;

        /// <summary> 姓名 </summary>
        public string name;

        /// <summary> 头像 </summary>
        public string head;

        /// <summary>
        /// 信息
        /// </summary>
        public string info;

        public ArenaEvent()
        {
            this.status = STATUS_WAIT;
        }

        public int getType()
        {
            return type;
        }

        public long getUserid()
        {
            return userid;
        }

        public int getStatus()
        {
            return status;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }

        public string getTypeName()
        {
            switch (type)
            {
                case TYPE_APPLY_EXCHANGE_MEDAL:
                    return "兑换奖章: " + getValue() / Arena.PROPORTION + "奖章";
                case TYPE_APPLY_DECR_TASK:
                    return "申请减少任务";
                case TYPE_APPLY_MATCH:
                    return "报名比赛: " + getValue() + "积分";
            }

            return "";
        }

        /// <summary>
        /// 获取单据值
        /// </summary>
        /// <returns></returns>
        public long getValue()
        {
            return value / Arena.PROPORTION;
        }


        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(uuid);
            data.writeInt(type);
            data.writeInt(status);
            data.writeLong(arenaid);
            data.writeLong(userid);
            data.writeLong(master);
            data.writeLong(value);
            data.writeLong(time);
            data.writeUTF(name);
            data.writeUTF(head);
            data.writeUTF(info);
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.type = data.readInt();
            this.status = data.readInt();
            this.arenaid = data.readLong();
            this.userid = data.readLong();
            this.master = data.readLong();
            this.value = data.readLong();
            this.time = data.readLong();
            this.name = data.readUTF();
            this.head = data.readUTF();
            this.info = data.readUTF();
        }
    }
}
