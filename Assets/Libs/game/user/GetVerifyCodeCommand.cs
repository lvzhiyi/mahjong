using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 获取短信验证码
    /// </summary>
    public class GetVerifyCodeCommand : UserCommand
    {
        /// <summary>
        /// 获取短信验证码
        /// </summary>
        public const int PORT_USER_VERIFYCODE_REQUEST = 141;

        string mobilenumber;

        public GetVerifyCodeCommand(string mobilenumber)
        {
            this.id = PORT_USER_VERIFYCODE_REQUEST;
            this.mobilenumber = mobilenumber;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(User.user.userid);
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