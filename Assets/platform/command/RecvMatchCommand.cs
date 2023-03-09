using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using scene.game;
using RecvMJMatchMsg = platform.mahjong.RecvMJMatchMsg;

namespace platform
{
    /// <summary>
    /// 接收比赛消息
    /// </summary>
    public class RecvMatchCommand : RecvPort
    {
        private ObserverMsg observer;
        public RecvMatchCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_MATCH_OPTION;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPMatchMsg());
            observer.addMsg(new RecvMJMatchMsg());
            observer.addMsg(new RecvPKMatchMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(),data);
        }
    }
}
