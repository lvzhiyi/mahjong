using System;
using scene.game;
using UnityEngine;

namespace basef.regions
{
    /// <summary>
    /// 地区面板
    /// </summary>
    public class RegionsPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 规则名视图
        /// </summary>
        private RegionsRuleNameView ruleNameView;

        [HideInInspector] public RegionsView regionsView;

        private Action action;

        UnrealScaleButton exit;

        protected override void xinit()
        {
            base.xinit();
            this.regionsView = this.content.Find("content").Find("center").Find("regions").GetComponent<RegionsView>();
            this.regionsView.init();

            this.ruleNameView = this.content.Find("content").Find("center").Find("right").GetComponent<RegionsRuleNameView>();
            this.ruleNameView.init();

            this.exit = this.content.Find("content").Find("top").Find("exit").GetComponent<UnrealScaleButton>();
            this.exit.init();
        }

        public void setAction(Action action)
        {
            this.action = action;
        }


        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (!b && action != null)
            {
                action();
                action = null;
            }
            else if (action == null)
            {
                exit.setVisible(false);
            }
        }


        protected override void xrefresh()
        {
            Regions regions = SpotRedRoot.roots.regionModule.getRegions();
            regionsView.setRegions(regions.region);
            regionsView.refresh();

            //string[] rulename= SpotRedRoot.roots.regionModule.region.rulename;
            //ruleNameView.setRuleName(rulename);
            //ruleNameView.refresh();
        }

        public void setRegionRuleName(int id)
        {
            Regions regions = SpotRedRoot.roots.regionModule.getRegions();
            //string[] rulename = regions.getRegion(id).rulename;
            //ruleNameView.setRuleName(rulename);
            //ruleNameView.refresh();
        }
    }
}
