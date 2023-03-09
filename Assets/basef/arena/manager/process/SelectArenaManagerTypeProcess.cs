using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 选择管理需要显示的内容
    /// </summary>
    public class SelectArenaManagerTypeProcess : MouseClickProcess
    {
        private ArenaManagerPanel panel;
        public override void execute()
        {
            int type = this.transform.parent.GetComponent<ArenaManagerBar>().type;
            panel = getRoot<ArenaManagerPanel>();
            panel.updateBars(type);
            showView(type);
        }

        public void showView(int type)
        {
            
            switch (type)
            {
                case ArenaManagerPanel.ARENA_SETTING:
                    panel.managerView.refresh();
                    panel.managerView.showBaseSettingView();
                    break;
                case ArenaManagerPanel.ARENA_OPEN_TABLE:
                    getStatisticsList();
                    break;
                case ArenaManagerPanel.ARENA_TICKET:
                    getTicketBill();
                    break;
                case ArenaManagerPanel.ARENA_EXTENSION:
                    panel.managerView.refresh();
                    panel.managerView.showExtensionView();
                    break;
                case ArenaManagerPanel.ARENA_MEMBER:
                    getMemberList();
                    break;
                case ArenaManagerPanel.ARENA_MANAGE:
                    panel.managerView.refresh();
                    panel.managerView.showRunView();
                    break;
                case ArenaManagerPanel.ARENA_WANFA:
                    panel.managerView.refresh();
                    panel.managerView.showWaFaView();
                    break;
            }
        }

        /// <summary>
        /// 获取视图统计
        /// </summary>
        public void getStatisticsList()
        {
            CommandManager.addCommand(new GetArenaStatistcsListDataCommand(Arena.arena.getId()),getStatisticsListBack);
        }

        private void getStatisticsListBack(object obj)
        {
            if (obj == null) return;
            panel.managerView.refresh();
            ArenaManagerPanel manager = UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            manager.managerView.showStatisticsView((ArrayList<ArenaStatistcsBarData>)obj);
        }

        /// <summary>
        /// 获取更多成员
        /// </summary>
        public void getMemberList()
        {
            CommandManager.addCommand(new GetArenaMembersListCommand(GetArenaMembersListCommand.SELECT_SERVER_NODES,true),getMemberListBack);
        }

        public void getMemberListBack(object obj)
        {
            if (obj == null) return;
            panel.managerView.refresh();
            ArenaManagerPanel manager = UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            ArenaMember[] members = (ArenaMember[])obj;
            manager.managerView.showMemberView(members);
        }

        private long start;
        private long endtime;
        public void getTicketBill()
        {
             endtime = TimeKit.getTodayServerStart();
             start = endtime - 4 * TimeKit.DAY_MILLS;
            CommandManager.addCommand(new GetArenaTicketBillListCommand(start,endtime,0), ticketBillback);
        }

        public void ticketBillback(object obj)
        {
            if (obj == null) return;
            panel.managerView.refresh();
            object[] objs = (object[])obj;
            TicketBill[] bill = (TicketBill[])objs[0];
            panel.managerView.showTicketManagerView(bill,start, endtime,(TicketTimeSoltTotal)objs[1]);
        }


    }
}
