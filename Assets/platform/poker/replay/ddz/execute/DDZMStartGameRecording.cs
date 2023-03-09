namespace platform.poker
{
    public class DDZMStartGameRecording : Process
    {
        private DDZMStartGameOperate operate;

        public DDZMStartGameRecording(object operate)
        {
            this.operate = (DDZMStartGameOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = DDZMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            room.setStatus(Room.STATE_MATCH, true);                             
            room.roomRule.setGameNum(operate.tray);                              

            match.replay = true;                                                
            match.SetCallScore(0);
            match.lordCardsInit(3);
            match.multipleBean.resetData();                                     
            match.setRoomRule(room.roomRule);                                    
            match.setPlayers(room.players, room.indexOf());                       
            match.setBase(operate.operateData.type,
                        operate.operateData.stage,
                        operate.operateData.paidui,
                        operate.operateData.step,
                        operate.operateData.round,
                        operate.operateData.operates);

            panel.refreshGameView();                                             
            panel.topView.recordShow(false);
            panel.topView.dwShow(false);      
            panel.gameView.recorder.setVisible(false);                           
            panel.gameView.landlordcards.setData(match.lordCards);             
            panel.gameView.hand.resetMingPai();                                      
            panel.gameView.desktop.hideCards();                                   
            panel.gameView.clock.showClock(operate.operateData.round);          

            operate.playOver();
        }
    }
}