using cambrian.common;
using scene.game;
using platform.spotred;
using platform.mahjong;
using platform.poker;

namespace platform
{
    /// <summary>
    /// 前台-后台发送-接收销毁房间的消息
    /// </summary>
    public class RecvRoomDestoryCommand:RecvPort
    {
        private ObserverMsg observer;
        public RecvRoomDestoryCommand()
        {
            id = RecvConst.COMMAND_CLIENT_ROOM_DESTORY;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPRoomDestoryMsg());
            observer.addMsg(new RecvMJRoomDestoryMsg());
            observer.addMsg(new RecvPKRoomDestoryMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
