using cambrian.common;

namespace basef.arena
{
    /// <summary> 奖章明细 </summary>
    public class ArenaMedalBill : BytesSerializable
    {
        /** 增加类型：赛场兑换 */
        public const int TYPE_ARENA_EXCHANGE = 106;
        /// <summary> 单据流水号 </summary>
        public long uuid;

        /// <summary> 单据类型 </summary>
        public int type;

        /// <summary> 单据来源 </summary> 归属（例如：用户ID,休闲场ID等）
        public long source;

        /// <summary> 单据数值 </summary>
        long value;

        /// <summary> 单据创建时间 </summary>
        public long time;

        /// <summary> 单据附加信息 </summary>
        public string info;

        /// <summary> 玩家剩余奖章 </summary>
        public long medal;

        /// <summary> 赛场ID </summary>
        public long arenaid;

       

        public long getValue()
        {
            return value;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.type = data.readInt();
            data.readInt();//后端判断单据操作模式
            this.source = data.readLong();
            this.value = data.readLong();
            this.arenaid = data.readLong();
            this.medal = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();
        }
    }
}
