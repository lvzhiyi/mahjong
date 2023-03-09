using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 比赛奖品 bar </summary>
    public class ArenaExchangeMatchContentBar : UnrealLuaSpaceObject
    {
        Image gifts;

        UnrealTextField title;

        UnrealTextField price;

        private ExchangeEntery entery;

        protected override void xinit()
        {
            title = transform.Find("title").GetComponent<UnrealTextField>();
            title.init();

            price = transform.Find("price").GetComponent<UnrealTextField>();
            price.init();

            gifts = transform.Find("gifts").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            title.text = "奖章X" + (entery.getValue() / ArenaAddAwardPanel.MEDAL_RATIO);
            price.text = entery.getValue() + "X积分";
        }

        public void setData(ExchangeEntery entery)
        {
            this.entery = entery;
        }

        /// <summary> 获取商品UUID </summary>
        public int getUuid()
        {
            return this.entery.uuid;
        }

        public ExchangeEntery getData()
        {
            return entery;
        }
    }
}
