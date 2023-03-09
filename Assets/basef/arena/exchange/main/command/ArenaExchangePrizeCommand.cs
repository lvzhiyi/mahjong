using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 兑换奖品 虚拟物品 </summary>
    public class ArenaExchangePrizeCommand : UserCommand
    {
        /// <summary>
        /// 兑换条码ID
        /// </summary>
        int uuid;
        /// <summary>
        /// 自定义兑换数值
        /// </summary>
        long value;

        public ArenaExchangePrizeCommand(int uuid)
        {
            this.id = CommandConst.PORT_ARENA_GET_MEDAL;
            this.uuid = uuid;
        }

        public ArenaExchangePrizeCommand(long value)
        {
            this.id = CommandConst.PORT_ARENA_GET_MEDAL;
            this.uuid = 0;
            this.value = value;
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(uuid);
            data.writeLong(value);
        }

    }
}
