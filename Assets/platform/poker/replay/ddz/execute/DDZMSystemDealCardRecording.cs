namespace platform.poker
{
    public class DDZMSystemDealCardRecording : Process
    {
        private DDZMSystemDealCardOperate operate;

        private int selfOperate;

        public DDZMSystemDealCardRecording(object operate)
        {
            this.operate = (DDZMSystemDealCardOperate)operate;
            selfOperate = this.operate.getSelfOperate();
        }

        public override void execute()
        {
            var match = DDZMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            match.setBase(operate.operateData.type,       
                      operate.operateData.stage,
                      operate.operateData.paidui,          
                      operate.operateData.step,
                      operate.operateData.round,
                      operate.operateData.operates);
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