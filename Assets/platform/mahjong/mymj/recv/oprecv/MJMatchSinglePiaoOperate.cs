using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 单个人选择飘
    /// </summary>
    public class MJMatchSinglePiaoOperate: MJBaseOperate
    {
        /// <summary>
        /// 谁
        /// </summary>
        public int index;

        public MJMatchSinglePiaoOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
        }
    }
}
