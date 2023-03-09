using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开导入成员面板 </summary>
    public class OpenArenaImprotMembersManagerPanel : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaMenberListCommand(),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;

            ArenaImprotMembersManagerPanel panel = UnrealRoot.root.getDisplayObject<ArenaImprotMembersManagerPanel>();
            panel.setData(obj);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
