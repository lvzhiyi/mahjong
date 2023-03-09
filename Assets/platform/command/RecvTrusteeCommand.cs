using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接受托管消息
    /// </summary>
    public class RecvTrusteeCommand : RecvPort
    {
        private ObserverMsg observer;
        public RecvTrusteeCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_Trustee;
            observer = new RecvMatchObserverMsg();
            register();
        }

        /// <summary>
        /// 注册消息接收者
        /// </summary>
        public void register()
        {
            observer.addMsg(new RecvCPTrusteeMsg());
            observer.addMsg(new RecvMJTrusteeMsg());
            observer.addMsg(new RecvPKTrusteeMsg());
        }

        public override void bytesRead(ByteBuffer data)
        {
            observer.notify(Room.room.getRule().getPlatFormValue(), data);
        }
    }
}
