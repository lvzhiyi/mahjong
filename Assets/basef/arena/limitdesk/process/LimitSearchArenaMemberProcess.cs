using cambrian.common;

namespace basef.arena
{
    public class LimitSearchArenaMemberProcess:MouseClickProcess
    {
        private ArenaAddLimitDeskPanel panel;
        public override void execute()
        {
            panel=getRoot<ArenaAddLimitDeskPanel>();
            if (panel.searchid.text == null || panel.searchid.text.Length == 0) return;
            long userid= StringKit.parseLong(panel.searchid.text);
            CommandManager.addCommand(new SearchArenaMemberCommand(Arena.arena.getId(),userid,true),back);
        }

        public void back(object obj)
        {
            if (obj == null)
                return;
            ArenaMember member=(ArenaMember)obj;
            panel.setSearchMemeber(member);

        }
    }
}
