using cambrian.common;
using platform.spotred.over;
using platform.spotred.waitroom;

namespace platform.spotred.room
{
    public class OperateOverProcess:MouseClickProcess
    {
        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            this.root.setVisible(false);

            //判断全部盘数是否完成 
            if (Room.room.roomRule.isOver())
            {
                AllOverPanel all_panel = UnrealRoot.root.getDisplayObject<AllOverPanel>();
                all_panel.show(Room.room.clone());
                UnrealRoot.root.showPanel<AllOverPanel>();
            }
            //下一局
            else
            {
                SpotWaitRoomPanel r_panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                r_panel.setVisible(true);
                CommandManager.addCommand(new PlayerReadyCommand(true, Room.room.getRoomIndex()));
                r_panel.refresh();
                panel.setVisible(false);
            }
        }

        public void execute1()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            this.root.setVisible(false);

            //判断全部盘数是否完成 
            if (Room.room.roomRule.isOver())
            {
                AllOverPanel all_panel = UnrealRoot.root.getDisplayObject<AllOverPanel>();
                all_panel.show(Room.room.clone());
                UnrealRoot.root.showPanel<AllOverPanel>();
            }
            //下一局
            else
            {
                SpotWaitRoomPanel r_panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                r_panel.setVisible(true);
                CommandManager.addCommand(new PlayerReadyCommand(true, Room.room.getRoomIndex()));
                r_panel.refresh();
                panel.setVisible(false);
            }
        }
    }
}
