using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaIncrTaskCommand:UserCommand
    {
        private long arenaid;

        private long task;
        /// <summary>
        /// 目标
        /// </summary>
        private long dest;

        public ArenaIncrTaskCommand(long arenaid,long task,long dest)
        {
            id = CommandConst.PORT_ARENA_INCR_TASK;
            this.arenaid = arenaid;
            this.task = task;
            this.dest = dest;
        }


        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(task*Arena.PROPORTION);
            data.writeLong(dest);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }

        
    }
}
