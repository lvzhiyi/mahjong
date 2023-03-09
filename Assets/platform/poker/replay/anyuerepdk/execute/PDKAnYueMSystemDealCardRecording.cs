namespace platform.poker
{
    public class PDKAnYueMSystemDealCardRecording : Process
    {
        private PDKMSystemDealCardOperate operate;

        private int selfOperate;

        public PDKAnYueMSystemDealCardRecording(object operate)
        {
            this.operate = (PDKMSystemDealCardOperate)operate;
            selfOperate = this.operate.getSelfOperate();
        }

        public override void execute()
        {
            var match = PDKAnYueMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();

            match.setBase(operate.operateData.type,
                  operate.operateData.stage,
                  operate.operateData.paidui,
                  operate.operateData.step,
                  operate.operateData.round,
                  operate.operateData.operates);
            match.DealCards(operate.cards);                                     
            match.recorder.incrCount(operate.cards[match.mindex]);             
            panel.operate = operate.operateData.operates[match.mindex];        
            panel.gameView.recorder.setData(match.recorder.getRemainCount());   
            panel.gameView.recorder.setVisible(false);                         
            panel.gameView.status.hideBanker();                               
            panel.gameView.clock.setVisible(false);                           
            panel.gameView.clock.setIndex(operate.operateData.round);         
            panel.topView.setMuitplier(PDKAnYueMatch.match.multiple.boomPoint);     
            for (int i = 0; i < match.players.Length; i++)                      
            {
                panel.gameView.status.setCardNum(i, operate.cards[i].Length);
                if (operate.cards[i].Length != 0)
                {
                    panel.gameView.hand.refresHands(true, i, CardSort.LTSCards(operate.cards[i]));
                }
            }
            operate.playOver();
        }
    }
}