using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace basef.im
{
    public class IMLoginCommand:IMCommand
    {
        /// <summary>
        /// 用户-认证-通过通行码 </summary>
        public const int USER_CERTIFY_PASSCHECK = 111;

        User user;

        long time;

        public IMLoginCommand(User user, long time)
        {
            this.id = USER_CERTIFY_PASSCHECK;
            this.user = user;
            this.time = time;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeLong(user.userid);
            data.writeUTF(user.passcheck);
            data.writeLong(time);
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            user.bytesRead(data);
            if (log.isDebug) Debug.Log(log.getMessage(this, "user=" + this.user));
            this.callbackobj = user;
        }
    }
}
