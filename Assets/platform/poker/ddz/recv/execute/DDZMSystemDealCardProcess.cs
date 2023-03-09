using cambrian.common;

namespace platform.poker
{
    /// <summary> 系统发牌 具体操作 </summary>
    public class DDZMSystemDealCardProcess : Process
    {
        DDZMSystemDealCardOperate operate;

        public DDZMSystemDealCardProcess(object operate)
        {
            this.operate = (DDZMSystemDealCardOperate)operate;
        }

        public override void execute()
        {
            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            var match = DDZMatch.match;

            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            match.DealCards(operate.cards);                                    
            match.recorder.incrCount(operate.cards[match.mindex]);             
            match.setCallScore(0);

            panel.operate = operate.operateData.operates[match.mindex];
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            panel.gameView.recorder.setVisible(false);
            panel.gameView.status.hideBanker();
            panel.gameView.clock.setVisible(false);
            panel.gameView.clock.setIndex(operate.operateData.round);
            panel.topView.setMuitplier(DDZMatch.match.grossMuitiplier);
            panel.gameView.dealpoker.deal(operate.cards);

            operate.playOver();

            if (StatusKit.isStatus(panel.operate,PKOperateStatus.CAN_MING))
            {
                panel.gameView.operate.showOperateView(panel.operate);
            }
        }        
    }
}
