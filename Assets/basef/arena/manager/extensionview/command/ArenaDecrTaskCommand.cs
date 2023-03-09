using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 减少任务
    /// </summary>
    public class ArenaDecrTaskCommand : UserCommand
    {
        private long arenaid;

        private long task;
        /// <summary>
        /// 目标
        /// </summary>
        private long dest;

        public ArenaDecrTaskCommand(long arenaid,long task,long dest)
        {
            id = CommandConst.PORT_ARENA_DECR_TASK;
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
            callbackobj = data.readLong();
        }

        
    }
}
