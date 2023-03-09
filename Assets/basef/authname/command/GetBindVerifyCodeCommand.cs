using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.authname
{
    public class GetBindVerifyCodeCommand : UserCommand
    {

        string mobilenumber;

        public GetBindVerifyCodeCommand(string mobilenumber)
        {
            this.id = CommandConst.PORT_PLAYER_REQUEST_VERIFYCODE_BIND;
            this.mobilenumber = mobilenumber;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeUTF(this.mobilenumber);
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            this.callbackobj = data.readBoolean();
        }
    }
}
