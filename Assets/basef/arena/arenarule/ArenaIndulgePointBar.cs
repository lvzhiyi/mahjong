using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 防成谜分数bar
    /// </summary>
    public class ArenaIndulgePointBar : UnrealLuaSpaceObject
    {
        private TicketLevel level;

        private UnrealTextField minpoint;

        private UnrealInputTextField maxpoint;

        private UnrealInputTextField ticket;

        Text titles;

        private Image bg;
        /// <summary>
        /// 删除按钮
        /// </summary>
        UnrealScaleButton btn;
        /// <summary>
        /// 固定收取金豆值
        /// </summary>
        private UnrealInputTextField guding;
        Text text4;

        protected override void xinit()
        {
            base.xinit();
            minpoint = transform.Find("minpoint").GetComponent<UnrealTextField>();
            minpoint.init();
            maxpoint = transform.Find("maxpoint").GetComponent<UnrealInputTextField>();
            maxpoint.init();
            maxpoint.endChange = maxpointchange;
            ticket = transform.Find("ticket").GetComponent<UnrealInputTextField>();
            ticket.init();
            ticket.endChange = ticketchange;
            bg = transform.Find("bg").GetComponent<Image>();
            titles = transform.Find("text").GetComponent<Text>();
            btn = transform.Find("btn").GetComponent<UnrealScaleButton>();
            text4 = transform.Find("text_4").GetComponent<Text>();
            guding = transform.Find("ticket1").GetComponent<UnrealInputTextField>();
            guding.init();
            guding.endChange = gudingchange;
        }

        public void ticketchange(string str)
        {
            if (str == null || str.Length == 0)
            {
                ticket.text = level.getValue().ToString();
                return;
            }
            double v = StringKit.parseDouble(str);
            if (v < 0)
                v = level.getValue();
            level.setValue(MathKit.getLongEnlarge100(v));
            ticket.text = v.ToString();
        }
        /// <summary>
        /// 获取固定金豆值
        /// </summary>
        /// <param name="str"></param>
        public void gudingchange(string str)
        {
            if (str == null || str.Length == 0)
            {
                guding.text = level.getGoldValue().ToString();
                return;
            }
            double v = StringKit.parseDouble(str);
            if (v > level.getValue())
            {
                Alert.autoShow("输入值不正确，必须小于收取积分值");
                guding.text = level.getGoldValue().ToString();
                return;
            }
            if (v < 0)
                v = level.getGoldValue();
            level.setGoldValue(MathKit.getLongEnlarge100(v));
            guding.text = v.ToString();
        }

        public void maxpointchange(string str)
        {
            if (str == null || str.Length == 0)
            {
                maxpoint.text = level.getMaxScore().ToString();
                return;
            }

            if (!StringKit.isNumber(str))
            {
                maxpoint.text = level.getMaxScore().ToString();
                return;
            }

            double maxScore = StringKit.parseDouble(str);
            if (index_space != 0)
            {
                //保底分数可以设置相等
                if (maxScore <= level.getMinScore())
                {
                    Alert.autoShow("输入值不正确，必须大于该阶段最小值");
                    maxpoint.text = level.getMaxScore().ToString();
                    return;
                }
            }
            else
            {
                if (maxScore < level.getMinScore())
                {
                    Alert.autoShow("输入值不正确，必须大于该阶段最小值");
                    maxpoint.text = level.getMaxScore().ToString();
                    return;
                }
            }
            level.setMaxScore(MathKit.getLongEnlarge100(maxScore));
            //ArenaRulePanel panel = getRoot<ArenaRulePanel>();
            ArenaWfNextPanel panel = getRoot<ArenaWfNextPanel>();
            panel.indulgeView.rule.updateTicketLevel(index_space, (long)(level.getMinScore() * 100), (long)(level.getMaxScore() * 100), (long)(level.getValue() * 100), (long)(level.getGoldValue() * 100));
            panel.indulgeView.refresh();
        }




        public void setData(TicketLevel level)
        {
            this.level = level;
        }



        protected override void xrefresh()
        {
            minpoint.text = level.getMinScore().ToString();
            bg.gameObject.SetActive(level.maxScore == -1);
            if (level.maxScore == -1)
            {
                maxpoint.text = "无上限";
            }
            else
            {
                maxpoint.text = level.getMaxScore().ToString();
            }

            ticket.text = level.getValue().ToString();

            guding.text = level.getGoldValue().ToString();

            text4.gameObject.SetActive(true);
            guding.gameObject.SetActive(true);
            if (index_space == 0)
            {
                text4.gameObject.SetActive(false);
                guding.gameObject.SetActive(false);
            }
            titles.text = index_space == 0 ? "保底分数" : "大赢家分数";

            btn.setVisible(index_space > 1);
        }
    }
}
