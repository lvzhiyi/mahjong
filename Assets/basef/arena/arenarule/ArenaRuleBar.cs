using basef.rule;
using UnityEngine;

namespace basef.arena
{
    public class ArenaRuleBar:RuleBar
    {
        [HideInInspector] public ArenaLockRule lockRule;

        private bool isLock;

        protected override void xinit()
        {
            base.xinit();
        }

        public void setArenaLockRule(ArenaLockRule lockRule,bool isLock)
        {
            this.lockRule = lockRule;
            this.isLock = isLock;
            setRule(lockRule.rule);
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (isLock)
            {
                txtchecked.text = lockRule.name;
                txtnormal.text = lockRule.name;
            }
        }
    }
}
