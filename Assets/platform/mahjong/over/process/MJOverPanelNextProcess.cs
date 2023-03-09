using cambrian.common;
using platform.spotred.waitroom;

namespace platform.mahjong
{
    /// <summary>
    /// 单局结算，下一局操作
    /// </summary>
    public class MJOverPanelNextProcess : MouseClickProcess
    {
        public override void execute()
        {
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
                if (Room.room != null)
                {
                    int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);
                   
                    if (gametype == GamePanelData.AY_GAME)
                    {
                        AYMJRoomPanel panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
                        panel.showWaitView();
                        panel.setVisible(true);
                    }
                    else if (Room.room.getRule().getPlatFormValue() == GamePlatform.MJ_GAME)
                    {
                        MahjongRoomPanel panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
                        panel.showWaitView();
                        panel.setVisible(true);
                    }
                }
                CommandManager.addCommand(new PlayerReadyCommand(true, Room.room.getRoomIndex()));
                this.root.setVisible(false);
            }
        }
    }
}
