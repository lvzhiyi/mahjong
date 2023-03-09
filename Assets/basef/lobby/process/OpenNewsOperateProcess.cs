using basef.newsrun;


namespace basef.lobby
{
    /// <summary>
    /// 大厅界面打开新华网运营界面按钮
    /// </summary>
    public class OpenNewsOperateProcess : MouseClickProcess
    {

        public override void execute()
        {
            UnrealRoot.root.getDisplayObject<NewsRunPanel>().refresh();
            UnrealRoot.root.getDisplayObject<NewsRunPanel>().setCVisible(true);
        }
    }
}