namespace platform.poker
{
    public class PDKMOverRecording : Process
    {
        private PDKMOverOperate operate;

        public PDKMOverRecording(object operate)
        {
            this.operate = (PDKMOverOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = PDKMatch.match;
            var rPanel = UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>();
            var sPanel = UnrealRoot.root.getDisplayObject<PDKOverSinglePanel>();

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

            sPanel.setData(operate.bird10Index, operate.scores, operate.springPoint, operate.bombScores, operate.cardsLength, operate.surplusCards, operate.playerCard, operate.handcardsuse);
            sPanel.refresh();
            sPanel.changeLayer();
            operate.playOver();
        }
    }
}