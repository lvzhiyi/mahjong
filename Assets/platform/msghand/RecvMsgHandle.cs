using cambrian.common;

namespace platform
{
    public abstract class RecvMsgHandle
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        public int gamePlatform;
        public abstract void handle(ByteBuffer data);
    }
}
