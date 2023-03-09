using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.authname
{
    /// <summary> 修改密码 </summary>
    public class UpdatePasswordCommand : UserCommand
    {
        private string password;
        private string check;

        public UpdatePasswordCommand(string password, string check)
        {
            this.id = CommandConst.PORT_PLAYER_UPDATA_PASSWORD;
            this.password = password;
            this.check = check;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeUTF(password);
            data.writeUTF(check);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readInt();
        }
    }
}
