using cambrian.common;

namespace basef.arena
{
    /// <summary> 获取奖章明细列表 </summary>
    class GetArenaAccountsMedalListProcess : MouseClickProcess
    {
        ArenaAccountsMedalPanel panel;

        public override void execute()
        {
            if (panel == null) panel = getRoot<ArenaAccountsMedalPanel>();
            int type = panel.getType();
            CommandManager.addCommand(new GetArenaAccountsMedalListCommand(panel.userid,type),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            //panel.setData((ArrayList<ArenaMedalBill>)obj);
            panel.refresh();
        }
    }
}
