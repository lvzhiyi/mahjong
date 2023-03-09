using platform.bean;

namespace platform.poker
{
    public class DDZMShowCardRecording : Process
    {
        private DDZMShowCardOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMShowCardRecording(object operate)
        {
            this.operate = (DDZMShowCardOperate)operate;
        }

        public DDZMShowCardRecording(OperateData operateData, int index)
        {
            operate = new DDZMShowCardOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            match.setBase(operate.operateData.type,
                       operate.operateData.stage,
                       operate.operateData.paidui,
                       operate.operateData.step,
                       operate.operateData.round,
                       operate.operateData.operates);

            special();

            panel.gameView.clock.showClock(operate.operateData.round);                   
            panel.gameView.desktop.hideCards(operate.operateData.round);                 

            operate.playOver();
        }

        /// <summary> 特殊操作 </summary>
        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
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

                match.multipleBean.changeBoom(operate.cardsinfo.cards);                  
                match.deskCard.addReplay(operate.cardsinfo);                            

                playerCards.delHandCards(operate.cardsinfo.cards);                      
                panel.gameView.status.showCardWarning(operate.index, playerCards.handcards.count);
                int[] cards = playerCards.getSortHandCards();                           

                panel.topView.setMuitplier(match.grossMuitiplier);                        
                panel.gameView.status.setCardNum(operate.index, cards.Length);            
                panel.gameView.hand.refresHands(operate.index, cards);                   
            }
        }

        /// <summary> 阶段结束时操作 </summary>
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
                        panel.gameView.stage.hidePStatus(true);                       //隐藏所有人的状态
                        match.deskCard.statesClaer();
                        return;
                    }
                }
            }
        }
    }
}