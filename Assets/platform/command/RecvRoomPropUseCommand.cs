using cambrian.common;
using platform.mahjong;
using platform.poker;
using scene.game;

namespace platform.spotred.room
{
    public class RecvRoomPropUseCommand : RecvPort
    {
        private ObserverMsg observer;
        public RecvRoomPropUseCommand()
        {
            id = RecvConst.PORT_CLIENT_ROOM_USE_PROP;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPRoomPropUseMsg());
            observer.addMsg(new RecvMJRoomPropUseMsg());
            observer.addMsg(new RecvPKRoomPropUseMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
