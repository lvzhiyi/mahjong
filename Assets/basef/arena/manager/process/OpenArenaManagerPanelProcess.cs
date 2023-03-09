using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开管理界面
    /// </summary>
    public class OpenArenaManagerPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaManagerPanel panel= UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            panel.refresh();
            if (!Arena.arena.getMember().isMaster())
            {
                getMemberList();
            }
            else
            {
                panel.managerView.showBaseSettingView();
                UnrealRoot.root.showPanel<ArenaManagerPanel>();
            }

            panel.setLastPanel(this.root);

        }

        public void getMemberList()
        {
            CommandManager.addCommand(new GetArenaMembersListCommand(GetArenaMembersListCommand.SELECT_SERVER_NODES,true), getMemberListBack);
        }

        public void getMemberListBack(object obj)
        {
            if (obj == null) return;
            ArenaManagerPanel panel = UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            ArenaMember[] members = (ArenaMember[])obj;
            panel.managerView.showMemberView(members);
            UnrealRoot.root.showPanel<ArenaManagerPanel>();
        }
    }
}
