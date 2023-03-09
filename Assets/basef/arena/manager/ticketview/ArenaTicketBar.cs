using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaTicketBar:ScrollBar
    {
        /// <summary>
        /// 门票
        /// </summary>
        private Text ticket;
        /// <summary>
        /// 玩法名称
        /// </summary>
        private Text rulename;

        /// <summary>
        /// 游戏人数
        /// </summary>
        private Text playercount;
        /// <summary>
        /// 房间id
        /// </summary>
        private Text roomid;
        /// <summary>
        /// 时间
        /// </summary>
        private Text time;

        public TicketBill bill;

        protected override void xinit()
        {
            ticket = transform.Find("ticket").GetComponent<Text>();
            rulename = transform.Find("rulename").GetComponent<Text>();
            playercount = transform.Find("playercount").GetComponent<Text>();
            time = transform.Find("time").GetComponent<Text>();
            roomid = transform.Find("roomid").GetComponent<Text>();
        }

        public void setBill(TicketBill bill)
        {
            this.bill = bill;
        }

        protected override void xrefresh()
        {
            ticket.text = bill.getValueD().ToString();
            rulename.text = bill.rule;
            playercount.text = bill.count.ToString();
            time.text = TimeKit.format(bill.time, "yyyy/MM/dd HH:mm:ss");
            roomid.text = bill.roomid.ToString();
        }
    }
}
