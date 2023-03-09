namespace basef.arena
{
    /// <summary>
    /// 打开操作视图
    /// </summary>
    public class OpenArenaMemberOperateViewProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaMemberBar memberBar= transform.parent.GetComponent<ArenaMemberBar>();
            ArenaManagerPanel panel= UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            ArenaMemberBar bar=transform.parent.GetComponent<ArenaMemberBar>();
            panel.managerView.memberView.memberDetailView.setData(bar.member);
            panel.managerView.memberView.memberDetailView.refresh();
            panel.managerView.memberView.memberDetailView.setCallBack = memberBar.callback;
            panel.managerView.memberView.memberDetailView.setVisible(true);
            panel.managerView.memberView.memberDetailView.setArenaManagerBar(bar);
        }
    }
}
