using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary> 邀请界面 添加玩家 </summary>
    public class ArenaInvitationAddUserProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaInvitationContentBar bar = transform.parent.GetComponent<ArenaInvitationContentBar>();
            SimplePlayer player = getRoot<ArenaInvitationPanel>().player;
            if (player == null)
            {
                Alert.show("未找到指定玩家！");
                return;
            }

            CommandManager.addCommand(new ArenaAddPlayerCommand(Arena.arena.getId(), player.userid), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            bool result = (bool)obj;
            if (result)
            {
                Alert.show("邀请成功");
            }
            ArenaInvitationPanel panel = getRoot<ArenaInvitationPanel>();
            panel.setData(null);
            panel.refresh();
        }
    }
}