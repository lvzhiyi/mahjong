using basef.player;
using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取指定推广员的返利利率
    /// </summary>
    public class GetArenaRadioByAgentCommand:UserCommand
    {
        private long dest;
        public GetArenaRadioByAgentCommand(long dest)
        {
            id = CommandConst.PORT_ARENA_RADIO_BY_AGENT;
            this.dest = dest;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(dest);
        }

        public override void bytesRead(ByteBuffer data)
        {
            RebateList self = null;
            if (dest != Player.player.userid)
            {
                self = new RebateList();
                self.bytesRead(data);
            }

            RebateList list=new RebateList();
            list.bytesRead(data);
            callbackobj = new object[2]{list, self};
        }
    }
}
