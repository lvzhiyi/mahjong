namespace platform.poker
{
    public class PDKTenMStartGameRecording : Process
    {
        private PDKTenMStartGameOperate operate;

        public PDKTenMStartGameRecording(object operate)
        {
            this.operate = (PDKTenMStartGameOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = PDKTenMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();

            room.setStatus(Room.STATE_MATCH, true);                            
            room.roomRule.setGameNum(operate.tray);                             

            match.replay = true;                                                
            match.multiple.reset();                                             
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
            panel.gameView.hand.resetMingPai();                                 
            panel.gameView.desktop.hideCards();                                  
            panel.gameView.clock.showClock(operate.operateData.round);          

            operate.playOver();
        }
    }
}