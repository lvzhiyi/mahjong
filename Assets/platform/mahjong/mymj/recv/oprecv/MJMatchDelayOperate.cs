using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 延迟时间
    /// </summary>
    public class MJMatchDelayOperate:MJBaseOperate
    {
        public MJMatchDelayOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            
        }

        public override long getWaittime()
        {
            return 1500;
        }
    }
}
