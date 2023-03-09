using basef.player;
using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 搜索玩家
    /// </summary>
    public class ArenaSearchPlayerCommand:UserCommand
    {
        private long userid;
        public ArenaSearchPlayerCommand(long userid)
        {
            id = CommandConst.PORR_PLAYER_BRIEFUSER_INFO;
            this.userid = userid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(userid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            SimplePlayer player=new SimplePlayer();
            player.bytesRead(data);
            callbackobj = player;
        }
    }
}
