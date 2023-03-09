using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开新增限制组面板
    /// </summary>
    public class OpenAddLimitDeskPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaMembersListCommand(GetArenaMembersListCommand.SELECT_SERVER_NODES,true),back);
        }

        public void back(object obj)
        {
            if (obj==null)
            {
                return;
            }
            ArenaMember[] members = (ArenaMember[])obj;
            ArenaAddLimitDeskPanel panel = UnrealRoot.root.getDisplayObject<ArenaAddLimitDeskPanel>();
            panel.setLeftData(new ArrayList<ArenaMember>(members));
            panel.setRigthData(new ArrayList<ArenaMutexMember>());
            panel.setIsUpdate(false,0);
            panel.refresh();
            panel.setCVisible(true);
            this.root.setVisible(false);
        }
    }
}
