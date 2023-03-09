using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 单个人定缺
    /// </summary>
    public class MJMatchSingleDistypeOperate:MJBaseOperate
    {
        /// <summary>
        /// 谁
        /// </summary>
        public int index;

        public MJMatchSingleDistypeOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
        }
    }
}
