using platform.spotred.room;

namespace platform.spotred.playback
{
    public class ReplayRecvCanPoDuiProcess:Process
    {
        private CanPoDuiOperate operate;

        public ReplayRecvCanPoDuiProcess(BaseOperate operate)
        {
            this.operate = (CanPoDuiOperate)operate;
        }

        public override void execute()
        {
            this.operate.playOver();
        }
    }
}
