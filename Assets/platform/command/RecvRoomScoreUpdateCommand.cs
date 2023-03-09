using cambrian.common;
using platform.poker;
using platform.mahjong;
using platform.spotred;
using scene.game;

namespace platform
{
    public class RecvRoomScoreUpdateCommand: RecvPort
    {
        private ObserverMsg observer;
        public RecvRoomScoreUpdateCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_UPDATE_SCORE;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPRoomScoreUpdateMsg());
            observer.addMsg(new RecvMJRoomScoreUpdateMsg());
            observer.addMsg(new RecvPKRoomScoreUpdateMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
