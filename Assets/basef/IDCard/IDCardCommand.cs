using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.IDCard
{
    /// <summary> 实名认证 </summary>
    public class IDCardCommand : UserCommand
    {
        //姓名  身份证
        private string name;
        private string idcard;

        public IDCardCommand(string name, string idcard)
        {
            this.id = CommandConst.PORT_PLAYER_SUBMIT_SFRZ;
            this.name = name;
            this.idcard = idcard;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeUTF(name);
            data.writeUTF(idcard);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}
