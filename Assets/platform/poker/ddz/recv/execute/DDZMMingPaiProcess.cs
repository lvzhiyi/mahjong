using platform.bean;

namespace platform.poker
{
    public class DDZMingPaiPoint
    {
        public const int DEAL_POINT = 2;
        public const int SHOW_POINT = 4;
    }

    public class DDZMMingPaiProcess : Process
    {
        DDZMMingPaiOperate operate;

        PKRoomPanel panel;

        DDZMatch match;

        public DDZMMingPaiProcess(object operate)
        {
            this.operate = (DDZMMingPaiOperate)operate;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();

            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            match.selectCard.clear();                                                               
            match.getPCardIndex(operate.index).handcards.clear();                                 
            match.getPCardIndex(operate.index).handcards.add(operate.cards);                        
            match.recorder.addMPUserIndex(operate.index);                                         
            if (match.mindex != operate.index) match.recorder.incrCount(operate.cards);            

            panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.MING_PAI);          

            endstage();

            operate.playOver();

            panel.gameView.operate.showOperateView(panel.operate, true);
        }

        void endstage()
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
