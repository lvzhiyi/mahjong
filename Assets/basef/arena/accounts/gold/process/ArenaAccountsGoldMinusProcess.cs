using cambrian.common;

namespace basef.arena
{
    /// <summary> 点击筛选 金豆流水不足 </summary>
    public class ArenaAccountsGoldMinusProcess : MouseClickProcess
    {
        ArenaAccountsGoldPanel panel;

        public override void execute()
        {
            if (panel == null) panel = getRoot<ArenaAccountsGoldPanel>();
            int type = panel.getType();
            bool checkGoldminus = !panel.getCheckGoldminus();
            CommandManager.addCommand(
                new GetArenaAccountsGoldListCommand(panel.userid,type,checkGoldminus),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setCheckGoldminus();
            panel.setData((ArrayList<ArenaAccountsGoldContentData>)obj);
            panel.refresh();
        }
    }
}
