using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 更新基础设置
    /// </summary>
    public class UpdateArenaBaseSettingCommand:UserCommand
    {
        /// <summary>
        /// 竞赛场名称
        /// </summary>
        private string arenaname;
        /// <summary>
        /// 公告
        /// </summary>
        private string notice;
        /// <summary>
        /// 是否显示排行榜
        /// </summary>
        private bool isShowRank;
        /// <summary>
        /// 是否打烊
        /// </summary>
        private bool issuspend;

        private long arenaid;

        public UpdateArenaBaseSettingCommand(long arenaid, string arenaname, string notice, bool isShowRank, bool issuspend)
        {
            id = CommandConst.PORT_ARENA_SETTING_BASE;
            this.arenaid = arenaid;
            this.arenaname = arenaname;
            this.notice = notice;
            this.isShowRank = isShowRank;
            this.issuspend = issuspend;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeUTF(arenaname);
            data.writeUTF(notice);
            data.writeBoolean(issuspend);
            data.writeBoolean(isShowRank);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readInt();
        }
    }
}
