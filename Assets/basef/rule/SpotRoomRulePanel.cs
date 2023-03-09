using platform.spotred;
using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    /// <summary>
    /// 规则界面
    /// </summary>
    public class SpotRoomRulePanel : UnrealLuaPanel
    {
        /// <summary>
        /// 普通创建房间,代理开放
        /// </summary>
        public const int COMMON_CREATE_ROOM = 1, PROMTER_CREATE_ROOM = 2;

        protected Rule save_rule;

        bool isPromoter;
        /// <summary>
        /// 创建房间状态,普通创建房间,代开房
        /// </summary>
        int create_room_stauts;
        //UI
        [HideInInspector] public RulesView rulesView;
        /// <summary>
        /// 规则
        /// </summary>
        protected UnrealTableContainer ruleContainer;
        /// <summary>
        /// 游戏类型
        /// </summary>
        protected UnrealTableContainer gameContainer;

        /// <summary>
        /// 功能正在开发中
        /// </summary>
        protected Text info;
        /// <summary>
        /// 代开房选项
        /// </summary>
        //private Toggle toggle;

        protected override void xinit()
        {
            base.xinit();
            ruleContainer =
                content.Find("rule").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            ruleContainer.init();

            gameContainer = content.Find("gametype").Find("mask").Find("container")
                .GetComponent<UnrealTableContainer>();
            gameContainer.init();

            rulesView = content.Find("rulesinfo").GetComponent<RulesView>();
            rulesView.init();
            //if (rulesView.transform.Find("toggle") != null)
            //    toggle = rulesView.transform.Find("toggle").GetComponent<Toggle>();

            info = content.Find("info").GetComponent<Text>();
        }

        public void setData(Rule rule,bool isPromoter)
        {
            this.save_rule = rule;
            this.isPromoter = isPromoter;
        }

        /// <summary>
        /// 是否创建代开房(UI上绑定的方法)
        /// </summary>
        /// <param name="b"></param>
        public void isCreatePromoterRoom(bool b)
        {
            if (b)
                setCreateRoomStauts(PROMTER_CREATE_ROOM);
            else
                setCreateRoomStauts(COMMON_CREATE_ROOM);
        }

        /// <summary>
        /// 设置创建房间状态(普通创建,还是代理开放)
        /// </summary>
        public void setCreateRoomStauts(int status)
        {
            this.create_room_stauts = status;
        }

        /// <summary>
        /// 获取创建房间状态
        /// </summary>
        /// <returns></returns>
        public int getCreatRoomStauts()
        {
            return create_room_stauts;
        }

        protected override void xrefresh()
        {
            info.gameObject.SetActive(false);
            //if (toggle != null)
            //{
            //    toggle.gameObject.SetActive(false);
            //    if (isPromoter)
            //    {
            //        toggle.gameObject.SetActive(true);
            //        toggle.isOn = false;
            //    }
            //}
            

            GameType select = null;

            GameType[] gameTypes= GameConfig.getInstance().getGameType();
            int len = gameTypes.Length;
            gameContainer.resize(len);
            for (int i = 0; i < len; i++)
            {
                RuleGameTypeBar bar=(RuleGameTypeBar)gameContainer.bars[i];
                bar.setGameType(gameTypes[i]);
                bar.index_space = i;
                bar.refresh();
                if (save_rule != null&&gameTypes[i].isExistRule(save_rule.sid))
                {
                    bar.selected();
                    select = gameTypes[i];
                }
            }

            if (select == null)
            {
                select = gameTypes[0];
                ((RuleGameTypeBar)gameContainer.bars[0]).selected();
            }

            int[] rules = GameConfig.getInstance().getRulesSort1(save_rule,select.ruleid);

            Rule selectRule=refreshRule(rules);
            rulesView.setRule(selectRule);
            rulesView.refresh();

        }

        private Rule refreshRule(int[] rules)
        {
            Rule selectRule = null;

            this.ruleContainer.resize(rules.Length);

            bool b = false;
            for (int i = 0; i < rules.Length; i++)
            {
                Rule rule = RuleManager.manager.newSample(rules[i]);
                RuleBar bar = (RuleBar)ruleContainer.bars[i];
                bar.index_space = i;
              
                if (save_rule != null && save_rule.sid == rule.sid)
                {
                    bar.setRule(save_rule);
                    selectRule = save_rule;
                    b = true;
                }
                else
                {
                    bar.setRule(rule);
                }

                bar.refresh();

                if (selectRule != null && b)
                {
                    bar.selected();
                    b = false;
                }
                else
                    bar.selectNormal();
            }

            if (selectRule == null)
            {
                selectRule= ((RuleBar)ruleContainer.bars[0]).getRule();
                ((RuleBar)ruleContainer.bars[0]).selected();
            }

            this.ruleContainer.resizeDelta();

            return selectRule;
        }

        public RulesView getRulesView()
        {
            return rulesView;
        }

        public virtual void showRule(GameType gameType,int index)
        {
            for (int i = 0; i < gameContainer.size; i++)
            {
                RuleGameTypeBar bar = (RuleGameTypeBar)this.gameContainer.bars[i];
                if (i == index)
                    bar.selected();
                else
                    bar.selectNormal();
            }

            Rule selectRule = refreshRule(gameType.ruleid);

            if (!selectRule.iscomplete)
            {
                this.rulesView.setVisible(false);
                this.info.gameObject.SetActive(true);
                return;
            }

            this.info.gameObject.SetActive(false);

            this.rulesView.setVisible(true);
            this.rulesView.setRule(selectRule);
            this.rulesView.refresh();
        }

        public virtual void showView(Rule rule, int index)
        {
            for (int i = 0; i < ruleContainer.size; i++)
            {
                RuleBar bar = (RuleBar) this.ruleContainer.bars[i];
                if (i == index)
                    bar.selected();
                else
                    bar.selectNormal();
            }

            if (!rule.iscomplete)
            {
                this.rulesView.setVisible(false);
                this.info.gameObject.SetActive(true);
                return;
            }

            this.info.gameObject.SetActive(false);

            this.rulesView.setVisible(true);
            this.rulesView.setRule(rule);
            this.rulesView.refresh();
        }

        public void refreshWanFa(bool b,int index)
        {
            this.rulesView.getRule().setWanFan(b,index);
            rulesView.refreshWaFa();
        }
    }
}
