using cambrian.unreal.scroll;
using UnityEngine.UI;
namespace basef.arena
{
    public class ArenaQuickGameRuleBar : ScorllTableBar
    {
        Text text;

        public ArenaLockRule rule;

        protected override void xinit()
        {
            text = transform.Find("rule").Find("text").GetComponent<Text>();
        }


        public void setData(ArenaLockRule rule)
        {
            this.rule = rule;
        }

        protected override void xrefresh()
        {
            this.text.text = rule.name;
        }
    }
}

