using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开推广设置
    /// </summary>
    public class OpenArenRuleAccountPanelProcess : MouseClickProcess
    {
        private ArenaExtensionNextBar bar;
        public override void execute()
        {
            bar = transform.parent.GetComponent<ArenaExtensionNextBar>();
            CommandManager.addCommand(new GetRuleAccountCommand(Arena.arena.getId(), bar.extension.userid), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaRuleAccountcs> list = (ArrayList<ArenaRuleAccountcs>)obj;
            ArenRuleAccountPanel panel = UnrealRoot.root.getDisplayObject<ArenRuleAccountPanel>();
            panel.setRebate(list, bar.extension.userid);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
