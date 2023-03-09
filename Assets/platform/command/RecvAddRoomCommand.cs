using cambrian.common;
using scene.game;
using platform.spotred;
using platform.poker;
using platform.mahjong;

namespace platform
{
    /// <summary>
    /// 川牌-房间-接收玩家加入房间（后台广播）
    /// </summary>
    public class RecvAddRoomCommand : RecvPort
    {
        private ObserverMsg observer;

        public RecvAddRoomCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_ADD;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPAddRoomMsg());
            observer.addMsg(new RecvMJAddRoomMsg());
            observer.addMsg(new RecvPKAddRoomMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
