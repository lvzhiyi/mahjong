using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 更新操作
    /// </summary>
    public class MJMatchUpdateStateslOperate : MJBaseOperate
    {
        public MJMatchUpdateStateslOperate(int type, int selfIndex, bool isRelay = false) : base(type, selfIndex, isRelay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
        }
    }
}