using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 新增推广员
    /// </summary>
    public class ArenaAddExtensionProcess:MouseClickProcess
    {
        //private ArenaAddExtensionBar bar;

        private ArenaMember member;

        public override void execute()
        {
            //bar =transform.parent.GetComponent<ArenaAddExtensionBar>();
            //CommandManager.addCommand(new ArenaAddExtensionCommand(bar.member.userid),back);
            member = getRoot<ArenaAddExtensionPanel>().member;
            if(member==null)
            {
                Alert.autoShow("未找到该玩家！");
                return;
            }
            CommandManager.addCommand(new ArenaAddExtensionCommand(member.userid), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            Alert.autoShow("已设置为合伙人！");
            //getRoot<ArenaAddExtensionPanel>().remove(bar.member);
            getRoot<ArenaAddExtensionPanel>().remove(member);          
            CommandManager.addCommand(new GetArenaAgentDetailListCommand(), detailListback);
        }

        public void detailListback(object obj)
        {
            if (obj == null) return;
            object[] objs = (object[])obj;
            ArenaAgentPanel panel= UnrealRoot.root.getDisplayObject<ArenaAgentPanel>();
            panel.nextView.setData(objs);
            panel.nextView.refresh();
            this.root.setCVisible(false);
        }
    }
}
