namespace platform.poker
{
    public class DDZMDealLandlordCardProcess : Process
    {
        private DDZMDealLandlordCardOperate operate;

        private DDZMatch match;

        private PKRoomPanel panel;

        public DDZMDealLandlordCardProcess(object operate)
        {
            this.operate = (DDZMDealLandlordCardOperate)operate;
        }

        public override void execute()
        {
            panel = PKRoomPanel.Panel;
            match = DDZMatch.match;
            var playerCards = match.getPCardIndex(operate.index);
            var indexCardsNum = playerCards.AddHandCards(operate.cards);
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            match.setDiZhuCards(operate.cards);
            match.setDiZhuIndex(operate.index);
            if (operate.index == match.mindex) match.recorder.incrCount(operate.cards);
            panel.gameView.stage.refresh();
            panel.gameView.status.setCardNum(operate.index, indexCardsNum.count);
            panel.gameView.hand.refresHands(operate.index, playerCards.getSortHandCards());
            endstage();
            operate.playOver();
            panel.gameView.operate.showOperateView(panel.operate);
        }

        private void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.JIABEI ||
                operate.operateData.stage == DDZSTAGE.MATCH ||
                operate.operateData.stage == DDZSTAGE.MINGPAI)
            {

                match.selectCard.clear();
                panel.gameView.hand.cancelAllSelect();
                panel.gameView.desktop.hideCards(true);
                panel.gameView.status.showBanker(match.diZhuIndex);
                panel.gameView.landlordcards.setData(match.lordCards);
                panel.gameView.clock.showClock(operate.operateData.round);
                panel.topView.setMuitplier(match.grossMuitiplier);
            }
        }
    }
}