using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{

    public class ChangUIRuleView : UnrealLuaSpaceObject
    {

        string[] deskbgnames = new string[]
       {
            "绛紫色","橙红色","橙黄色","经典灰","淡蓝色","草绿色","蓝褐色","经典紫"
       };

        /// <summary>
        /// 规则容器
        /// </summary>
        ScrollContainer container1;
        /// <summary>
        /// 桌布容器
        /// </summary>
        ScrollTableContainer container;


        /// <summary>
        /// 选中规则
        /// </summary>
        int selectedRule;
        /// <summary>
        /// 选中桌布
        /// </summary>
        int selectedDeskBg;

        RuleSkin ruleSkin;

        ArrayList<ArenaLockRule> rules;

        protected override void xinit()
        {
            container = this.transform.Find("container").GetComponent<ScrollTableContainer>();
            container.init();

            container1 = this.transform.Find("container_1").GetComponent<ScrollContainer>();
            container1.init();
        }

        public void setData(RuleSkin ruleSkin)
        {
            rules = Arena.arena.fkcSettings.getRules();
            this.ruleSkin = ruleSkin;
            this.ruleSkin.update(rules);
            if (rules.count != 0)
            {
                ArenaLockRule rule = rules.get(0);
                selectedRule = rule.uuid;
                selectedDeskBg = ruleSkin.getDeskBg(selectedRule,rule.rule.getPlatFormValue());
            }
        }

        protected override void xrefresh()
        {
            refreshRule();
            refreshDeskBg();
        }

        // ==========================选择=============================
        public void selectRule(int ruleid,int platform)
        {
            selectedRule = ruleid;
            selectedDeskBg = ruleSkin.getDeskBg(ruleid, platform);
            container1.refreshShow();
            container.refreshShow();
        }

        public void selectDeskBg(int index)
        {
            selectedDeskBg = index;
            ruleSkin.setDeskBg(selectedRule, index);
            container1.refreshShow();
            container.refreshShow();
        }

        // ==========================保存=============================
        public void saveSetting(ByteBuffer data)
        {
            ruleSkin.bytesWrite(data);
        }

        // ==========================规则=============================
        public void refreshRule()
        {
            container1.updateData(ruleCallback);
            container1.resize(rules.count);
            container1.resizeDelta();
        }

        private void ruleCallback(ScrollBar bar, int index)
        {
            ChangUIRuleBar temp = (ChangUIRuleBar)bar;
            temp.index_space = index;
            ArenaLockRule rule = rules.get(index);
            temp.setData(selectedRule, ruleSkin.getDeskBg(rule.uuid, rule.rule.getPlatFormValue()), rule);
            temp.refresh();
        }

        // ==========================桌布=============================
        public void refreshDeskBg()
        {
            int size = rules.count != 0 ? deskbgnames.Length : 0;
            container.updateData(deskbgCallback);
            container.resize(size);
            container.resizeDelta();
        }

        private void deskbgCallback(ScorllTableBar bar, int index)
        {
            ChangUIDeskBgBar temp = (ChangUIDeskBgBar)bar;
            temp.index_space = index;
            temp.setData(selectedDeskBg, deskbgnames[index]);
            temp.refresh();
        }
    }
}