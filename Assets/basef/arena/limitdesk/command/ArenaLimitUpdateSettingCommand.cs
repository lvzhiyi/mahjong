using cambrian.common;
using cambrian.game;
using scene.game;


namespace basef.arena
{
    /// <summary>
    /// 竞赛场同桌限制确认
    /// </summary>
    public class ArenaLimitUpdateSettingCommand : UserCommand
    {

        /// <summary>
        /// 竞赛场id
        /// </summary>
        private long arenaid;
        /// <summary>
        /// 目标玩家id
        /// </summary>
        private long destid;

        /// <summary>
        /// 同桌限制控制 0关闭 1普通玩家 2普通玩家和代理 3整个树
        /// </summary>
        private int value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arenaid">竞赛场id</param>
        /// <param name="destid">玩家id</param>
        /// <param name="value">控制</param>
        public ArenaLimitUpdateSettingCommand(long arenaid,long destid,int value)
        {
            id = CommandConst.PORT_ARENA_LIMIT_SETTING_BASE;
            this.arenaid = arenaid;
            this.destid = destid;
            this.value = value;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(destid);
            data.writeInt(value);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}