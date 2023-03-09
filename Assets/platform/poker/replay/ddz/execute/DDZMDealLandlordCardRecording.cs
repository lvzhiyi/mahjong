using cambrian.game;

namespace platform.poker
{
    public class DDZMDealLandlordCardRecording : Process
    {
        private DDZMDealLandlordCardOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMDealLandlordCardRecording(object operate)
        {
            this.operate = (DDZMDealLandlordCardOperate)operate;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            var playerCards = match.getPCardIndex(operate.index);
            var indexCardsNum = playerCards.AddHandCards(operate.cards);

            match.setBase(operate.operateData.type,
                      operate.operateData.stage,
                      operate.operateData.paidui,
                      operate.operateData.step,
                      operate.operateData.round,
                      operate.operateData.operates);

            match.setDiZhuCards(operate.cards);
            match.setDiZhuIndex(operate.index);

            if (operate.index == match.mindex) match.recorder.incrCount(operate.cards);   

            panel.gameView.stage.refresh();                                               
            panel.gameView.status.setCardNum(operate.index, indexCardsNum.count);          
            panel.gameView.hand.refresHands(operate.index, playerCards.getSortHandCards()); 

            endstage();

            panel.gameView.operate.showOperateView(panel.operate);

            operate.playOver();
        }

        /// <summary> 阶段结束操作 </summary>
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
