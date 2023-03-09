using cambrian.common;

namespace basef.arena
{
    public class ArenCreateRuleProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaWfNextPanel panel = this.getRoot<ArenaWfNextPanel>();
       
            long mingoldnum = StringKit.parseLong(panel.indulgeView.mingoldnum.text);
            if (mingoldnum < 0) mingoldnum = 0;
            int diyu_gold_jiesan = StringKit.parseInt(panel.indulgeView.diyu_gold_jiesan.text);
            if (diyu_gold_jiesan < 0) diyu_gold_jiesan = 0;
            int men_di_yu_bu_fa = StringKit.parseInt(panel.indulgeView.men_di_yu_bu_fa.text);
            if (men_di_yu_bu_fa < 0) men_di_yu_bu_fa = 0;
            int fu_gold = StringKit.parseInt(panel.indulgeView.fu_gold.text);
            if (fu_gold < -1) fu_gold = -1;
            ArenaLockRule rule= panel.indulgeView.rule;

           

            ArenaIndulgeView view= transform.parent.GetComponent<ArenaIndulgeView>();
            rule.mpType = view.ticketsType;
            if (view.ticketsType == ArenaLockRule.TYPE_AA)
            {
                rule.list.clear();
                rule.list.add(view.aaLevel);
            }

            rule.setLimitGold(mingoldnum);
            rule.setDisbandThreshod(diyu_gold_jiesan);
            rule.setThreshold(men_di_yu_bu_fa);
            rule.setOverDraft(fu_gold);

            CommandManager.addCommand(new ArenaCreateAndDeleteCommand(Arena.arena.getId(),true,rule),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            Alert.show("增加成功");
            this.root.setVisible(false);
            ArenaManagerPanel panel= UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            panel.managerView.waFaView.refresh();
        }
    }
}
