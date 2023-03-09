using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 选择赛场管理类型
    /// </summary>
    public class ArenaRunSelectTypeProcess : MouseClickProcess
    {
        private ArenaRunView runview;
        public override void execute()
        {
            int index = GetComponent<ArenaRunTypesBar>().index_space;
            runview= transform.parent.parent.GetComponent<ArenaRunView>();

            runview.showTypes(index);

            if (index == 0)
            {
                runview.awardView.refresh();
                runview.awardView.setVisible(true);
                runview.formView.setVisible(false);
            }
            else
            {
                CommandManager.addCommand(new GetArenaGoldStatsCommand(Arena.arena.getId()),back);
                
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
            runview.formView.setData((ArenaForm)obj);
            runview.formView.refresh();
            runview.formView.setVisible(true);
            runview.awardView.setVisible(false);
        }
    }

}
