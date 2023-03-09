using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开赛场消息面板 </summary>
    public class OpenArenaMsgPanelProcess : MouseClickProcess
    {
        ArenaMsgPanel panel;
        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ArenaMsgPanel>();
            panel.refresh();
            UnrealRoot.root.getDisplayObject<ArenaPanel>().topView.showMsgHint(false);
            UnrealRoot.root.showPanel<ArenaMsgPanel>();
            if (!Arena.arena.getMember().isAgent() && !Arena.arena.getMember().isMaster())//普通成员
                CommandManager.addCommand(new GetArenaPersonalJournalRecordCommand(), personback);
            else
                CommandManager.addCommand(new GetArenaMsgApplyContentListCommand(ArenaEvent.EVENT_TYPE_MEMBER), back);
        }
        private void personback(object obj)
        {
            panel.managerView.showView(ArenaMsgPanel.Msg_ArenaPersonalJournal, obj);
            UnrealRoot.root.showPanel<ArenaMsgPanel>();
        }
        private void back(object obj)
        {
            panel.managerView.showView(ArenaMsgPanel.Msg_ArenaApply, obj);
            UnrealRoot.root.showPanel<ArenaMsgPanel>();
        }
    }
}
