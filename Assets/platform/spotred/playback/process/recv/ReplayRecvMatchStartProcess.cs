using platform.spotred.room;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放-接收比赛开始
    /// </summary>
    public class ReplayRecvMatchStartProcess:Process
    {
        private ReplayMatchStartOperate operate;
        public ReplayRecvMatchStartProcess(BaseOperate operate)
        {
            this.operate = (ReplayMatchStartOperate) operate;
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
