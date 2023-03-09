using cambrian.common;
using platform.poker;
using platform.mahjong;
using platform.spotred;

namespace platform
{
    /// <summary>
    /// 重连操作
    /// </summary>
    public class ReConnectOperate : Process
    {
        Room room;

        ByteBuffer data;

        private ObserverMsg observer;

        public ReConnectOperate(Room room, ByteBuffer data)
        {
            this.room = room;
            this.data = data;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new CPConnectMsg());
            observer.addMsg(new MJConnectMsg());
            observer.addMsg(new PKConnectMsg());
        }
        public override void execute()
        {
            observer.notify(Room.room.roomRule.rule.getPlatFormValue(), data);
        }
    }
}
