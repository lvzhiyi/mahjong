using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 总的管理视图
    /// </summary>
    public class ArenaManagerView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 赛场设置
        /// </summary>
        [HideInInspector] public ArenaSettingView baseSettingView;
        /// <summary>
        /// 统计视图
        /// </summary>
        private ArenaStatisticsView statisticsView;
        /// <summary>
        /// 门票管理
        /// </summary>
        private ArenaTicketManagerView ticketManagerView;
        /// <summary>
        /// 推广管理
        /// </summary>
        [HideInInspector] public ArenaExtensionView extensionView;
        /// <summary>
        /// 会员视图
        /// </summary>
        [HideInInspector] public ArenaMemberView memberView;
        /// <summary>
        /// 管理视图
        /// </summary>
        public ArenaRunView runView;
        /// <summary>
        /// 玩法视图
        /// </summary>
        [HideInInspector] public ArenaWaFaView waFaView;

        protected override void xinit()
        {
            baseSettingView = transform.Find("settingview").GetComponent<ArenaSettingView>();
            baseSettingView.init();
            statisticsView = transform.Find("statisticsview").GetComponent<ArenaStatisticsView>();
            statisticsView.init();
            ticketManagerView = transform.Find("ticketview").GetComponent<ArenaTicketManagerView>();
            ticketManagerView.init();
            extensionView = transform.Find("extensionview").GetComponent<ArenaExtensionView>();
            extensionView.init();
            memberView = transform.Find("memberview").GetComponent<ArenaMemberView>();
            memberView.init();
            runView = transform.Find("arenarunview").GetComponent<ArenaRunView>();
            runView.init();
            waFaView = transform.Find("wanfaview").GetComponent<ArenaWaFaView>();
            waFaView.init();
        }

        protected override void xrefresh()
        {
            baseSettingView.setVisible(false);
            statisticsView.setVisible(false);
            ticketManagerView.setVisible(false);
            extensionView.setVisible(false);
            memberView.setVisible(false);
            runView.setVisible(false);
            waFaView.setVisible(false);
        }

        /// <summary>
        /// 显示基础设置
        /// </summary>
        public void showBaseSettingView()
        {
            baseSettingView.refresh();
            baseSettingView.setVisible(true);
        }

        /// <summary>
        /// 显示统计视图
        /// </summary>
        public void showStatisticsView(ArrayList<ArenaStatistcsBarData> list)
        {
            statisticsView.setData(list);
            statisticsView.refresh();
            statisticsView.setVisible(true);
        }

        /// <summary>
        /// 显示门票管理
        /// </summary>
        public void showTicketManagerView(TicketBill[] bills,long starttime,long endtime, TicketTimeSoltTotal totals)
        {
            ticketManagerView.refresh();
            ticketManagerView.setData(bills,starttime,endtime,totals);
            ticketManagerView.setVisible(true);
        }

        /// <summary>
        /// 显示推广管理
        /// </summary>
        public void showExtensionView()
        {
            extensionView.refresh();
            extensionView.setVisible(true);
        }

        /// <summary>
        /// 显示成员视图
        /// </summary>
        public void showMemberView(ArenaMember[] members)
        {
            memberView.setData(new ArrayList<ArenaMember>(members));
            memberView.refresh();
            memberView.setVisible(true);
        }

        /// <summary>
        /// 显示管理视图
        /// </summary>
        public void showRunView()
        {
            runView.refresh();
            runView.setVisible(true);
        }

        public void showWaFaView()
        {
            waFaView.refresh();
            waFaView.setVisible(true);
        }
    }
}
