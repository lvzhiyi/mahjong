using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using scene.game;

namespace platform
{
    public class RecvPlayerConnectStatusComamnd:RecvPort
    {
        private ObserverMsg observer;
        public RecvPlayerConnectStatusComamnd()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_CONNECT_STATUS;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPPlayerConnectStatusMsg());
            observer.addMsg(new mahjong.RecvMJPlayerConnectStatusMsg());
            observer.addMsg(new RecvPKPlayerConnectStatusMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (Room.room == null) return;
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
