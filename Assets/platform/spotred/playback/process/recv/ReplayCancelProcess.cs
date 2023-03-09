using platform.spotred.room;

namespace platform.spotred.playback
{
    public class ReplayCancelProcess:Process
    {
        CancelOperate cancelOperate;
        public ReplayCancelProcess(BaseOperate cancelOperate)
        {
            this.cancelOperate = (CancelOperate)cancelOperate;
        }

        public override void execute()
        {
        //    ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
        //    if (cancelOperate.operates[cancelOperate.selfIndex] == 0)//无操作
        //        panel.operateView.hideAllBtn();

            this.cancelOperate.playOver();
        }
    }
}
