using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取赛场报表
    /// </summary>
    public class GetArenaGoldStatsCommand:UserCommand
    {
        private long arenaid;
        public GetArenaGoldStatsCommand(long arenaid)
        {
            id = CommandConst.PORT_ARENA_GOLD_STATS;
            this.arenaid = arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            ArenaForm form=new ArenaForm();
            form.bytesRead(data);
            callbackobj = form;
        }
    }
}
