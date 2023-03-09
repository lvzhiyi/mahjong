using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 解散竞赛场
    /// </summary>
    public class DisbandArenaCommand : UserCommand
    {
        private long arenaid;
        public DisbandArenaCommand(long arenaid)
        {
            this.id = CommandConst.PORT_DISBAND_ARENA;
            this.arenaid = arenaid;
        }


        public override void bytesWrite(ByteBuffer data)
        {
           data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
