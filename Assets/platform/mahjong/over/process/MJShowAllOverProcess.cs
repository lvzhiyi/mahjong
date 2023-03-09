using cambrian.common;
using platform.spotred.waitroom;

namespace platform.mahjong
{
    public class MJShowAllOverProcess:Process
    {
        public override void execute()
        {
            MahjongRoomPanel panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();

            //判断全部盘数是否完成 
            if (Room.room.roomRule.isOver())
            {
                MJAllOverPanel all_panel = UnrealRoot.root.getDisplayObject<MJAllOverPanel>();
                all_panel.show(Room.room.clone());
                UnrealRoot.root.showPanel<MJAllOverPanel>();
            }
            //下一局
            else
            {
                CommandManager.addCommand(new PlayerReadyCommand(true, Room.room.getRoomIndex()));
                panel.showWaitView();
                panel.setVisible(true);
                UnrealRoot.root.getDisplayObject<MJOverPanel>().setVisible(false);
            }
        }
    }
}
