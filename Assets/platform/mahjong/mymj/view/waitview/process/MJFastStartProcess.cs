namespace platform.mahjong
{
    public class MJFastStartProcess:MouseClickProcess
    {
        public override void execute()
        {
            int playerCount = Room.room.getRoomRealPlayerCount();
            int len = Room.room.players.Length;
            if (playerCount == 1 || playerCount == len)
            {
                Alert.autoShow("当前人数不符合快速开局条件!");
                return;
            }

            MJSelectFSPanel panel= UnrealRoot.root.getDisplayObject<MJSelectFSPanel>();
            panel.setPlayerCount(playerCount);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
