namespace platform.poker
{
    public class DDZMMingPaiCancelProcess : Process
    {
        DDZMMingPaiCancelOperate operate;

        public DDZMMingPaiCancelProcess(object operate)
        {
            this.operate = (DDZMMingPaiCancelOperate)operate;
        }

        public override void execute()
        {
            var match = DDZMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();

            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;

            panel.gameView.stage.hidePStatus();                       

            operate.playOver();

            panel.gameView.operate.showOperateView(panel.operate,true); 
        }
    }
}
