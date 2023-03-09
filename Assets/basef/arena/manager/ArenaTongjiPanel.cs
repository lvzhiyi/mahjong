using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场统计界面
    /// </summary>
    public class ArenaTongjiPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 奖励统计0，房间统计1
        /// </summary>
        public const int TONGJI_AWARD = 0, TONGJI_ROOM = 1;

        /// <summary>
        /// 擂主名称类型
        /// </summary>
        string[] masterNametype = new string[]
        {
            "奖励统计","房间统计"
        };

        /// <summary>
        /// 合伙人名称类型
        /// </summary>
        string[] nametype = new string[]
        {
            "奖励统计"
        };

        private int selectType = 0;

        private ScrollContainer container;

        /// <summary>
        /// 奖励统计
        /// </summary>
        ArenaTicketManagerView awardView;
        /// <summary>
        /// 房间统计
        /// </summary>
        ArenaStatisticsView roomView;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("leftType").GetComponent<ScrollContainer>();
            container.init();
            awardView = content.Find("content").Find("view").Find("ticketview").GetComponent<ArenaTicketManagerView>();
            awardView.init();
            roomView = content.Find("content").Find("view").Find("statisticsview").GetComponent<ArenaStatisticsView>();
            roomView.init();
        }

        protected override void xrefresh()
        {
            Arena arena = Arena.arena;
            ArenaMember member = arena.getMember();
            bool isMaster = arena.isMaster(member.userid);
            bool isAssistant = member.hasPerms(32) && arena.isMaster(member.master);// 是副擂主且有统计管理权限
            if (isMaster||isAssistant)
            {
                container.updateData(callback);
                container.resize(masterNametype.Length);
                container.resizeDelta();
            }
            else
            {
                container.updateData(callback1);
                container.resize(nametype.Length);
                container.resizeDelta();
            }

            updateSelect(0);
        }

        public void callback(ScrollBar bar,int index)
        {
            ArenaLeftTypeBar temp = (ArenaLeftTypeBar)bar;
            temp.setData(masterNametype[index],selectType);
            temp.index_space = index;
            temp.refresh();
        }
        public void callback1(ScrollBar bar,int index)
        {
            ArenaLeftTypeBar temp = (ArenaLeftTypeBar)bar;
            temp.setData(nametype[index],selectType);
            temp.index_space = index;
            temp.refresh();
        }

        public void updateSelect(int type)
        {
            selectType = type;
            for (int i = 0; i < this.container.bars.count; i++)
            {
                ArenaLeftTypeBar bar = (ArenaLeftTypeBar)this.container.bars.get(i);
                bar.setSelected(type);
                bar.refresh();
            }
            awardView.gameObject.SetActive(false);
            roomView.gameObject.SetActive(false);
            if (type==TONGJI_AWARD)
            {
                long endtime = TimeKit.getTodayServerStart();
                long starttime = endtime - 4 * TimeKit.DAY_MILLS;
                CommandManager.addCommand(new GetArenaTicketBillListCommand(starttime, endtime, 0), obj=>
                {
                    if (obj == null) return;
                    object[] objs = (object[])obj;
                    TicketBill[] bills = (TicketBill[])objs[0];
                    awardView.setData(bills, starttime, endtime, (TicketTimeSoltTotal)objs[1]);
                    awardView.refresh();
                    awardView.setVisible(true);
                });
            }
            else
            {
                CommandManager.addCommand(new GetArenaStatistcsListDataCommand(Arena.arena.getId()), obj=>
                {
                    if (obj == null) return;
                    ArrayList<ArenaStatistcsBarData> list = (ArrayList<ArenaStatistcsBarData>)obj;
                    roomView.setData(list);
                    roomView.refresh();
                    roomView.gameObject.SetActive(true);
                });
            }
        }
    }
}
