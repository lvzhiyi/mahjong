namespace basef.arena
{
    /// <summary>
    /// 选择竞赛场标签
    /// </summary>
    public class SelectArenaRuleLabelProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaDeskLabelBar bar= transform.GetComponent<ArenaDeskLabelBar>();
            ArenaPanel panel = getRoot<ArenaPanel>();
            panel.deskView.ruleLabelSelect(bar.index_space);
            panel.deskView.refresh();
        }
    }
}
