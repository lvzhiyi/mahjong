using cambrian.common;

namespace basef.arena
{
    public class UpdateLimitRenaMemeberProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaMembersListCommand(GetArenaMembersListCommand.SELECT_SERVER_NODES, true), back);
        }

        public void back(object obj)
        {
            if (obj == null)
            {
                return;
            }
            ArenaMember[] members = (ArenaMember[]) obj;
            ArenaAddLimitDeskPanel panel = UnrealRoot.root.getDisplayObject<ArenaAddLimitDeskPanel>();
            panel.setLeftData(new ArrayList<ArenaMember>(members));
            ArenaLimitBar bar = transform.parent.GetComponent<ArenaLimitBar>();
            panel.setRigthData(bar.mutex.getList());
            panel.setIsUpdate(true, bar.mutex.id);
            panel.refresh();
            panel.setCVisible(true);
            this.root.setVisible(false);
        }
    }
}

