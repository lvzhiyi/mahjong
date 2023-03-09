using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using scene.game;

namespace platform
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家准备(广播)
    /// </summary>
    public class RecvPlayerReadyCommand:RecvPort
    {
        private ObserverMsg observer;
        public RecvPlayerReadyCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_READY;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPPlayerReadyMsg());
            observer.addMsg(new RecvMJPlayerReadyMsg());
            observer.addMsg(new RecvPKPlayerReadyMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
