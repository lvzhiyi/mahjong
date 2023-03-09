using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 报牌
    /// </summary>
    public class AYMJMatchBaoPaiOperate: MJBaseOperate
    {
        public int index;

       

        public AYMJMatchBaoPaiOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
        }
    }
}
