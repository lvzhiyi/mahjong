using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.authname
{
    /// <summary> 用户-认证-通过账号密码 </summary>
    public class SetPasswordCommand : UserCommand
    {
        string password;

        string check;

        public SetPasswordCommand(string password, string check)
        {
            this.id = CommandConst.PORT_PLAYER_SET_PASSWORD;
            this.password = password;
            this.check = check;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeUTF(password);
            data.writeUTF(check);
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            int status = data.readInt();
            this.callbackobj = status;
        }
    }
}