using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    public class RulesView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 滚动组件
        /// </summary>
        protected ScrollRect scrollRect;

        protected Transform container;
        /// <summary>
        /// 局数
        /// </summary>
        protected UnrealRadioList jushu;

        /// <summary>
        /// 封顶
        /// </summary>
        protected UnrealRadioList fengding;

        /// <summary>
        /// 选番
        /// </summary>
        protected UnrealRadioList xuanfan;

        /// <summary>
        /// 点炮
        /// </summary>
        protected UnrealRadioList dianpao;

        /// <summary>
        /// 玩家人数
        /// </summary>
        protected UnrealRadioList playersnum;

        /// <summary>
        /// 查叫
        /// </summary>
        protected UnrealRadioList chajiao;
        /// <summary>
        /// 设置底分
        /// </summary>
        private UnrealRadioList difen;
        /// <summary>
        /// 设置倍分
        /// </summary>
        private UnrealRadioList beifen;
        /// <summary>
        /// 自摸
        /// </summary>
        protected UnrealRadioList zimocontainer;

        /// <summary>
        /// 点杠花
        /// </summary>
        protected UnrealRadioList dghuacontainer;

        /// <summary>
        /// 换三张
        /// </summary>
        protected UnrealRadioList huanszcontainer;

        /// <summary>
        /// 托管
        /// </summary>
        protected UnrealRadioList trusteeship;

        /// <summary>
        /// 托管时间
        /// </summary>
        protected UnrealRadioList trusteeshiptime;

        /// <summary>
        /// 翻型
        /// </summary>
        protected UnrealRadioList inversion;

        /// <summary>
        /// 几番起胡
        /// </summary>
        protected UnrealRadioList canHuPointContainer;

        /// <summary>
        /// 选择飘
        /// </summary>
        protected UnrealRadioList piaoContainer;

        /// <summary>
        /// 游戏模式
        /// </summary>
        protected UnrealRadioList gamemodel;

        /// <summary>
        /// 游戏倍数
        /// </summary>
        protected UnrealRadioList maxmultiple;

        /// <summary>
        /// 顺序
        /// </summary>
        protected UnrealRadioList order;

        /// <summary>
        /// 手牌数
        /// </summary>
        protected UnrealRadioList handsnum;

        /// <summary>
        /// 限制视图
        /// </summary>
        protected LimitView limitView;
        /// <summary>
        /// 申请解散房间时间
        /// </summary>
        protected UnrealRadioList applyexittime;

        /// <summary>
        /// 提前解散算不算本局算不算分
        /// </summary>
        protected UnrealRadioList tiQianJieSan;



        /// <summary>
        /// 托管后，解散
        /// </summary>
        protected UnrealRadioList trusteeshipjiesan;

        /// <summary>
        /// 规则玩法，比如贵阳做鸡
        /// </summary>
        protected UnrealRadioList ruleWf;

        /// <summary>
        /// 连庄
        /// </summary>
        protected UnrealRadioList remainBanker;

        /// <summary>
        /// 首局做庄
        /// </summary>
        protected UnrealRadioList makeBanker;
        /// <summary>
        /// 卖分
        /// </summary>
        protected UnrealRadioList salescore;
        /// <summary>
        /// 必胡
        /// </summary>
        protected UnrealRadioList musthu; 
        /// <summary>
        /// 必胡
        /// </summary>
        protected UnrealRadioList lunZhuang;
        /// <summary>
        /// 炸弹分数
        /// </summary>
        protected UnrealRadioList multipScore;

        /// <summary>
        /// 打法
        /// </summary>
        protected UnrealRadioList dafa;

        [HideInInspector] public UnrealTableContainer wanfa;

        protected Rule rule;

        protected Text moren_ytfh_rule;

        /// <summary>
        /// 可显示的单选项列表
        /// </summary>
        protected ArrayList<UnrealSpaceObject> radioList;

        // private Text tips;

        [HideInInspector] public bool isLocked;

        protected override void xinit()
        {
            scrollRect = transform.Find("content").GetComponent<ScrollRect>();
            container = scrollRect.transform.Find("mask").Find("container");
            fengding = container.Find("fengding").GetComponent<UnrealRadioList>();
            fengding.init();

            xuanfan = container.Find("xuanfan").GetComponent<UnrealRadioList>();
            xuanfan.init();

            ruleWf = container.Find("rulewanfa").GetComponent<UnrealRadioList>();
            ruleWf.init();

            if(container.Find("jiesanTime")!=null)
            {
                applyexittime = container.Find("jiesanTime").GetComponent<UnrealRadioList>();
                applyexittime.init();
            }

            if (container.Find("tiqianjiesan") != null)
            {
                tiQianJieSan = container.Find("tiqianjiesan").GetComponent<UnrealRadioList>();
                tiQianJieSan.init();
            }

            remainBanker = container.Find("remainbanker").GetComponent<UnrealRadioList>();
            remainBanker.init();

            makeBanker = container.Find("makebanker").GetComponent<UnrealRadioList>();
            makeBanker.init();

            salescore = container.Find("salescore").GetComponent<UnrealRadioList>();
            salescore.init();

            musthu = container.Find("musthu").GetComponent<UnrealRadioList>();
            musthu.init();

            lunZhuang = container.Find("lunzhuang").GetComponent<UnrealRadioList>();
            lunZhuang.init();

            multipScore = container.Find("multipScore").GetComponent<UnrealRadioList>();
            multipScore.init();

            wanfa = container.Find("wanfa").GetComponent<UnrealTableContainer>();
            wanfa.init();

            jushu = container.Find("gamesnum").GetComponent<UnrealRadioList>();
            jushu.init();

            chajiao = container.Find("chajiao").GetComponent<UnrealRadioList>();
            chajiao.init();

            if (container.Find("difenmj") != null)
            {
                difen = container.Find("difenmj").GetComponent<UnrealRadioList>();
                difen.init();
            }

            if (container.Find("beifen") != null)
            {
                beifen = container.Find("beifen").GetComponent<UnrealRadioList>();
                beifen.init();
            }

            dianpao = container.Find("dianpao").GetComponent<UnrealRadioList>();
            dianpao.init();

            zimocontainer = container.Find("zimo").GetComponent<UnrealRadioList>();
            zimocontainer.init();
            dghuacontainer = container.Find("dgh").GetComponent<UnrealRadioList>();
            dghuacontainer.init();

            huanszcontainer = container.Find("huansz").GetComponent<UnrealRadioList>();
            huanszcontainer.init();

            playersnum = container.Find("playersnum").GetComponent<UnrealRadioList>();
            playersnum.init();

            if (container.Find("piao") != null)
            {
                piaoContainer = container.Find("piao").GetComponent<UnrealRadioList>();
                piaoContainer.init();
            }

            if (container.Find("inversion") != null)
            {
                inversion = container.Find("inversion").GetComponent<UnrealRadioList>();
                inversion.init();
            }

            if (container.Find("pointhustart") != null)
            {
                canHuPointContainer = container.Find("pointhustart").GetComponent<UnrealRadioList>();
                canHuPointContainer.init();
            }

            if (container.Find("tuoguan") != null)
            {
                trusteeship = container.Find("tuoguan").GetComponent<UnrealRadioList>();
                trusteeship.init();
            }

            if (container.Find("tuoguantime") != null)
            {
                trusteeshiptime = container.Find("tuoguantime").GetComponent<UnrealRadioList>();
                trusteeshiptime.init();
            }

            if (container.Find("tuoguanjiesan") != null)
            {
                trusteeshipjiesan = container.Find("tuoguanjiesan").GetComponent<UnrealRadioList>();
                trusteeshipjiesan.init();
            }

            if (container.Find("limit") != null)
            {
                limitView = container.Find("limit").GetComponent<LimitView>();
                limitView.init();
            }

            if (container.Find("gamemodel") != null)
            {
                gamemodel = container.Find("gamemodel").GetComponent<UnrealRadioList>();
                gamemodel.init();
            }

            if (container.Find("maxmultiple") != null)
            {
                maxmultiple = container.Find("maxmultiple").GetComponent<UnrealRadioList>();
                maxmultiple.init();
            }

            if (container.Find("order") != null)
            {
                order = container.Find("order").GetComponent<UnrealRadioList>();
                order.init();
            }

            if (container.Find("handsnum") != null)
            {
                handsnum = container.Find("handsnum").GetComponent<UnrealRadioList>();
                handsnum.init();
            }

            if (container.Find("dafa") != null)
            {
                dafa = container.Find("dafa").GetComponent<UnrealRadioList>();
                dafa.init();
            }

            moren_ytfh_rule = container.Find("ytfhtips").GetComponent<Text>();

            radioList = new ArrayList<UnrealSpaceObject>();
        }

        public virtual void setRule(Rule rule)
        {
            this.rule = rule;
        }

        public Rule getRule()
        {
            return rule;
        }

        protected override void xrefresh()
        {
            radioList.clear();
            refreshNormalSingle();
            moren_ytfh_rule.gameObject.SetActive(false);
            if (rule.getTips().Length > 0)
            {
                moren_ytfh_rule.text = "(" + rule.getTips() + ")";
                moren_ytfh_rule.gameObject.SetActive(true);
            }

            relayout();
        }

        public virtual void refreshNormalSingle()
        {
            refreshJuShu();
            refreshRuleWf();//规则玩法，这个是一个规则中，还要分小规则
            refreshModel();//游戏模式
            refreshHandsNum();//手牌数量
            refreshorder();//出牌顺序
            refreshMaxMultiple();//游戏最大倍数
            refreshDianPao();
            refreshFaShu();
            refreshXuanFan();//选番
            refreshPlayNum();
            refreshBeifen();
            refreshRemainBanker();
            refreshDafa();
            refreshDifen();
            refreshInversion();
            refreshChaJiao();
            refreshZiMo();
            refreshDGHua();
            refreshcanHuPoint();
            refreshPiao();
            refreshHuansz();
            refreshSaleScore();
            refreshMultipScore();
            refreshWaFa();
            refreshLunZhuang();
            refreshMustHu();
            refreshMakerBanker();
            refreshLimitView();//限制视图
        }

        /// <summary>
        /// 刷新游戏模式
        /// </summary>
        public void refreshHandsNum()
        {
            if (handsnum == null) return;
            string[] phandsnum = rule.getLuaRuleSingleNames(Rule.HANDSNUM);
            if (phandsnum != null)
            {
                this.handsnum.resize(phandsnum.Length);

                int index = 0;

                for (int i = 0; i < phandsnum.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.handsnum.bars[i];
                    bar.index_space = i;

                    bar.setTitle(phandsnum[i], isLocked);
                    int b = rule.getIntAtribute("handsnum");
                    if (b == int.Parse(phandsnum[i].Substring(0, phandsnum[i].Length - 1)))
                        index = i;
                    bar.refresh();
                }

                handsnum.selectedIndex(index);
                handsnum.resizeDelta();
                handsnum.setVisible(true);
                if (!isExistUnrealSpace(this.handsnum))
                    radioList.add(this.handsnum);
            }
            else
            {
                this.handsnum.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新叫地主顺序
        /// </summary>
        public void refreshorder()
        {
            if (order == null) return;
            string[] info = rule.getLuaRuleSingleNames(Rule.ORDER);
            if (info != null)
            {
                this.order.resize(info.Length);

                int index = 0;

                for (int i = 0; i < info.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.order.bars[i];
                    bar.index_space = i;

                    bar.setTitle(info[i], isLocked);
                    int b = rule.getIntAtribute("order");
                    if (b == i)
                        index = i;
                    bar.refresh();
                }

                order.selectedIndex(index);
                order.resizeDelta();
                order.setVisible(true);
                if (!isExistUnrealSpace(this.order))
                    radioList.add(this.order);
            }
            else
            {
                this.order.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新游戏倍数
        /// </summary>
        public void refreshMaxMultiple()
        {
            if (maxmultiple == null) return;
            string[] info = rule.getLuaRuleSingleNames(Rule.MAX_MULTIPLE);
            if (info != null)
            {
                this.maxmultiple.resize(info.Length);

                int index = 0;

                for (int i = 0; i < info.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.maxmultiple.bars[i];
                    bar.index_space = i;

                    bar.setTitle(info[i], isLocked);
                    int b = rule.getIntAtribute("maxmultiple");
                    if (b == int.Parse(info[i]))
                        index = i;
                    bar.refresh();
                }

                maxmultiple.selectedIndex(index);
                maxmultiple.resizeDelta();
                maxmultiple.setVisible(true);
                if (!isExistUnrealSpace(this.maxmultiple))
                    radioList.add(this.maxmultiple);
            }
            else
            {
                this.maxmultiple.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新游戏模式
        /// </summary>
        public void refreshModel()
        {
            if (gamemodel == null) return;
            string[] model = rule.getLuaRuleSingleNames(Rule.MODEL_TYPE);
            if (model != null)
            {
                this.gamemodel.resize(model.Length);

                int index = 0;

                for (int i = 0; i < model.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.gamemodel.bars[i];
                    bar.index_space = i;

                    bar.setTitle(model[i], isLocked);
                    int b = rule.getIntAtribute("gameModel");
                    if (b == i)
                        index = i;
                    bar.refresh();
                }

                gamemodel.selectedIndex(index);
                gamemodel.resizeDelta();
                gamemodel.setVisible(true);
                if (!isExistUnrealSpace(this.gamemodel))
                    radioList.add(this.gamemodel);
            }
            else
            {
                this.gamemodel.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新局数
        /// </summary>
        public virtual void refreshJuShu()
        {
            this.jushu.resize(rule.jushu.Length);
            int index = 0;
            for (int i = 0; i < this.rule.jushu.Length; i++)
            {
                RuleRadioBar bar = (RuleRadioBar)jushu.bars[i];
                bar.index_space = i;
                bar.setTitle(rule.jushu[i][0] + "局<size=20>(" + this.rule.jushu[i][1] + "元宝)</size>", isLocked);
                bar.setCount(rule.jushu[i][0]);
                bar.refresh();
                if (rule.matchCount == this.rule.jushu[i][0])
                {
                    index = i;
                }
            }

            jushu.selectedIndex(index);
            jushu.resizeDelta();
            if (!isExistUnrealSpace(this.jushu))
                radioList.add(this.jushu);
        }

        /// <summary>
        /// 刷新飘
        /// </summary>
        public virtual void refreshPiao()
        {
            string[] piao = rule.getLuaRuleSingleNames(Rule.PIAO);

            if (piao != null)
            {
                piaoContainer.resize(piao.Length);
                int value = rule.getIntAtribute("piaoType");
                for (int i = 0; i < piao.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)piaoContainer.bars[i];
                    bar.index_space = i;
                    bar.setTitle(piao[i], isLocked);
                    bar.refresh();
                }

                if (value == 0)
                    value = 2;
                else if (value == 1)
                    value = 0;
                else
                {
                    value = 1;
                }
                piaoContainer.selectedIndex(value);
                piaoContainer.resizeDelta();
                piaoContainer.setVisible(true);
                if (!isExistUnrealSpace(piaoContainer))
                    radioList.add(piaoContainer);
            }
            else
            {
                piaoContainer.setVisible(false);
            }
        }


        string[] applayExitTimes = new string[] { "1分钟", "2分钟", "3分钟", "5分钟" };

        int[] tt = new int[] { 60000, 120000, 180000, 300000 };
        
        /// <summary>
        /// 刷新申请解散房间时间
        /// </summary>
        public virtual void refreshApplyexittime()
        {
            applyexittime.resize(applayExitTimes.Length);
            int value = rule.getIntAtribute("jiesantime");
            for (int i = 0; i < applayExitTimes.Length; i++)
            {
                RuleRadioBar bar = (RuleRadioBar)applyexittime.bars[i];
                bar.index_space = i;
                bar.setTitle(applayExitTimes[i], isLocked);
                bar.setCount(tt[i]);
                bar.refresh();
                if (value == tt[i])
                    value = i;
            }

            applyexittime.selectedIndex(value);
            applyexittime.resizeDelta();
            applyexittime.setVisible(true);
            if (!isExistUnrealSpace(applyexittime))
                radioList.add(applyexittime);
        }

        /// <summary>
        /// 刷新提前解散当局，记不记录分
        /// </summary>
        public virtual void refreshTiQianJieSan()
        {

            string[] name = rule.getLuaRuleSingleNames(22);

            if (name != null)
            {
                tiQianJieSan.resize(name.Length);
                bool b = rule.getRuleAtribute("iscurrentscore");
                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)tiQianJieSan.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                }

                tiQianJieSan.selectedIndex(b ? 0 :1);
                tiQianJieSan.resizeDelta();
                tiQianJieSan.setVisible(true);
                if (!isExistUnrealSpace(tiQianJieSan))
                    radioList.add(tiQianJieSan);
            }
            else
            {
                tiQianJieSan.setVisible(false);
            }
        }



        /// <summary>
        /// 刷新翻型
        /// </summary>
        public virtual void refreshInversion()
        {
            string[] fanxing = rule.getLuaRuleSingleNames(Rule.FAN_XING);

            if (fanxing != null)
            {
                inversion.resize(fanxing.Length);
                bool b = rule.getRuleAtribute("pointType");
                for (int i = 0; i < fanxing.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)inversion.bars[i];
                    bar.index_space = i;
                    bar.setTitle(fanxing[i], isLocked);
                    bar.refresh();
                }

                inversion.selectedIndex(b ? 1 : 0);
                inversion.resizeDelta();
                inversion.setVisible(true);
                if (!isExistUnrealSpace(inversion))
                    radioList.add(inversion);
            }
            else
            {
                inversion.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新几番起胡
        /// </summary>
        public virtual void refreshcanHuPoint()
        {
            string[] canhuPoint = rule.getLuaRuleSingleNames(Rule.CAN_HU_POINT);

            if (canhuPoint != null)
            {
                canHuPointContainer.resize(canhuPoint.Length);
                int b = rule.getIntAtribute("leastfan");
                for (int i = 0; i < canhuPoint.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)canHuPointContainer.bars[i];
                    bar.index_space = i;
                    bar.setTitle(canhuPoint[i], isLocked);
                    bar.refresh();
                }

                canHuPointContainer.selectedIndex(b == 0 ? 0 : 1);
                canHuPointContainer.resizeDelta();
                canHuPointContainer.setVisible(true);
                if (!isExistUnrealSpace(canHuPointContainer))
                    radioList.add(canHuPointContainer);
            }
            else
            {
                canHuPointContainer.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新点炮
        /// </summary>
        public void refreshDianPao()
        {
            string[] dp = rule.getLuaRuleSingleNames(Rule.DIAO_PAO_TYPE);
            if (dp != null)
            {
                this.dianpao.resize(dp.Length);

                int index = 0;

                for (int i = 0; i < dp.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.dianpao.bars[i];
                    bar.index_space = i;

                    bar.setTitle(dp[i], isLocked);
                    bool b = rule.isDianPaoRule(i);
                    if (b)
                        index = i;
                    bar.refresh();
                }

                dianpao.selectedIndex(index);
                dianpao.resizeDelta();
                dianpao.setVisible(true);
                if (!isExistUnrealSpace(this.dianpao))
                    radioList.add(this.dianpao);
            }
            else
            {
                this.dianpao.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新查叫
        /// </summary>
        public void refreshChaJiao()
        {
            string[] cj = rule.getLuaRuleSingleNames(Rule.CHA_JIAO_TYPE);
            if (cj != null)
            {
                chajiao.resize(cj.Length);
                bool b = rule.getRuleAtribute("chajia");
                for (int i = 0; i < cj.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)chajiao.bars[i];
                    bar.index_space = i;
                    bar.setTitle(cj[i], isLocked);
                    bar.refresh();
                }

                chajiao.selectedIndex(b ? 1 : 0);
                chajiao.resizeDelta();
                chajiao.setVisible(true);
                if (!isExistUnrealSpace(chajiao))
                    radioList.add(chajiao);
            }
            else
            {
                chajiao.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新底分
        /// </summary>
        public void refreshDifen()
        {
            if (difen == null) return;
            string[] cj = rule.getLuaRuleSingleNames(Rule.DI_FEN);
            if (cj != null)
            {
                difen.resize(cj.Length);
                int index = -1;
                int point = rule.getBet()/100;
                int count = 0;
                for (int i = 0; i < cj.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)difen.bars[i];
                    bar.index_space = i;
                    bar.setTitle(cj[i], isLocked);
                    count = StringKit.parseInt(cj[i]);
                    bar.setCount(count);
                    bar.refresh();
                    if (count == point)
                        index = i;
                }

                difen.selectedIndex(index);
                difen.resizeDelta();
                difen.setVisible(true);
                if (!isExistUnrealSpace(difen))
                    radioList.add(difen);
            }
            else
            {
                difen.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新倍分
        /// </summary>
        public void refreshBeifen()
        {
            if (beifen == null) return;
            string[] cj = rule.getLuaRuleSingleNames(30);
            if (cj != null)
            {
                beifen.resize(cj.Length);
                int index = -1;
                int befen = rule.getIntAtribute("beifen");
                int count = 0;
                for (int i = 0; i < cj.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)beifen.bars[i];
                    bar.index_space = i;
                    bar.setTitle(cj[i]+"倍", isLocked);
                    count = StringKit.parseInt(cj[i]);
                    bar.setCount(count);
                    bar.refresh();
                    if (count == befen)
                        index = i;
                }

                beifen.selectedIndex(index);
                beifen.resizeDelta();
                beifen.setVisible(true);
                if (!isExistUnrealSpace(beifen))
                    radioList.add(beifen);
            }
            else
            {
                beifen.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新自摸增益
        /// </summary>
        public void refreshZiMo()
        {
            string[] ziMo = rule.getLuaRuleSingleNames(Rule.ZI_MO_ADDTION_TYPE);
            if (ziMo != null)
            {
                this.zimocontainer.resize(ziMo.Length);
                int index = 0;
                for (int i = 0; i < ziMo.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.zimocontainer.bars[i];
                    bar.index_space = i;
                    bar.setTitle(ziMo[i], isLocked);
                    if (i == rule.getIntAtribute("zimoAddtion") - 1)
                        index = i;
                    bar.refresh();
                }

                zimocontainer.selectedIndex(index);
                this.zimocontainer.resizeDelta();
                this.zimocontainer.setVisible(true);
                if (!isExistUnrealSpace(this.zimocontainer))
                    radioList.add(this.zimocontainer);
            }
            else
            {
                this.zimocontainer.setVisible(false);
            }
        }


        /// <summary>
        /// 刷新点杠花
        /// </summary>
        public void refreshDGHua()
        {
            string[] ziMo = rule.getLuaRuleSingleNames(Rule.KONG_ADDTING_TYPE);
            if (ziMo != null)
            {
                dghuacontainer.resize(ziMo.Length);
                bool b = rule.getRuleAtribute("konghuasself");
                for (int i = 0; i < ziMo.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.dghuacontainer.bars[i];
                    bar.index_space = i;
                    bar.setTitle(ziMo[i], isLocked);
                    bar.refresh();
                }

                dghuacontainer.selectedIndex(b ? 1 : 0);
                dghuacontainer.resizeDelta();
                dghuacontainer.setVisible(true);
                if (!isExistUnrealSpace(dghuacontainer))
                    radioList.add(dghuacontainer);
            }
            else
            {
                dghuacontainer.setVisible(false);
            }
        }


        /// <summary>
        /// 刷新换三张
        /// </summary>
        public void refreshHuansz()
        {
            string[] replace = rule.getLuaRuleSingleNames(Rule.REPLACE_COUNT_TYPE);
            if (replace != null)
            {
                huanszcontainer.resize(replace.Length);
                int b = rule.getIntAtribute("replacecount");
                bool isSameCount = rule.getRuleAtribute("replaceSame");

                int index = -1;
                for (int i = 0; i < replace.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.huanszcontainer.bars[i];
                    bar.index_space = i;
                    bar.setTitle(replace[i], isLocked);
                    if (i == 0 && b == 0)
                        index = i;
                    else if (i == 1 && b == 3 && !isSameCount)
                    {
                        index = i;
                    }
                    else if (i == 2 && b == 3 && isSameCount)
                    {
                        index = i;
                    }

                    bar.refresh();
                }

                huanszcontainer.selectedIndex(index);
                huanszcontainer.resizeDelta();
                huanszcontainer.setVisible(true);
                if (!isExistUnrealSpace(this.huanszcontainer))
                    radioList.add(this.huanszcontainer);
            }
            else
            {
                huanszcontainer.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新选番
        /// </summary>
        public void refreshXuanFan()
        {
            string[] titles = rule.getLuaRuleSingleNames(32);
            if (titles != null)
            {
                this.xuanfan.resize(titles.Length);
                int index = 0;

                int value = rule.getIntAtribute("hasPoint");

                for (int i = 0; i < titles.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.xuanfan.bars[i];
                    bar.index_space = i;
                    bar.setTitle(titles[i], isLocked);
                    bar.refresh();
                    if (i==value)
                        index = i;
                }

                xuanfan.selectedIndex(index);
                xuanfan.resizeDelta();
                xuanfan.setVisible(true);
                if (!isExistUnrealSpace(this.xuanfan))
                    radioList.add(this.xuanfan);
            }
            else
            {
                xuanfan.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新番数
        /// </summary>
        public void refreshFaShu()
        {
            int[] fanshu = rule.getFanShu();
            if (fanshu != null)
            {

                this.fengding.resize(fanshu.Length);
                int index = 0;
                for (int i = 0; i < fanshu.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.fengding.bars[i];
                    bar.index_space = i;
                    bar.setTitle(fanshu[i] + getFengDingInfo(), isLocked);
                    bar.setCount(fanshu[i]);
                    bar.refresh();
                    if (rule.maxPoint == fanshu[i])
                        index = i;
                }

                fengding.selectedIndex(index);
                fengding.resizeDelta();
                fengding.setVisible(true);
                if (!isExistUnrealSpace(this.fengding))
                    radioList.add(this.fengding);
            }
            else
            {
                fengding.setVisible(false);
            }
        }

        private string getFengDingInfo()
        {
            if (rule.sid == 2011)//海南麻将
            {
                return "倍";
            }
            else
            {
                return "番";
            }
        }

        /// <summary>
        /// 刷新规则玩法,子游戏
        /// </summary>
        public void refreshRuleWf()
        {
            string[] name = rule.getLuaRuleSingleNames(24);
            if (name != null)
            {
                ruleWf.resize(name.Length);
                int index = rule.getIntAtribute("rulewf");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.ruleWf.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                    if (index == i)
                    {
                        index = i;
                    }
                }
                ruleWf.selectedIndex(index);
                ruleWf.resizeDelta();
                ruleWf.setVisible(true);
                if (!isExistUnrealSpace(ruleWf))
                    radioList.add(ruleWf);
            }
            else
            {
                ruleWf.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新连庄
        /// </summary>
        public void refreshRemainBanker()
        {
            string[] name = rule.getLuaRuleSingleNames(25);
            if (name != null)
            {
                remainBanker.resize(name.Length);
                int index = rule.getIntAtribute("remianbanker");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.remainBanker.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                    if (index == i)
                    {
                        index = i;
                    }
                }
                remainBanker.selectedIndex(index);
                remainBanker.resizeDelta();
                remainBanker.setVisible(true);
                if (!isExistUnrealSpace(remainBanker))
                    radioList.add(remainBanker);
            }
            else
            {
                remainBanker.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新连庄
        /// </summary>
        public void refreshMakerBanker()
        {
            string[] name = rule.getLuaRuleSingleNames(26);
            if (name != null)
            {
                makeBanker.resize(name.Length);
                int makebanker = rule.getIntAtribute("makebanker");
                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.makeBanker.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                }
                makeBanker.selectedIndex(makebanker);
                makeBanker.resizeDelta();
                makeBanker.setVisible(true);
                if (!isExistUnrealSpace(makeBanker))
                    radioList.add(makeBanker);
            }
            else
            {
                makeBanker.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新卖分
        /// </summary>
        public void refreshSaleScore()
        {
            string[] name = rule.getLuaRuleSingleNames(27);
            if (name != null)
            {
                salescore.resize(name.Length);
                int index = rule.getIntAtribute("salescore");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.salescore.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                    if (index == i)
                    {
                        index = i;
                    }
                }
                salescore.selectedIndex(index);
                salescore.resizeDelta();
                salescore.setVisible(true);
                if (!isExistUnrealSpace(salescore))
                    radioList.add(salescore);
            }
            else
            {
                salescore.setVisible(false);
            }
        }

        /// <summary>
        /// 必胡
        /// </summary>
        public void refreshMustHu()
        {
            string[] name = rule.getLuaRuleSingleNames(28);
            if (name != null)
            {
                musthu.resize(name.Length);
                bool ismusthu = rule.getRuleAtribute("musthu");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.musthu.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                }
                musthu.selectedIndex(ismusthu ? 0 : 1);
                musthu.resizeDelta();
                musthu.setVisible(true);
                if (!isExistUnrealSpace(musthu))
                    radioList.add(musthu);
            }
            else
            {
                musthu.setVisible(false);
            }
        }

        /// <summary>
        /// 轮庄
        /// </summary>
        public void refreshLunZhuang()
        {
            string[] name = rule.getLuaRuleSingleNames(29);
            if (name != null)
            {
                lunZhuang.resize(name.Length);
                bool islunzhuang = rule.getRuleAtribute("lunzhuang");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.lunZhuang.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                }
                lunZhuang.selectedIndex(islunzhuang ? 0 : 1);
                lunZhuang.resizeDelta();
                lunZhuang.setVisible(true);
                if (!isExistUnrealSpace(lunZhuang))
                    radioList.add(lunZhuang);
            }
            else
            {
                lunZhuang.setVisible(false);
            }
        }



        /// <summary>
        /// 炸弹加倍
        /// </summary>
        public void refreshMultipScore()
        {
            string[] name = rule.getLuaRuleSingleNames(31);
            if (name != null)
            {
                multipScore.resize(name.Length);
                int score = rule.getIntAtribute("multipScore");

                for (int i = 0; i < name.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.multipScore.bars[i];
                    bar.index_space = i;
                    bar.setTitle(name[i], isLocked);
                    bar.refresh();
                }

                if (score == 5)
                    score = 1;
                else if (score == 10)
                    score = 2;
                multipScore.selectedIndex(score);
                multipScore.resizeDelta();
                multipScore.setVisible(true);
                if (!isExistUnrealSpace(multipScore))
                    radioList.add(multipScore);
            }
            else
            {
                multipScore.setVisible(false);
            }
        }

        

        /// <summary>
        /// 刷新人数
        /// </summary>
        public void refreshPlayNum()
        {
            int[] pnums = this.rule.getPlayerNum();
            if (pnums != null)
            {
                this.playersnum.resize(pnums.Length);
                int index = 0;
                for (int i = 0; i < pnums.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.playersnum.bars[i];
                    bar.index_space = i;
                    bar.setTitle(pnums[i] + "人", isLocked);
                    if (rule.playerCount == pnums[i])
                        index = i;
                    bar.refresh();
                }

                playersnum.selectedIndex(index);
                playersnum.resizeDelta();
                playersnum.setVisible(true);
                if (!isExistUnrealSpace(this.playersnum))
                    radioList.add(this.playersnum);
            }
            else
            {
                playersnum.setVisible(false);
            }
        }

        /// <summary>
        /// 刷新打法
        /// </summary>
        public void refreshDafa()
        {
            string[] dafa = this.rule.getLuaRuleSingleNames(Rule.DAFA);
            if (dafa != null)
            {
                this.dafa.resize(dafa.Length);
                int index = 0;
                for (int i = 0; i < dafa.Length; i++)
                {
                    RuleRadioBar bar = (RuleRadioBar)this.dafa.bars[i];
                    bar.index_space = i;
                    bar.setTitle(dafa[i], isLocked);
                    if (rule.getIntAtribute("dafa") == i) index = i;
                    bar.refresh();
                }

                this.dafa.selectedIndex(index);
                this.dafa.resizeDelta();
                this.dafa.setVisible(true);
                if (!isExistUnrealSpace(this.dafa))
                    radioList.add(this.dafa);
            }
            else
            {
                this.dafa.setVisible(false);
            }
        }

        public void refreshWaFa()
        {
            string[] wanfa = rule.getWanFa();

            if (wanfa != null)
            {
                this.wanfa.resize(wanfa.Length);
                for (int i = 0; i < wanfa.Length; i++)
                {
                    WanFaBar bar = (WanFaBar)this.wanfa.bars[i];
                    bar.index_space = i;
                    bar.setData(wanfa[i], rule.isSelected(i), isLocked);
                    bar.refresh();
                }

                this.wanfa.resizeDelta();
                this.wanfa.setVisible(true);
                if (!isExistUnrealSpace(this.wanfa))
                    radioList.add(this.wanfa);
            }
            else
                this.wanfa.setVisible(false);
        }

        protected bool isExistUnrealSpace(UnrealSpaceObject space)
        {
            for (int i = 0; i < radioList.count; i++)
            {
                if (radioList.get(i) == space)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 无托管
        /// </summary>
        private int noTrustee = -1;

        /// <summary>
        /// 有托管
        /// </summary>
        private int trustee = 1;

        /// <summary>
        /// 刷新托管方式
        /// </summary>
        public void refreshTrustessShip()
        {
            trusteeship.resize(2);
            string content = null;
            bool isCheck = false;
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                RuleRadioBar bar = (RuleRadioBar)this.trusteeship.bars[i];
                bar.index_space = i;
                content = (i == 0 ? "无" : "有");
                isCheck = false;
                bool istrust = rule.getTrusteeship() == trustee;
                if ((i == 0 && !istrust) || i == 1 && istrust)
                    isCheck = true;

                bar.setTitle(content, isLocked);

                bar.setCount((i == 0 ? noTrustee : trustee));
                if (isCheck)
                    index = i;
                bar.refresh();
            }

            trusteeship.selectedIndex(index);
            this.trusteeship.resizeDelta();

            if (!isExistUnrealSpace(trusteeship))
                radioList.add(trusteeship);
        }

        /// <summary>
        /// 刷新托管时间
        /// </summary>
        public void refreshTrustessShipTime()
        {
            int[] times = rule.getTrusteeshipTimeArray();
            trusteeshiptime.resize(times.Length);
            string content = null;
            int time = 0;
            bool isShow = rule.getTrusteeship() == trustee;
            int trusteetime = rule.getTrusteeTime();
            if (trusteetime == 0) trusteetime = times[0];

            int index = -1;
            for (int i = 0; i < times.Length; i++)
            {
                RuleRadioBar bar = (RuleRadioBar)this.trusteeshiptime.bars[i];
                bar.index_space = i;
                time = times[i];
                content = time >= 60000 ? (time / 60000) + "分钟" : time / 1000 + "秒";

                bar.setTitle(content, isLocked);
                bar.setCount(times[i]);
                if (isShow && trusteetime == time)
                    index = i;
                bar.refresh();
            }

            trusteeshiptime.selectedIndex(index);
            this.trusteeshiptime.resizeDelta();
            if (!isExistUnrealSpace(trusteeshiptime))
                radioList.add(trusteeshiptime);
        }

        /// <summary>
        /// 刷新托管解散状态
        /// </summary>
        private string[] jiesanname = new string[] { "托管结束解散", "托管结束手动准备", "托管结束自动准备" };

        public void refreshTrustessJiesan()
        {
            int[] types = rule.getTrusteejstatustypes();
            trusteeshipjiesan.resize(types.Length);
            bool isShow = rule.getTrusteeship() == trustee;
            int trusteejiesan = rule.getTrusteejstatus();

            int index = -1;
            for (int i = 0; i < types.Length; i++)
            {
                RuleRadioBar bar = (RuleRadioBar)this.trusteeshipjiesan.bars[i];
                bar.index_space = i;
                bar.setTitle(jiesanname[i], isLocked);
                bar.setCount(types[i]);
                if (isShow && trusteejiesan == types[i])
                    index = i;
                bar.refresh();
            }

            trusteeshipjiesan.selectedIndex(index);
            this.trusteeshipjiesan.resizeDelta();
            if (!isExistUnrealSpace(trusteeshipjiesan))
                radioList.add(trusteeshipjiesan);
        }

        /// <summary>
        /// 刷新限制视图
        /// </summary>
        public void refreshLimitView()
        {
            limitView.setData(rule, isLocked);
            limitView.refresh();
            if (!isExistUnrealSpace(limitView))
                radioList.add(limitView);
        }

        /// <summary>
        /// 刷新托管方式，单选
        /// </summary>
        /// <param name="index"></param>
        public void selectTrustessShip(int index, int value)
        {
            trusteeship.selectedIndex(index);
            rule.setTrusteeship(value);
            refreshTrustessShipTime();
            refreshTrustessJiesan();
        }

        /// <summary>
        /// 刷新托管时间规则，单选
        /// </summary>
        /// <param name="index"></param>
        public void selectTrustessTime(int index, int value)
        {
            if (rule.getTrusteeship() == noTrustee) return;
            trusteeshiptime.selectedIndex(index);
            rule.setTrusteeTime(value);
        }

        /// <summary>
        /// 刷新托管解散规则，单选
        /// </summary>
        /// <param name="index"></param>
        public void selectTrustessJieSan(int index, int value)
        {
            if (rule.getTrusteeship() == noTrustee) return;
            trusteeshipjiesan.selectedIndex(index);
            rule.setTrusteejstatus(value);
        }


        protected override void xrelayout()
        {
            float y = 0;
            float heigth = 0;
            UnrealSpaceObject list = null;
            for (int i = 0; i < radioList.count; i++)
            {
                list = radioList.get(i);
                if (i == 0)
                {
                    y = list.transform.localPosition.y;
                    heigth = list.getHeight() / 2;
                }
                else
                {
                    heigth += list.getHeight() / 2;
                    DisplayKit.setLocalY(list.gameObject, y - heigth);
                    heigth += list.getHeight() / 2;
                }
            }

            resizeDisplayKit(y, heigth);
        }

        public virtual void resizeDisplayKit(float y, float heigth)
        {
            float moren_ytfh = 0;
            if (this.moren_ytfh_rule.gameObject.activeSelf)
                moren_ytfh = this.moren_ytfh_rule.transform.GetComponent<RectTransform>().rect.height / 2 + 10;
            heigth += moren_ytfh;
            if (this.moren_ytfh_rule.gameObject.activeSelf)
                DisplayKit.setLocalY(this.moren_ytfh_rule.gameObject, y - heigth);
            heigth += moren_ytfh;
            setting(heigth + jushu.getHeight() / 2, heigth);
        }

        private void setting(float h, float height)
        {
            if (h < height) return;
            RectTransform rectTransform = container.GetComponent<RectTransform>();
            Vector2 sizeVector2 = rectTransform.sizeDelta;
            float offset = (sizeVector2.y - h) / 2;
            sizeVector2.y = h;
            rectTransform.sizeDelta = sizeVector2;
            float y = container.localPosition.y;
            DisplayKit.setLocalY(container.gameObject, y + offset);

            scrollRect.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// 刷新玩牌人数规则，单选
        /// </summary>
        /// <param name="index"></param>
        public void refreshPlayersNum(int index)
        {
            rule.setLuaRuleSingleNames(Rule.PLAYER_COUNT_TYPE, index);
            refreshPlayNum();
            refreshWaFa();
            relayout();
        }

        /// <summary>
        /// 刷新打法，单选
        /// </summary>
        /// <param name="index"></param>
        public void refreshRuleDafa(int index)
        {
            rule.setLuaRuleSingleNames(Rule.DAFA, index);
            refreshDafa();
            relayout();
        }
    }
}
