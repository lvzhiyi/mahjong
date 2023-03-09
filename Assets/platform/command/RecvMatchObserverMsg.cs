using cambrian.common;

namespace platform
{
    /// <summary>
    /// 分发接收比赛消息到具体的游戏中
    /// </summary>
    public class RecvMatchObserverMsg : ObserverMsg
    {
        public override void notify(int gamePlatform, ByteBuffer data)
        {
            RecvMsgHandle handle;
            for (int i = 0; i < list.count; i++)
            {
                handle = list.get(i);
                if (list.get(i).gamePlatform == gamePlatform)
                {
                    handle.handle(data);
                }
            }
        }
    }
}
