using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 显示防成谜视图
    /// </summary>
    public class ShowRuleIndulgeViewProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRulePanel panel = getRoot<ArenaRulePanel>();
            string nickname = panel.rulesView.nickname.text;
            panel.rulesView.arenaLockRule.name = nickname;
            double difen = StringKit.parseDouble(panel.rulesView.difenscore.text);
            if (difen < 0) difen = 1;
            panel.rulesView.arenaLockRule.rule.setBet(MathKit.getIntEnlarge100(difen));
            //panel.showIndulgeView();

            ArenaWfNextPanel nextpanel = UnrealRoot.root.getDisplayObject<ArenaWfNextPanel>();
            nextpanel.setData(panel.rulesView.arenaLockRule);
            nextpanel.refresh();
            nextpanel.setCVisible(true);
            nextpanel.setLastPanel(this.root);
            this.root.setCVisible(false);
        }
    }
}
