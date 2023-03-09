using cambrian.common;

namespace basef.arena
{
    /// <summary> 新增奖品界面 </summary>
    public class ArenaAddAwardPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 金豆兑换奖章比例
        /// </summary>
        public static int MEDAL_RATIO=100;


        /// <summary> 兑换金豆条件 </summary>
        UnrealInputTextField condition;

        /// <summary>
        /// 奖章
        /// </summary>
        private UnrealTextField award;
        /// <summary>
        /// 兑换
        /// </summary>
        public ExchangeEntery entery;

        protected override void xinit()
        {
            base.xinit();
            condition = this.content.Find("centers").Find("content").Find("condition").GetComponent<UnrealInputTextField>();
            condition.init();
            condition.valueChange = awardValueChange;

            award = this.content.Find("centers").Find("content").Find("award").GetComponent<UnrealTextField>();
            award.init();
        }

        public void awardValueChange(string value)
        {
            if (value == null || value.Length == 0) return;
            award.text = (StringKit.parseLong(value) / MEDAL_RATIO)+"";
        }

        public void setExchangeEntery(ExchangeEntery entery)
        {
            this.entery = entery;
        }

        protected override void xrefresh()
        {
            if (entery != null)
            {
                condition.text = entery.getValue().ToString();
            }
            else
            {
                condition.text = "";
            }
        }

        public string getCondition()
        {
            return condition.text; 
        }

        public string getAward()
        {
            return award.text;
        }
    }
}
