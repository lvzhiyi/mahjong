using cambrian.common;

namespace basef.arena
{
    public class TicketBill:BytesSerializable
    {
        /// <summary> 序列号</summary>
        public long uuid;
        ///<summary> 竞赛场ID </summary>
        public long arenaid;
        ///<summary> 玩家ID </summary>
        public long userid;
        ///<summary> 房间ID </summary>
        public int roomid;
        ///<summary> 结束时间 </summary>
        public long time;
        ///<summary> 比赛人次 </summary>
        public int count;
        ///<summary> 玩法名称 </summary>
        public string rule;
        ///<summary> 门票返利值 </summary>
        long value;

        public double getValueD()
        {
            return MathKit.getRound2Long(value) ;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.arenaid = data.readLong();
            this.userid = data.readLong();
            this.rule = data.readUTF();
            this.roomid = data.readInt();
            this.value = data.readLong();
            this.count = data.readInt();
            this.time = data.readLong();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(uuid);
            data.writeLong(arenaid);
            data.writeLong(userid);
            data.writeUTF(rule);
            data.writeInt(roomid);
            data.writeLong(value);
            data.writeInt(count);
            data.writeLong(time);
        }

    }
}
