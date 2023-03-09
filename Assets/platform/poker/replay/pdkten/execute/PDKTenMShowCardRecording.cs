using platform.bean;

namespace platform.poker
{
    public class PDKTenMShowCardRecording : Process
    {
        private PDKTenMShowCardOperate operate;

        private PPDKTenReplayRoomPanel panel;

        private PDKTenMatch match;

        public PDKTenMShowCardRecording(object operate)
        {
            this.operate = (PDKTenMShowCardOperate)operate;
        }

        public PDKTenMShowCardRecording(OperateData operateData, int index)
        {
            operate = new PDKTenMShowCardOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            match = PDKTenMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();

            match.setBase(operate.operateData.type,
                   operate.operateData.stage,
                   operate.operateData.paidui,
                   operate.operateData.step,
                   operate.operateData.round,
                   operate.operateData.operates);

            special();

            panel.gameView.clock.showClock(operate.operateData.round);
            if ((operate != null && operate.cardsinfo != null && operate.cardsinfo.cards.Length != 10) || (operate != null && operate.cardsinfo == null))
                panel.gameView.desktop.hideCards(operate.operateData.round);                

            operate.playOver();
        }
                                                
        private void special()
        {
            if (operate.isType(PDKMatchMsg.CANCEL))
            {
                match.deskCard.states[operate.index] = -1;                               
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.PASS, true);  
                endstage();                                                             
            }
            else
            {
                var playerCards = match.getPCardIndex(operate.index);                     

                if (match.mindex != operate.index)
                {
                    match.recorder.incrCount(operate.cardsinfo.cards);                    
                    panel.gameView.recorder.setData(match.recorder.getRemainCount());   
                }

                match.multiple.changeBoom(operate.cardsinfo);                            
                match.deskCard.addReplay(operate.cardsinfo);                            

                playerCards.delHandCards(operate.cardsinfo.cards);                     
                panel.gameView.status.showCardWarning(operate.index, playerCards.handcards.count);
                int[] cards = playerCards.getSortHandCards();                           

                panel.topView.setMuitplier(match.multiple.boomPoint);                    
                panel.gameView.status.setCardNum(operate.index, cards.Length);           
                panel.gameView.hand.refresHands(operate.index, cards);                  
            }
        }
                                                     
        private void endstage()
        {
            int num = 0;
            for (int i = 0; i < match.playerCount; i++)
            {
                if (operate.operateData.round != i)
                {
                    num = (match.deskCard.getStates(i) == 0) ? num + 1 : num;
                    if (num >= 2)
                    {
                        panel.gameView.stage.hidePStatus(true);                  
                        match.deskCard.statesClaer();
                        return;
                    }
                }
            }
        }
    }
}