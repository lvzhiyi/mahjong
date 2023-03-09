using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 发送托管消息
    /// </summary>
    public class SendTrusteeCommand: UserCommand
    {
        bool trustee;


        int roomid;

        public SendTrusteeCommand(bool trustee,int roomid)
        {
            this.id = CommandConst.PORT_ROOM_TRUSTEE;
            this.trustee = trustee;
            this.roomid = roomid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeBoolean(trustee);
        }

        public override void bytesRead(ByteBuffer data)
        {
            bool b = data.readBoolean();//true,代表成功
        }
    }
}
