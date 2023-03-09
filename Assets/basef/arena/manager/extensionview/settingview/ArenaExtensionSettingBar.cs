using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaExtensionSettingBar : ScrollBar
    {
        private Text wfname;

        private Text rulename;

        private UnrealInputTextField text;
        /// <summary>
        /// 可分配区间
        /// </summary>
        private Text qujian;

        Text qujian1;

        UnrealInputTextField text1;


        private RuleRebate rebate;

        protected override void xinit()
        {
            wfname = transform.Find("nickname").GetComponent<Text>();
            rulename = transform.Find("type").GetComponent<Text>();
            text = transform.Find("text").GetComponent<UnrealInputTextField>();
            qujian = transform.Find("qujian").GetComponent<Text>();
            text.init();
            text.endChange = back;
            qujian1 = transform.Find("qujian1").GetComponent<Text>();
            text1 = transform.Find("text1").GetComponent<UnrealInputTextField>();
            text1.init();
            text1.endChange = back1;
        }

        public void setData(RuleRebate rebate)
        {
            this.rebate = rebate;
        }

        public void back1(string c)
        {
            var rule = Arena.arena.fkcSettings.getRenaLockRule(rebate.rule);
            double r = 0;
            if (c == null || c.Length == 0)
                r = 0;
            else
                r = StringKit.parseDouble(c);


            if (rule.list.count == 2)//大赢家支付，且分数区间只有一个
            {
                TicketLevel level = null;
                if (rule.mpType == 0)
                    level = rule.list.get(1);
                else
                    level = rule.list.get(0);

                double ticket = MathKit.getRound2Long(level.value - level.goldvalue);
                if (r > ticket)
                {
                    r = ticket;
                    text1.text = ticket.ToString();
                }
                var value = 0;
                if (ticket != 0)
                    value = MathKit.floor((float)(r * 100 / ticket));
                rebate.subRate = value;
                text.text = value + "";
            }
        }

        public void back(string c)
        {
            int r = 0;
            if (c == null || c.Length == 0)
                r = 0;
            r = StringKit.parseInt(c);
            if (r > 100)
            {
                r = 100;
                text.text = r.ToString();
            }

            rebate.subRate = r;


            ArenaLockRule rule = Arena.arena.fkcSettings.getRenaLockRule(rebate.rule);

            if (rule.list.count == 2)// 大赢家支付，且分数区间只有一个
            {
                TicketLevel level = null;
                if (rule.mpType == 0)
                    level = rule.list.get(1);
                else
                    level = rule.list.get(0);
                var ticket = MathKit.getRound2Long(level.value - level.goldvalue);
                text1.text = rebate.subRate * ticket / 100 + "";
            }
        }

        protected override void xrefresh()
        {
            text.text = rebate.subRate.ToString();
            ArenaLockRule rule = Arena.arena.fkcSettings.getRenaLockRule(rebate.rule);
            if (rule == null)
            {
                Alert.show("该规则不存在，请重新点击！");
                return;
            }
            wfname.text = rule.name;
            rulename.text = rule.rule.name;
            qujian.text = "0%-" + rebate.rate + "%";

            if (rule.list.count == 2) /// 大赢家支付，且分数区间只有一个
            {
                TicketLevel level = null;
                if (rule.mpType == 0)
                    level = rule.list.get(1);
                else
                    level = rule.list.get(0);
                var ticket = MathKit.getRound2Long(level.value - level.goldvalue);
                double value = rebate.rate * ticket / 100d;
                qujian1.text = "0-" + value;
                text1.text = rebate.subRate * ticket / 100d + "";
                qujian1.gameObject.SetActive(true);
                text1.gameObject.SetActive(true);
            }
            else
            {
                qujian1.gameObject.SetActive(false);
                text1.gameObject.SetActive(false);
            }
        }
    }
}
