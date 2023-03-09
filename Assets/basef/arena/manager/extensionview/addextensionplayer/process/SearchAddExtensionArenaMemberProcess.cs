using cambrian.common;

namespace basef.arena
{
    public class SearchAddExtensionArenaMemberProcess:MouseClickProcess
    {
        private ArenaAddExtensionPanel panel;
        public override void execute()
        {
            panel = getRoot<ArenaAddExtensionPanel>();
            if (panel.searchid.text == null || panel.searchid.text.Length == 0) return;
            long userid = StringKit.parseLong(panel.searchid.text);
            CommandManager.addCommand(new SearchArenaMemberCommand(Arena.arena.getId(), userid, false), back);
        }

        public void back(object obj)
        {
            if (obj == null)
                return;
            panel.setSearchMemeber((ArenaMember)obj);
        }
    }
}
