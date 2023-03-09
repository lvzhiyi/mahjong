namespace platform.poker
{
    public class PDKTenMOverProcess : Process
    {
        private PDKTenMOverOperate operate;

        public PDKTenMOverProcess(object operate)
        {
            this.operate = (PDKTenMOverOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var rPanel = UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
            var sPanel = UnrealRoot.root.getDisplayObject<PDKTenOverSinglePanel>();
            var match = PDKTenMatch.match;
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
            sPanel.setData(operate.scores, operate.springPoint, operate.bombScores, operate.cardsLength, operate.surplusCards, operate.playerCard, operate.handcardsuse);
            sPanel.refresh();                                                      
            sPanel.changeLayer();
            rPanel.topView.refresh(); 
            operate.playOver();
        }
    }
}
