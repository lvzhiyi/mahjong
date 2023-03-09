using cambrian.common;
using UnityEngine;

namespace cambrian.game
{
    public class RegistByDeviceCommand : UserCommand
    {
        /// <summary>
        /// 用户-注册-通过设备 </summary>
        public const int USER_REGIST_DEVICE = 101;

        User user;

        long master;

        public RegistByDeviceCommand(User user,long master)
        {
            this.id = USER_REGIST_DEVICE;
            this.master = master;
            this.user = user;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeLong(master);
            UserDevice.device.bytesWrite(data);
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            user.bytesRead(data);
            User.save(user);
            if (log.isDebug) Debug.Log(log.getMessage(this, "user=" + this.user));
            this.callbackobj = user;
        }
    }
}