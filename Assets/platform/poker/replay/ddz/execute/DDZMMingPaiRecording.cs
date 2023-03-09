namespace platform.poker
{
    public class DDZMMingPaiRecording : Process
    {
        private DDZMMingPaiOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMMingPaiRecording(object operate)
        {
            this.operate = (DDZMMingPaiOperate)operate;
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

            match.selectCard.clear();                                                               
            match.getPCardIndex(operate.index).handcards.clear();                                
            match.getPCardIndex(operate.index).handcards.add(operate.cards);                        
            match.recorder.addMPUserIndex(operate.index);                                         
            if (match.mindex != operate.index) match.recorder.incrCount(operate.cards);           

            panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.MING_PAI, true);     

            endstage();

            operate.playOver();
        }
                                                    
        private void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.MATCH)
            {
                match.multipleBean.changeMingPai(DDZMingPaiPoint.DEAL_POINT);                                
                panel.gameView.hand.refresHands(operate.index, CardSort.LTSCards(operate.cards), true, true); 
                panel.gameView.clock.showClock(operate.operateData.round);                                    
                panel.gameView.stage.hidePStatus(true);                                                       
                panel.gameView.recorder.setData(match.recorder.getRemainCount());                            
            }
            else
            {
                match.multipleBean.changeMingPai(DDZMingPaiPoint.SHOW_POINT);                               
                panel.gameView.dealpoker.dealMingPai(operate.index, operate.cards, false);                   
            }
            panel.topView.setMuitplier(DDZMatch.match.grossMuitiplier);                                     
        }
    }
}