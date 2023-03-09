using cambrian.common;
using platform.bean;

namespace platform.poker
{
    /// <summary> 比赛叫地主 数据 </summary>
    public class DDZMLandlordCallOperate : PKBaseOperate
    {
        public int index;

        public DDZMLandlordCallOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
        }
    }
}
