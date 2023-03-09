using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaAloneSetRuleRadioBar: ScorllTableBar
    {
        private Text wfname;

        private Text rulename;
        /// <summary>
        /// 可分配的奖励区间
        /// </summary>
        private Text qujian;


        private UnrealInputTextField text;

        private RuleRebate rebate;

        private RuleRebate selfRebate;

        protected override void xinit()
        {
            wfname = transform.Find("nickname").GetComponent<Text>();
            rulename = transform.Find("type").GetComponent<Text>();
            qujian = transform.Find("qujian").GetComponent<Text>();
            text = transform.Find("text").GetComponent<UnrealInputTextField>();
            text.init();
            text.endChange = back;
        }

        private bool isCanInput;

        public void setData(RuleRebate rebate,RuleRebate selfRebate,bool isCanInput)
        {
            this.rebate = rebate;
            this.selfRebate = selfRebate;
            this.isCanInput = isCanInput;
        }

        public void setCanInput(bool b)
        {
            this.isCanInput = b;
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

            rebate.rate = r;
        }

        protected override void xrefresh()
        {
            
            ArenaLockRule rule = Arena.arena.fkcSettings.getRenaLockRule(rebate.rule);
            if (rule == null)
            {
                Alert.show("该规则不存在，请重新点击！");
                return;
            }

            text.text = rebate.rate.ToString();
            text.interactive = isCanInput;
            wfname.text = rule.name;
            rulename.text = rule.rule.name;

            qujian.text = "0%-"+ selfRebate.rate + "%";
        }
    }
}
