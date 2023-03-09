using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 摸牌消息
    /// </summary>
    public class MJMatchDrawOperate:MJBaseOperate
    {

        public int index;

        public int card;

        public MJMatchDrawOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            card = data.readInt();
        }
    }
}
