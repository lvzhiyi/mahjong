using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 创建竞赛场
    /// </summary>
    public class CreateArenaCommand:UserCommand
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

        public CreateArenaCommand(string arenaname,string notice,bool isShowRank,bool issuspend)
        {
            id = CommandConst.PORT_ARENA_CREATE;
            this.arenaname = arenaname;
            this.notice = notice;
            this.isShowRank = isShowRank;
            this.issuspend = issuspend;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeUTF(arenaname);
            data.writeUTF(notice);
            data.writeBoolean(issuspend);
            data.writeBoolean(isShowRank);
        }

        public override void bytesRead(ByteBuffer data)
        {
            Arena.arena=new Arena();
            Arena.arena.bytesRead(data);
            Arena.arena.getMember().bytesRead(data);
            callbackobj = Arena.arena;
        }
    }
}
