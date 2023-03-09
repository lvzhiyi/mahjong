using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 单个玩家换牌消息
    /// </summary>
    public class MJMatchSingleReplaceOperate : MJBaseOperate
    {
        /// <summary>
        /// 玩家索引
        /// </summary>
        public int index;

        public int[] replacecard;

        public MJMatchSingleReplaceOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex,
            isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            replacecard = new int[data.readInt()];
            for (int i = 0; i < replacecard.Length; i++)
                replacecard[i] = data.readInt();
        }
    }
}
