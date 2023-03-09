using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 选择，2房或者3房事件
    /// </summary>
    public class MJSelect2Or3FSProcess : MouseClickProcess
    {
        public int fangshu;

        public override void execute()
        {
            if (Room.room == null) return;
            MJSelectFSPanel panel = getRoot<MJSelectFSPanel>();

            int fs =7;
            if (fangshu == 2)
            {
                fs = 5; //筒，条(掩码值)
            }
            else if (fangshu == 3)
            {
                fs = 7; //筒，条，万(掩码值)
            }

            CommandManager.addCommand(new MJSelectFSCommand(panel.getPlayerCount(), fs,Room.room.getRoomIndex()));
            root.setVisible(false);
        }
    }
}
