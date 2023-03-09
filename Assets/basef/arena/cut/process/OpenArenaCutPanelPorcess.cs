using cambrian.common;

namespace basef.arena
{
    public class OpenArenaCutPanelPorcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaListCommand(),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArrayList<Arena> list=(ArrayList<Arena>)obj;
            ArenaCutPanel panel = UnrealRoot.root.getDisplayObject<ArenaCutPanel>();
            panel.setData(list);
            panel.refresh();
            panel.setVisible(true);
        }


    }
}
