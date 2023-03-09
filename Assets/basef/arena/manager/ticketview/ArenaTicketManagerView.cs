using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 门票管理视图
    /// </summary>
    public class ArenaTicketManagerView : UnrealLuaSpaceObject
    {
        private ScrollContainer container;
        /// <summary>
        /// 开始时间
        /// </summary>
        private Dropdown starttime;
        /// <summary>
        /// 选择开始时间
        /// </summary>
        [HideInInspector] public int starttimeValue;
        /// <summary>
        /// 开始时间
        /// </summary>
        public long _starttime;

        /// <summary>
        /// 结束时间
        /// </summary>
        private Dropdown endtime;

        /// <summary>
        /// 选择结束时间
        /// </summary>
        [HideInInspector] public int endtimeValue;

        /// <summary>
        /// 结束时间
        /// </summary>
        public long _endtime;
        /// <summary>
        /// 最近的今天
        /// </summary>
        private int day=5;

        /// <summary>
        /// 类型选择
        /// </summary>
        private Dropdown ruletype;

        /// <summary>
        /// 类型选择时间
        /// </summary>
        [HideInInspector] public int ruletypeValue;

        /// <summary>
        /// 今天的起始时间
        /// </summary>
        private long todaystarttime;
        /// <summary>
        /// 达标房间
        /// </summary>
        private UnrealTextField reachRoom;
        /// <summary>
        /// 总计门票
        /// </summary>
        private UnrealTextField totalTicket;
        /// <summary>
        /// 门票收益
        /// </summary>
        private UnrealTextField ticketAward;
        /// <summary>
        /// 下级门票奖励
        /// </summary>
        private UnrealTextField nextTicketAward;


        ArrayList<TicketBill> list=new ArrayList<TicketBill>();

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
            starttime = transform.Find("starttime").GetComponent<Dropdown>();
            endtime = transform.Find("endtime").GetComponent<Dropdown>();
            //ruletype = transform.Find("ruletype").GetComponent<Dropdown>();

            reachRoom = transform.Find("uproom").GetComponent<UnrealTextField>();
            reachRoom.init();
            totalTicket = transform.Find("totalticket").GetComponent<UnrealTextField>();
            totalTicket.init();
            ticketAward = transform.Find("ticketaward").GetComponent<UnrealTextField>();
            ticketAward.init();
            nextTicketAward = transform.Find("nextaward").GetComponent<UnrealTextField>();
            nextTicketAward.init();
        }


        private string[] getTime(bool isStart)
        {
            ArrayList<string> days=new ArrayList<string>(5);

            todaystarttime = TimeKit.getTodayServerStart();
            if (isStart)
                days.add("请选择开始时间");
            else
            {
                days.add("请选择结束时间");
            }

            long time = 0;
            for (int i = 0; i < day; i++)
            {
                time = todaystarttime - i*TimeKit.DAY_MILLS;
                days.add(TimeKit.format(time, "yyyy-MM-dd"));
            }
            return days.toArray();
        }
        public void resetDropDownStart()
        {
            string[] showNames = getTime(true);
            starttime.options.Clear();
            Dropdown.OptionData temoData;
            for (int i = 0; i < showNames.Length; i++)
            {
                //给每一个option选项赋值
                temoData = new Dropdown.OptionData();
                temoData.text = showNames[i];

                starttime.options.Add(temoData);
            }
            starttime.captionText.text = showNames[day];//初始选项的显示
            starttime.value = day;
        }

        public void onStartSelect()
        {
            starttimeValue = starttime.value;
            if (starttimeValue == 0) _starttime = 0;
            _starttime = getTodayTime() - (starttimeValue-1) * TimeKit.DAY_MILLS;
         }

        public long getTodayTime()
        {
            return TimeKit.getTodayServerStart();
        }

        public void onEndSelect()
        {
            endtimeValue = endtime.value;
            if (endtimeValue == 0) _endtime = 0;
            _endtime= getTodayTime() - (endtimeValue-1) * TimeKit.DAY_MILLS;
        }

        /// <summary>
        /// 重设选择规则来显示
        /// </summary>
        public void resetDropDownRuleType()
        {
//            string[] showNames = getTime(true);
//            ruletype.options.Clear();
//            Dropdown.OptionData temoData;
//            for (int i = 0; i < showNames.Length; i++)
//            {
//                //给每一个option选项赋值
//                temoData = new Dropdown.OptionData();
//                temoData.text = showNames[i];
//
//                ruletype.options.Add(temoData);
//            }
//            ruletype.captionText.text = showNames[day];//初始选项的显示
        }

        public void onRuleTypeSelect()
        {
            ruletypeValue = ruletype.value;
        }

        public void resetDropDownEnd()
        {
            string[] showNames = getTime(false);
            endtime.options.Clear();
            Dropdown.OptionData temoData;
            for (int i = 0; i < showNames.Length; i++)
            {
                //给每一个option选项赋值
                temoData = new Dropdown.OptionData();
                temoData.text = showNames[i];

                endtime.options.Add(temoData);
            }
            endtime.captionText.text = showNames[1];//初始选项的显示
            endtime.value = 1;
        }

        protected override void xrefresh()
        {
            resetDropDownStart();
            resetDropDownEnd();
            resetDropDownRuleType();
        }

        public void setData(TicketBill[] bills,long starttime,long endtime, TicketTimeSoltTotal total)
        {
            list.clear();
            this._starttime = starttime;
            this._endtime = endtime;
            isMore = (bills.Length == 50);
            list.add(bills);
            setBottomInfo(total);//totals,0:达标房间数,1:参与人次，2：累收门票总数，3：实际返利总数，4.下级返利总数 ，
            refreshData();
        }

        public void setBottomInfo(TicketTimeSoltTotal totals)
        {
            nextTicketAward.setVisible(false);
            ticketAward.setVisible(false);
            reachRoom.textInfo = "达标房间数:";
            totalTicket.textInfo = "累收奖励:";
            ticketAward.textInfo = "奖励收益:";
            nextTicketAward.textInfo = "下级奖励:";

            if (Arena.arena.getMember().isMaster() && Arena.arena.isMultilevel())
            {
                nextTicketAward.setVisible(true);
                ticketAward.setVisible(true);
                reachRoom.text = totals.reachRoom.ToString();
                totalTicket.text = totals.getTotalTicket() +"";
                ticketAward.text = totals.getRealRebate() + "";
                nextTicketAward.text = totals.getNextRebate() + "";
            }
            else if (Arena.arena.isMultilevel()&&!Arena.arena.isLastAgent())
            {
                reachRoom.textInfo = "游戏人次:";
                totalTicket.textInfo = "我的奖励:";
                reachRoom.text = totals.matchCount.ToString();
                totalTicket.text = totals.getTotalTicket() + "";
                ticketAward.text = totals.getRealRebate()  + "";
                nextTicketAward.text = totals.getNextRebate() + "";
                nextTicketAward.setVisible(true);
                ticketAward.setVisible(true);
            }
            else
            {
                reachRoom.textInfo = "游戏人次:";
                totalTicket.textInfo = "我的奖励:";
                reachRoom.text = totals.matchCount.ToString();
                totalTicket.text = totals.getTotalTicket() + "";
            }
        }

        public void refreshData()
        {
            this.container.updateData(updateDatas);
            this.container.resize(list.count);
        }

         

        private bool isMore = false;

        public void updateDatas(ScrollBar bar, int index)
        {
            ArenaTicketBar ticketBar = (ArenaTicketBar)bar;
            ticketBar.setBill(list.get(index));
            ticketBar.refresh();

            if (isMore && (list.count - 1) == index)
            {
                CommandManager.addCommand(new GetArenaTicketBillListCommand(_starttime,_endtime,list.get(index).uuid), back);
            }
        }

        public void back(object obj)
        {
            if(obj==null) return;
            if (obj == null) return;
            object[] objs = (object[])obj;
            TicketBill[] bill = (TicketBill[])objs[0];
            isMore = bill.Length == 50;
            list.add(bill);
            container.incrResize(list.count);
        }
    }
}
