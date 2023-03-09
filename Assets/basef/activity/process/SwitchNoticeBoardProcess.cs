namespace basef.activity
{
    public class SwitchNoticeBoardProcess : MouseClickProcess
    {
        public override void execute()
        {
            NoticeBoardPanel panel = getRoot<NoticeBoardPanel>();
            NoticeBoardBar bar = GetComponent<NoticeBoardBar>();
            panel.checkContainerBar(bar.index_space);
            panel.gongGaoView.setNotice(bar.getNotcie());
            panel.gongGaoView.refresh();
        }
    }
}
