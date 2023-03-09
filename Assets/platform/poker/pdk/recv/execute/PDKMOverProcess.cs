namespace platform.poker
{
    public class PDKMOverProcess : Process
    {
        private PDKMOverOperate operate;

        public PDKMOverProcess(object operate)
        {
            this.operate = (PDKMOverOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var rPanel = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
            var sPanel = UnrealRoot.root.getDisplayObject<PDKOverSinglePanel>();
            var match = PDKMatch.match;
            match.remainingCards = operate.surplusCards;
            if (room.roomRule.num + 1 < room.roomRule.getMatchCount())         
            {
                room.roomRule.setGameNum(room.roomRule.num + 1);                 
                room.setStatus(Room.STATUS_READY, true);                         
            }
            else room.setStatus(Room.STATE_DESTORY, true);                        
            for (int i = 0; i < match.players.Length; i++)                        
            {
                room.getSpotRedCount().getIndexCount(i).score += operate.scores[i];
                rPanel.gameView.status.setCardNum(i, operate.playerCard[i].Length);
            }
            rPanel.gameView.recorder.setData(match.recorder.getRemainCount());     
            rPanel.gameView.operate.hideOperateView();                          
            rPanel.gameView.clock.setVisible(false);                             
            rPanel.headView.updateSocre();                                       
            sPanel.setData(operate.bird10Index, operate.scores, operate.springPoint, operate.bombScores, operate.cardsLength, operate.surplusCards, operate.playerCard, operate.handcardsuse);
            sPanel.refresh();                                                      
            sPanel.changeLayer();
            rPanel.topView.refresh(); 
            operate.playOver();
        }
    }
}
