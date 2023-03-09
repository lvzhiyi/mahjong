using cambrian.common;
using cambrian.game;

namespace cambrian.gamee
{
    public class ChatCertifyCommand:ChatCommand
    {
        /// <summary>
        /// 用户-认证-聊天 </summary>
        public const int CHAT_CERTIFY = 6121;

        private User user;

        public ChatCertifyCommand(User user)
        {
            this.id = CHAT_CERTIFY;
            this.user = user;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(user.userid);
            data.writeUTF(user.passcheck);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
