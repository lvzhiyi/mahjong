using cambrian.common;
using UnityEngine;

namespace cambrian.game
{
    /// <summary>
    /// 用户-认证-通过验证码
    /// </summary>
    public class CertifyVerifyCodeCommand : UserCommand
    {
        /// <summary>
        /// 用户-认证-通过验证码 </summary>
        public const int USER_CERTIFY_VERIFYCODE = 113;

        string mobilenumber;

        string verifyCode;

        public CertifyVerifyCodeCommand(string mobilenumber, string verifyCode)
        {
            this.id = USER_CERTIFY_VERIFYCODE;
            this.mobilenumber = mobilenumber;
            this.verifyCode = verifyCode;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeUTF(mobilenumber);
            data.writeUTF(verifyCode);
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
    }
}