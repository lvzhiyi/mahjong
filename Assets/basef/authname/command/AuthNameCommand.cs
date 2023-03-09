using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.authname
{
    /// <summary> 实名认证 </summary>
    public class AuthNameCommand:UserCommand
    {
        private string name;
        private string iphone;
        private string verifycode;

        public AuthNameCommand(string name,string iphone,string verifycode)
        {
            this.id = CommandConst.PORT_PLAYER_SUBMIT_SMRZ;
            this.name = name;
            this.iphone = iphone;
            this.verifycode = verifycode;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeUTF(name);
            data.writeUTF(iphone);
            data.writeUTF(verifycode);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}
