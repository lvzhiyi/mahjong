using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 用户-认证-通过账号密码
    /// </summary>
    public class CertifyByPasswordCommand : UserCommand
    {
        /// <summary>
        /// 用户-认证-通过ID和密码 </summary>
        public const int PORT_USER_CERTIFY_IDPASSWORD = 115;

        long userid;

        string password;

        long time;

        public CertifyByPasswordCommand(long userid, string password)
        {
            this.id = PORT_USER_CERTIFY_IDPASSWORD;
            this.userid = userid;
            this.password = password;
            this.time = TimeKit.currentTimeMillis;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeLong(userid);
            data.writeUTF(getSign());
            data.writeLong(time);
            UserDevice.device.bytesWrite(data);
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            User.user.bytesRead(data);
            User.save(User.user);
            this.callbackobj = User.user;
        }

        private string getSign()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.writeUTF(password);
            buffer.writeLong(time);
            return MD5.encode(buffer.toArray());
        }
    }
}