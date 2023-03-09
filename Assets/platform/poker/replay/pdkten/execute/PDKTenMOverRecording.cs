namespace platform.poker
{
    public class PDKTenMOverRecording : Process
    {
        private PDKTenMOverOperate operate;

        public PDKTenMOverRecording(object operate)
        {
            this.operate = (PDKTenMOverOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = PDKTenMatch.match;
            var rPanel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();
            var sPanel = UnrealRoot.root.getDisplayObject<PDKTenOverSinglePanel>();

            room.setStatus(Room.STATE_DESTORY, true);                              

            match.setBase(operate.operateData.type,       
                      operate.operateData.stage,        
                      operate.operateData.paidui,        
                      operate.operateData.step,         
                      operate.operateData.round,        
                      operate.operateData.operates);     

            for (int i = 0; i < match.players.Length; i++)                        
            {
                rPanel.gameView.status.setCardNum(i, operate.playerCard[i].Length);
                if (operate.playerCard[i].Length != 0)
                {
                    rPanel.gameView.hand.refresHands(true, i, CardSort.LTSCards(operate.playerCard[i]));
                }
            }

            rPanel.gameView.recorder.setData(match.recorder.getRemainCount());     
            rPanel.gameView.clock.setVisible(false);                              
            rPanel.headView.updateSocre();                                       

            sPanel.setData(operate.scores, operate.springPoint, operate.bombScores, operate.cardsLength, operate.surplusCards, operate.playerCard, operate.handcardsuse);
            sPanel.refresh();
            sPanel.changeLayer();
            operate.playOver();
        }
    }
}