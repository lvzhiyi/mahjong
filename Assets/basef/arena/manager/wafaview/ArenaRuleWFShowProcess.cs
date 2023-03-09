using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场玩法显示点击事件
    /// </summary>
    public class ArenaRuleWFShowProcess : MouseEventClickProcess
    {
        public override void execute()
        {
            ArenaRuleWFShowBar bar = this.transform.GetComponent<ArenaRuleWFShowBar>();
            bar.isSelected(!bar.isSelect);
            ArenaRuleWFShowPanel panel = this.root.GetComponent<ArenaRuleWFShowPanel>();
            panel.updateShow(bar.lockRule.uuid, bar.isSelect);
        }
    }
}