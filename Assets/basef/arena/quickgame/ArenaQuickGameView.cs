using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine.UI;

namespace basef.arena
{

    public class ArenaQuickGameView : UnrealLuaSpaceObject
    {

        Text text;

        /// <summary>
        /// 当前选中的规则
        /// </summary>
         ArenaLockRule rule;

        protected override void xinit()
        {
            text = transform.Find("selectrule").Find("ruletext").GetComponent<Text>();
        }

        public void setData(ArenaLockRule rule)
        {
            this.rule = rule;
        }

        public ArenaLockRule getLockRule()
        {
            return rule;
        }

        protected override void xrefresh()
        {
            if (Arena.arena.fkcSettings.getRuleCount() == 0)
            {
                this.transform.gameObject.SetActive(false);
            }
            else
            {
                this.transform.gameObject.SetActive(true);
            }

            if (rule != null)
            {
                text.text = rule.name;
            }
            else
            {
                text.text = "玩法选择";
            }
        }
    }
}
