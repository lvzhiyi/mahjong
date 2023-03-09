using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 金豆明细数据 </summary>
    public class ArenaAccountsGoldContentData : BytesSerializable
    {
        /// <summary> 玩家ID </summary>
        public long userid;

        /// <summary> 上级ID </summary>
        public long master;

        /// <summary> 单据创建时间 </summary>
        public long time;

        /// <summary> 单据附加信息 </summary>
        public string info;

        /// <summary> 单据流水号 </summary>
        public long uuid;

        /// <summary> 单据类型 </summary>
        public int type;

        /// <summary> 单据来源 归属 </summary> 例如：用户ID,休闲场ID等
        public long source;

        /// <summary> 单据数值 </summary>
        private long _value;
        /// <summary>
        /// 原由（目标）id
        /// </summary>
        public long cause;
        /// <summary>
        /// 原由（目标）名称
        /// </summary>
        public string causeName;
       
        public double value
        {
            get { return MathKit.getRound2Long(_value); }
        }
        
        /// <summary> 剩余数量 </summary>
        private long _arenagold;
        public double arenagold
        {
            get { return MathKit.getRound2Long(_arenagold); }
        }

        public override void bytesRead(ByteBuffer data)
        {
            uuid = data.readLong();
            userid = data.readLong();
            type = data.readInt();
            _value = data.readLong();
            _arenagold = data.readLong();
            source = data.readLong();
            master = data.readLong();
            time = data.readLong();
            info = data.readUTF();
            cause = data.readLong();
            causeName = data.readUTF();
        }
    }
}
