using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// AAA支付视图
    /// </summary>
    public class ArenaRuleAAAView:UnrealLuaSpaceObject
    {
        UnrealInputTextField input;

        public TicketLevel ticket;

        protected override void xinit()
        {
            input = transform.Find("ticket").GetComponent<UnrealInputTextField>();
            input.init();
            input.endChange = ticket_back;
        }


        public void setData(TicketLevel ticket)
        {
            this.ticket = ticket;
        }

        public void ticket_back(string value)
        {
            if (value == null || "".Equals(value) || "0".Equals(value))
                input.text = MathKit.getRound2Long(ticket.value).ToString();
            else
            {
                double v = StringKit.parseDouble(value);
                if (v < 0)
                {
                    Alert.autoShow("只能输入>0的值");
                    v = MathKit.getRound2Long(ticket.value);
                    if (v == 0)
                        v = 1;
                }

                ticket.value = MathKit.getLongEnlarge100(v);
                input.text = v.ToString();
            }
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            if (ticket == null)
                input.text = "0";
            else
                input.text = MathKit.getRound2Long(ticket.value).ToString();
        }
    }
}
