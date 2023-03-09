namespace platform.mahjong
{
    /// <summary>
    /// 游戏开始
    /// </summary>
    public class AYMJMatchStartRecording : Process
    {
        AYMJMatchStartOperate operate;

        int selfOperate;

        public AYMJMatchStartRecording(MJBaseOperate bOperate)
        {
            operate = (AYMJMatchStartOperate)bOperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            Room room = Room.room;
            room.roomRule.setGameNum(operate.tray);
            room.cancelReady();

            AYMJMatch.match = new AYMJMatch(room.getPlayerCount(), operate.step, operate.banker, room.getMasterIndex(), operate.paidui);

            AYMJMatch.match.setPlayers(room.players, room.indexOf());
            AYMJMatch.match.setRoomRule(room.roomRule);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(operate.getSelfOperate());
            panel.refresh();
            panel.showGameView();
            panel.refreshCardNum();
            panel.gameView.getMjIndexCenterView().setJuShu(room.roomRule.getNowsPlayNum(),room.roomRule.getMatchCount());
            panel.showCountTime(operate.round);

            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }

      

      
    }
}
