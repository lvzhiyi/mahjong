using cambrian.common;

namespace basef.arena
{
    /// <summary> 竞赛场 消息界面 类型选择按钮 </summary> 选择管理需要显示的内容
    public class ArenaMsgSelectTypeProcess : MouseClickProcess
    {
        private ArenaMsgPanel panel;
        int type;

        public override void execute()
        {
            //发送消息
            type = this.transform.parent.GetComponent<ArenaMsgTypeBar>().type;
            showView(type);
        }

        public void showView(int type)
        {
            
            switch (type)
            {
                case ArenaMsgPanel.Msg_ArenaApply:
                    CommandManager.addCommand(new GetArenaMsgApplyContentListCommand(ArenaEvent.EVENT_TYPE_MEMBER),back);
                    break;
                case ArenaMsgPanel.Msg_ChangeApply:
                    CommandManager.addCommand(new GetArenaMemeberChangeRecordCommand(0),back);
                    break;
                case ArenaMsgPanel.Msg_RoomTimeOut:
                    CommandManager.addCommand(new GetArenaMsgTimeOutListCommand(ArenaEvent.EVENT_TYPE_ROOM),back);
                    break;
                case ArenaMsgPanel.Msg_ArenaAuxiliaryScore:
                    CommandManager.addCommand(new GetArenaMsgAuxiliaryScoreRecordCommand(0), back);
                    break;
                case ArenaMsgPanel.Msg_ArenaPersonalJournal:
                    CommandManager.addCommand(new GetArenaPersonalJournalRecordCommand(), back);
                    break;
            }
        }

        /// <summary> 回调 </summary>
        private void back(object obj)
        {
            if (obj == null) return;
            panel = getRoot<ArenaMsgPanel>();
            panel.refresh();
            panel.managerView.showView(type,obj);
            panel.updateBars(type);
        }
    }
}
