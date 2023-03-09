using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.lobby
{
    public class LobbyBindXLCommand:UserCommand
    {
        /// <summary>
        /// 闲聊1
        /// </summary>
        public const int XL_PLATFORM = 1;
        /// <summary>
        /// 认证码(返回的code码)
        /// </summary>
        private string code;
        /// <summary>
        /// 绑定平台
        /// </summary>
        private int bindPlatform;
        public LobbyBindXLCommand(string code,int bindPlatform)
        {
            this.id = CommandConst.PORT_PLAYER_BIND_XIANLIAO;
            this.code = code;
            this.bindPlatform = bindPlatform;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(bindPlatform);//闲聊平台
            data.writeInt(0);//该值代表兄弟棋牌平台id
            data.writeUTF(code);
        }

        public override void bytesRead(ByteBuffer data)
        {
            User.user.status = data.readInt();
            callbackobj = User.user.status;
        }
    }
}
