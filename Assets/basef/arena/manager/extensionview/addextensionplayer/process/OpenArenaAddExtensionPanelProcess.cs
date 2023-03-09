using basef.arena;
using cambrian.common;

namespace arena.manager
{
    /// <summary>
    /// 打开增加推广员界面
    /// </summary>
    public class OpenArenaAddExtensionPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaMembersListCommand(GetArenaMembersListCommand.SELECT_SERVER_MEMBERS,false), back);
        }

        public void back(object obj)
        {
            if (obj == null)
                return;
            ArenaAddExtensionPanel panel = UnrealRoot.root.getDisplayObject<ArenaAddExtensionPanel>();
            ArenaMember[] members = (ArenaMember[])obj;
            panel.setData(members);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
