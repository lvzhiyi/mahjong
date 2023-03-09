using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开积分详细
    /// </summary>
    public class ArenAgentOperateScoreInfoProcess : MouseClickProcess
    {
        private ArenaNextExtension extension;

        ArenaAccountsGoldPanel panel;
        public override void execute()
        {
            extension = getRoot<ArenaAgentPanel>().nextView.operateAgent.extension;

            panel = UnrealRoot.root.getDisplayObject<ArenaAccountsGoldPanel>();
            int type = panel.getType();
            bool check = panel.getCheckGoldminus();
            CommandManager.addCommand(new GetArenaAccountsGoldListCommand(extension.userid,type,check,0),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaAccountsGoldContentData> list = (ArrayList<ArenaAccountsGoldContentData>)obj;
            panel.setInitData(list, extension.userid, extension.getTask());
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
