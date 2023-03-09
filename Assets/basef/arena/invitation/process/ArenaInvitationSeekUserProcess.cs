using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary> 邀请界面 搜索玩家 </summary>
    public class ArenaInvitationSeekUserProcess : MouseClickProcess
    {
        private ArenaInvitationPanel panel;
		
        public override void execute()
        {
            panel = getRoot<ArenaInvitationPanel>();
            if (panel.getUserSid() == null || panel.getUserSid() == "")
            {
                Alert.show("请输入搜索玩家ID");
                return;
            }
            long usersid = StringKit.parseLong(panel.getUserSid());
            CommandManager.addCommand(new ArenaSearchPlayerCommand(usersid),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            SimplePlayer player = (SimplePlayer) obj;
            panel.setData(player);
            panel.refresh();
        }
    }
}
