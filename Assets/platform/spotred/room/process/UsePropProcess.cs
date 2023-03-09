using cambrian.common;
using platform.spotred;
using basef.player;

namespace platform.spotred.room
{
    /// <summary>
    /// 使用道具
    /// </summary>
    public class UsePropProcess:MouseClickProcess
    {
        public override void execute()
        {
            PlayerPropInfoPanel panel= this.getRoot<PlayerPropInfoPanel>();
            long userid=panel.player.userid;
            if (userid == Player.player.userid) return;

            PlayerPropInfoBar bar= GetComponent<PlayerPropInfoBar>();
            if (Room.room == null) return;
            int index = Room.room.getPlayerIndex(userid);
            CommandManager.addCommand(new UsePropCommand(index,bar.prop.sid));
            panel.setVisible(false);
        }
    }
}
