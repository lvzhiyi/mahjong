using cambrian.common;
using scene.game;
using platform.spotred;
using platform.mahjong;
using platform.poker;

namespace platform
{
    public class RecvRoomPlayersGPSInfoCommand : RecvPort
    {
        private ObserverMsg observer;
        public RecvRoomPlayersGPSInfoCommand()
        {
            id = RecvConst.COMMAND_CLIENT_ROOM_PLAYERS_GPS;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPRoomPlayersGPSInfoMsg());
            observer.addMsg(new RecvMJRoomPlayersGPSInfoMsg());
            observer.addMsg(new RecvPKRoomPlayersGPSInfoMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
