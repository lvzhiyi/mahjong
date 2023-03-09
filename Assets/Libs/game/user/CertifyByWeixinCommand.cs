using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 用户-认证-通过微信
    /// </summary>
    public class CertifyByWeixinCommand : UserCommand
    {
        /// <summary>
        /// 用户-认证-通过微信 </summary>
        public const int USER_CERTIFY_WEIXIN = 114;
        /// <summary>
        /// 微信0，闲聊1
        /// </summary>
        public const int WEIXIN_CODE_TYPE = 0, XL_CODE_TYPE = 1;

        User user;

        string weixintmpcode;
        /// <summary>
        /// 平台code类型
        /// </summary>
        private int codetype;

        long master;

        public CertifyByWeixinCommand(User user, long master,int codetype)
        {
            this.id = USER_CERTIFY_WEIXIN;
            this.weixintmpcode = user.weixintmpcode;
            this.master = master;
            this.user = user;
            this.codetype = codetype;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeInt(codetype);
            data.writeInt(0);//该值代表麻将游戏平台id
            data.writeUTF(weixintmpcode);
            data.writeLong(master);
            UserDevice.device.bytesWrite(data);
        }
        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            user.bytesRead(data);
            User.save(user);
            this.callbackobj = user;
        }
    }
}