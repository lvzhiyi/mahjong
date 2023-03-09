using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 拉黑
    /// </summary>
    public class LaHeiMemberCommand:UserCommand
    {
        private long arenaid;

        private long userid;
        /// <summary>
        /// true:拉入黑名单，false:解除黑名单
        /// </summary>
        private bool b;
        public LaHeiMemberCommand(long arenaid,long userid,bool b)
        {
            id = CommandConst.PORT_ARENA_MEMBER_PERMIT;
            this.arenaid = arenaid;
            this.userid = userid;
            this.b = b;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(userid);
            data.writeBoolean(b);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
