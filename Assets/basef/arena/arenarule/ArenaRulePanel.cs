using platform.spotred;
using basef.rule;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 规则面板
    /// </summary>
    public class ArenaRulePanel:UnrealLuaPanel
    {
        private ArenaLockRule[] rule;
        //UI
        [HideInInspector] public ArenaRuleDetailView rulesView;
        /// <summary>
        /// 防沉迷视图
        /// </summary>
        //[HideInInspector] public ArenaIndulgeView indulgeView;
        /// <summary>
        /// 具体的规则容器
        /// </summary>
        private UnrealTableContainer ruleContainer;

        /// <summary>
        /// 游戏类型
        /// </summary>
        private UnrealTableContainer gameContainer;

        /// <summary>
        /// 功能正在开发中
        /// </summary>
        private Text info;

        /// <summary>
        /// 显示规则的描述视图
        /// </summary>
        //private ArenaRuleInfoView infview;

        /// <summary>
        /// 是否是显示规则的描述视图
        /// </summary>
        private bool isLocked;

        //private UnrealLuaSpaceObject titles;

        protected override void xinit()
        {
            base.xinit();
            ruleContainer = content.Find("rule").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            ruleContainer.init();
            gameContainer = content.Find("gametype").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            gameContainer.init();

            rulesView = content.Find("wf").GetComponent<ArenaRuleDetailView>();
            rulesView.init();

            //indulgeView = content.Find("othersetting").GetComponent<ArenaIndulgeView>();
            //indulgeView.init();

            //infview = content.Find("wfinfo").GetComponent<ArenaRuleInfoView>();
            //infview.init();
            //titles = content.Find("titles").GetComponent<UnrealLuaSpaceObject>();
            info = content.Find("info").GetComponent<Text>();
        }

        public void setRule(ArenaLockRule[] rule,bool isLocked)
        {
            this.rule = rule;
            this.isLocked = isLocked;
        }


        protected override void xrefresh()
        {
            info.gameObject.SetActive(false);
            //indulgeView.setVisible(false);
            //infview.setVisible(false);
            ArenaLockRule[] rules = showGameType(rule);
            refreshRule(rules);

            //titles.setVisible(true);

            //if (isLocked)
            //{
            //    rulesView.setVisible(false);
            //    titles.setVisible(false);
            //    return;
            //}
        }


        private ArenaLockRule[] getArenaLockRules(int[] r)
        {
            ArenaLockRule[] rules = new ArenaLockRule[r.Length];
            for (int i = 0; i < r.Length; i++)
            {
                rules[i] = new ArenaLockRule();
                rules[i].rule = RuleManager.manager.newSample(r[i]);
            }
            return rules;
        }

        /// <summary>
        /// 设置竞赛场规则
        /// </summary>
        /// <param name="gameTypes"></param>
        private void setArenaLockRules(GameType[] gameTypes)
        {
            ArenaLockRule[] lockRules;
            for (int i = 0; i < gameTypes.Length; i++)
            {
                lockRules = getArenaLockRules(gameTypes[i].ruleid);
                gameTypes[i].addArenaLockRule(lockRules);
            }
        }

        /// <summary>
        /// 显示游戏类型
        /// </summary>
        private ArenaLockRule[] showGameType(ArenaLockRule[] rule)
        {
            GameType[] gameTypes = GameConfig.getInstance().getCloneTypes();
            if (rule == null)
            {
                setArenaLockRules(gameTypes);
            }
            else
            {
                ArrayList<GameType> types = new ArrayList<GameType>();
                gameTypes = GameConfig.getInstance().getCloneTypes();
                for (int i = 0; i < rule.Length; i++)
                {
                    for (int j = 0; j < gameTypes.Length; j++)
                    {
                        if (gameTypes[j].isExistRule(rule[i].rule.sid))
                        {
                            gameTypes[j].addArenaLockRule(rule[i]);
                        }
                    }
                }

                for (int i = 0; i < gameTypes.Length; i++)
                {
                    if (gameTypes[i].getArenaLockRule() != null)
                    {
                        types.add(gameTypes[i]);
                    }
                }

                gameTypes = types.toArray();

            }
            gameContainer.resize(gameTypes.Length);
            for (int i = 0; i < gameTypes.Length; i++)
            {
                RuleGameTypeBar bar = (RuleGameTypeBar) gameContainer.bars[i];
                bar.setGameType(gameTypes[i]);
                bar.index_space = i;
                bar.refresh();
            }

            ((RuleGameTypeBar) gameContainer.bars[0]).selected();

            return gameTypes[0].getArenaLockRule().toArray();
        }



        public void showRule(GameType gameType, int index)
        {
            for (int i = 0; i < gameContainer.size; i++)
            {
                RuleGameTypeBar bar = (RuleGameTypeBar)this.gameContainer.bars[i];
                if (i == index)
                    bar.selected();
                else
                    bar.selectNormal();
            }

            ArenaLockRule selectRule = refreshRule(gameType.getArenaLockRule().toArray());

            if (!selectRule.rule.iscomplete)
            {
                rulesView.setVisible(false);
                info.gameObject.SetActive(true);
                return;
            }

            info.gameObject.SetActive(false);

            if (!isLocked)
            {
                rulesView.setVisible(true);
                rulesView.setArenRule(selectRule);
                rulesView.refresh();
            }
        }


        private ArenaLockRule refreshRule(ArenaLockRule[] rules)
        {
            ruleContainer.resize(rules.Length);

            for (int i = 0; i < rules.Length; i++)
            {
                ArenaRuleBar bar = (ArenaRuleBar)this.ruleContainer.bars[i];
                bar.index_space = i;
                bar.setArenaLockRule(rules[i], isLocked);
                bar.refresh();
            }

            ruleContainer.resizeDelta();

            ((ArenaRuleBar)ruleContainer.bars[0]).selected();
            showView(rules[0],0);
            return rules[0];
        }

        public void showView(ArenaLockRule rule, int index)
        {
            for (int i = 0; i < ruleContainer.size; i++)
            {
                ArenaRuleBar bar = (ArenaRuleBar)this.ruleContainer.bars[i];
                if (i == index)
                    bar.selected();
                else
                    bar.selectNormal();
            }

            if (isLocked)
            {
                //infview.setArenaLockRule(rule);
                //infview.refresh();
                //infview.setVisible(true);
                rulesView.setVisible(false);
                return;
            }

            if (!rule.rule.iscomplete)
            {
                rulesView.setVisible(false);
                info.gameObject.SetActive(true);
                return;
            }

            info.gameObject.SetActive(false);

            rulesView.setVisible(true);
            rulesView.setArenRule(rule.clone());
            rulesView.refresh();

            //indulgeView.setVisible(false);
        }

        public void refreshWanFa(bool b, int index)
        {
            rulesView.getRule().setWanFan(b, index);
            rulesView.refreshWaFa();
        }

        //public void showIndulgeView()
        //{
        //    indulgeView.setVisible(true);
        //    indulgeView.setData(rulesView.arenaLockRule);
        //    indulgeView.refresh();
        //    rulesView.setVisible(false);
        //}

        public void showRulesView()
        {
            rulesView.setVisible(true);
            rulesView.refresh();
            //indulgeView.setVisible(false);
        }
    }
}
