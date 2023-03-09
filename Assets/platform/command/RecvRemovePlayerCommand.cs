using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using scene.game;

namespace platform
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家离开房间
    /// </summary>
    public class RecvRemovePlayerCommand: RecvPort
    {
        private ObserverMsg observer;
        public RecvRemovePlayerCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_REMOVE;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPRemovePlayerMsg());
            observer.addMsg(new RecvMJRemovePlayerMsg());
            observer.addMsg(new RecvPKRemovePlayerMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (Room.room == null) return;
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
