using cambrian.common;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 防成谜设置视图
    /// </summary>
    public class ArenaIndulgeView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 滚动组件
        /// </summary>
        private ScrollRect scrollRect;
        /// <summary>
        /// 名称空间
        /// </summary>
        private UnrealLuaSpaceObject gold;
        /// <summary>
        /// 门票收取类型
        /// </summary>
        private UnrealTableContainer ticketType;
        /// <summary>
        /// 分数阶段
        /// </summary>
        private UnrealTableContainer point;

        private Transform container;
        /// <summary>
        /// 进房最低金豆
        /// </summary>
        public UnrealInputTextField mingoldnum;
        /// <summary>
        /// 低于好多解散
        /// </summary>
        public UnrealInputTextField diyu_gold_jiesan;
        /// <summary>
        /// 门票低于多少不反
        /// </summary>
        public UnrealInputTextField men_di_yu_bu_fa;
        /// <summary>
        /// 负金豆
        /// </summary>
        public UnrealInputTextField fu_gold;
        /// <summary>
        /// AAA支付视图
        /// </summary>
        ArenaRuleAAAView aaaView;


        [HideInInspector] public ArenaLockRule rule;

        UnrealScaleButton btn;

        protected override void xinit()
        {

            scrollRect = transform.Find("content").GetComponent<ScrollRect>();
            container = scrollRect.transform.Find("mask").Find("container");

            gold = container.Find("gold").GetComponent<UnrealLuaSpaceObject>();
            gold.init();

            fu_gold = gold.transform.Find("fugold").GetComponent<UnrealInputTextField>();
            fu_gold.init();
            fu_gold.endChange = fu_gold_back;
            men_di_yu_bu_fa = gold.transform.Find("tikect").GetComponent<UnrealInputTextField>();
            men_di_yu_bu_fa.init();
            men_di_yu_bu_fa.endChange = men_no_fan_back;
            diyu_gold_jiesan = gold.transform.Find("minjiesan").GetComponent<UnrealInputTextField>();
            diyu_gold_jiesan.init();
            diyu_gold_jiesan.endChange = diyu_gold_back;
            mingoldnum = gold.transform.Find("mingold").GetComponent<UnrealInputTextField>();
            mingoldnum.init();
            mingoldnum.endChange = minback;

            ticketType = container.Find("tikecttype").GetComponent<UnrealTableContainer>();
            ticketType.init();

            point = container.Find("point").GetComponent<UnrealTableContainer>();
            point.init();
            aaaView = container.Find("aapay").GetComponent<ArenaRuleAAAView>();
            aaaView.init();

            btn = container.Find("tikecttype").Find("btn").GetComponent<UnrealScaleButton>();
            btn.init();

        }

        public void setData(ArenaLockRule rule)
        {
            this.rule = rule.clone();

        }

        /// <summary>
        /// 记录门面类型
        /// </summary>
        public int ticketsType;
        /// <summary>
        /// 大赢家支付
        /// </summary>
        public ArrayList<TicketLevel> curTickets;
        /// <summary>
        /// AA支付等级
        /// </summary>
        public TicketLevel aaLevel;

        /// <summary>
        /// 只保证外部调用
        /// </summary>
        protected override void xrefresh()
        {
            long value = rule.getLimitGold();
            if (value == 0)
                value = 1;
            mingoldnum.text = value.ToString();
            value = rule.getDisbandThreshold();
            if (value == 0)
                value = 1;
            diyu_gold_jiesan.text = value.ToString();
            men_di_yu_bu_fa.text = rule.getThReshold().ToString();
            if (rule.rule.sid == 2011)
            {
                fu_gold.gameObject.SetActive(true);
            }
            else
                fu_gold.gameObject.SetActive(false);
            fu_gold.text = rule.getOverDraft().ToString();

            refreshTicketType(rule.mpType);
            ticketsType = rule.mpType;
            curTickets = rule.list;

            if (ticketsType == ArenaLockRule.TYPE_AA)
            {
                aaLevel = new TicketLevel(rule.list.get(0).value);
                curTickets.clear();
                curTickets.add(new TicketLevel(0, 0, 0, 0));
                curTickets.add(new TicketLevel());
                rule.list = curTickets;
            }

            verticalPosition = 1;

            refreshShow();
        }


        public int verticalPosition = 1;

        public void refresh1()
        {
            verticalPosition = 0;
            refreshShow();
        }

        public void refreshTicketType(int type)
        {
            ticketType.resize(2);
            for (int i = 0; i < 2; i++)
            {
                ArenaRuleTicketTypeBar bar = (ArenaRuleTicketTypeBar)ticketType.bars[i];
                bar.setData(type);
                bar.index_space = i;
                bar.refresh();
            }
        }

        public void diyu_gold_back(string value)
        {
            if (value == null || "".Equals(value) || "0".Equals(value))
                diyu_gold_jiesan.text = rule.getDisbandThreshold().ToString();
            else
            {
                long v = StringKit.parseLong(value);
                if (v < 0)
                {
                    Alert.autoShow("只能输入>0的值");
                    v = rule.getDisbandThreshold();
                    if (v == 0)
                        v = 1;
                }

                rule.setDisbandThreshod(StringKit.parseInt(value));
                diyu_gold_jiesan.text = v.ToString();
            }
        }

        public void fu_gold_back(string value)
        {
            Regex reg = new Regex(@"^\d+\.\d+$");
            if (value == null || "".Equals(value))
                fu_gold.text = rule.getOverDraft().ToString();
            else if (reg.IsMatch(value))
            {
                Alert.autoShow("只能输入整数");
                fu_gold.text = rule.getOverDraft().ToString();
            }
            else
            {
                long v = StringKit.parseLong(value);
                if (v < -1)
                {
                    Alert.autoShow("只能输入>-1的值");
                    v = rule.getOverDraft();
                }
                rule.setOverDraft((int)v);
                fu_gold.text = v.ToString();
            }
        }

        public void men_no_fan_back(string value)
        {
            if (value == null || "".Equals(value))
                men_di_yu_bu_fa.text = rule.getThReshold().ToString();
            else
            {
                long v = StringKit.parseLong(value);
                if (v < 0)
                {
                    Alert.autoShow("只能输入>0的值");
                    v = rule.getThReshold();
                }

                rule.setThreshold((int)v);
                men_di_yu_bu_fa.text = v.ToString();
            }
        }

        public void minback(string value)
        {
            if (value == null || "".Equals(value) || "0".Equals(value))
            {
                long v = rule.getLimitGold();
                if (v == 0)
                    v = 1;
                mingoldnum.text = v.ToString();
            }
            else
            {
                long v = StringKit.parseLong(value);
                if (v < 0)
                {
                    Alert.autoShow("只能输入>0的值");
                    v = rule.getLimitGold();
                    if (v == 0)
                        v = 1;
                }
                rule.setLimitGold(v);
                mingoldnum.text = v.ToString();
            }
        }

        public void refreshShow()
        {
            if (ticketsType == ArenaLockRule.TYPE_STEP)
                refreshPoint();
            else
            {
                if (aaLevel != null)
                    aaaView.setData(aaLevel);
                else
                {
                    aaLevel = new TicketLevel(100);
                    aaaView.setData(aaLevel);
                }
                aaaView.refresh();
            }

            point.setVisible(ticketsType == ArenaLockRule.TYPE_STEP);
            aaaView.setVisible(ticketsType == ArenaLockRule.TYPE_AA);
            btn.setVisible(ticketsType == ArenaLockRule.TYPE_STEP);
            relayout();
        }

        /// <summary>
        /// 刷新分数阶段
        /// </summary>
        public void refreshPoint()
        {
            this.point.resize(curTickets.count);
            for (int i = 0; i < curTickets.count; i++)
            {
                ArenaIndulgePointBar bar = (ArenaIndulgePointBar)this.point.bars[i];
                bar.index_space = i;
                bar.setData(curTickets.get(i));
                bar.refresh();
            }

            this.point.resizeDelta();
        }


        protected override void xrelayout()
        {
            float y = this.gold.transform.localPosition.y;

            float heigth = this.gold.getHeight() / 2;

            if (this.ticketType.getVisible())
            {
                heigth += this.ticketType.getHeight() / 2;
                DisplayKit.setLocalY(this.ticketType.gameObject, y - heigth);
                heigth += this.ticketType.getHeight() / 2;
            }

            if (this.point.getVisible())
            {
                heigth += this.point.getHeight() / 2;
                DisplayKit.setLocalY(this.point.gameObject, y - heigth);
                heigth += this.point.getHeight() / 2;
            }

            if (this.aaaView.getVisible())
            {
                heigth += this.aaaView.getHeight() / 2;
                DisplayKit.setLocalY(this.aaaView.gameObject, y - heigth);
                heigth += this.aaaView.getHeight() / 2;
            }

            resizeDisplayKit(y, heigth);
        }

        public void resizeDisplayKit(float y, float heigth)
        {
            setting(heigth + gold.getHeight() / 2, heigth);
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

            scrollRect.verticalNormalizedPosition = verticalPosition;
        }
    }
}
