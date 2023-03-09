using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 听牌操作
    /// </summary>
    public class MJMatchTingOperate:MJBaseOperate
    {
        /// <summary>
        /// 谁
        /// </summary>
        public int index;

        public MJMatchTingOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
        }
    }
}
