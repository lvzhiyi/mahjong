using scene.game;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 实物奖品 bar </summary>
    public class ArenaExchangePhysicalContentBar : UnrealLuaSpaceObject
    {
        Image gifts;

        UnrealTextField title;

        /// <summary> 显示金额,奖章数量 </summary>
        UnrealTextField price;


        public ArenaExchangePhysicalGoodsData data;

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
            title.text = data.title;
            price.text = "奖章X" + data.price2+"+"+data.price1+"元";
            //设置奖励图片
            gifts.sprite = CacheManager.shopimg[data.imgindex];
            gifts.SetNativeSize();
        }

        public void setData(ArenaExchangePhysicalGoodsData data)
        {
            this.data = data;
        }

        public int getSid()
        {
            return data.sid;
        }

        public string getTitleName()
        {
            return title.text;
        }

        public string getPrice()
        {
            return price.text;
        }
    }
}
