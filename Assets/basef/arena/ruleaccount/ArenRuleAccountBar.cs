using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenRuleAccountBar : ScorllTableBar
    {
        private Text ruleName;

        private Text match;

        private Text gold;

        private ArenaRuleAccountcs accounts;

        protected override void xinit()
        {
            ruleName = transform.Find("nickname").GetComponent<Text>();
            match = transform.Find("count").GetComponent<Text>();
            gold = transform.Find("gold").GetComponent<Text>();
        }

        public void setData(ArenaRuleAccountcs accounts)
        {
            this.accounts = accounts;
        }

        protected override void xrefresh()
        {
            ruleName.text = accounts.rulename;
            match.text = accounts.getMatchData();
            gold.text = accounts.getTicketData();
        }
    }
}
