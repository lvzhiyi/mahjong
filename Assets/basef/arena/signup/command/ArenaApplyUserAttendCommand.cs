using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 玩家点击报名 发送数据 </summary>
    public class ArenaApplyUserAttendCommand : UserCommand
    {
        /// <summary> 下标 或者 金豆值 </summary>
        long value;

        public ArenaApplyUserAttendCommand(long value)
        {
            this.id = CommandConst.PORT_ARENA_APPLY;
            this.value = value;
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(value * Arena.PROPORTION);
        }
    }
}
