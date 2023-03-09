using cambrian.common;
using UnityEngine;

namespace cambrian.game
{
    /// <summary>
    /// 用户-认证-通过通行码
    /// </summary>
    public class CertifyByPasscheckCommand : UserCommand
    {
        /// <summary>
        /// 用户-认证-通过通行码 </summary>
        public const int USER_CERTIFY_PASSCHECK = 111;

        User user;

        public CertifyByPasscheckCommand(User user)
        {
            this.id = USER_CERTIFY_PASSCHECK;
            this.user = user;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeLong(user.userid);
            data.writeUTF(user.passcheck);
            UserDevice.device.bytesWrite(data);
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0)
            {
                User.logout(user);
                return;
            }
            user.bytesRead(data);
            User.save(user);

            if (log.isDebug) Debug.Log(log.getMessage(this, "user=" + this.user));
            this.callbackobj = user;
        }
    }
}