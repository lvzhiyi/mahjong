using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 取消操作
    /// </summary>
    public class MJMatchCancelOperate : MJBaseOperate
    {
        /// <summary>
        /// 谁取消的
        /// </summary>
        public int index;

        public MJMatchCancelOperate(int type, int selfIndex, bool isRelay = false) : base(type, selfIndex, isRelay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
        }
    }
}