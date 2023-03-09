using cambrian.common;

namespace basef.arena
{
    /// <summary> 获取金豆明细列表 </summary>
    public class GetArenaAccountsGoldListProcess : MouseClickProcess
    {
        ArenaAccountsGoldPanel panel;

        public override void execute()
        {
            if (panel == null) panel = getRoot<ArenaAccountsGoldPanel>();
            int type = panel.getType();
            bool checkGoldminus = panel.getCheckGoldminus();
            CommandManager.addCommand(
                new GetArenaAccountsGoldListCommand(panel.userid,type,checkGoldminus),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaAccountsGoldContentData>)obj);
            panel.refresh();
        }
    }
}
