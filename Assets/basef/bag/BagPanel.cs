using cambrian.common;
using basef.player;
using UnityEngine;

namespace basef.bag
{
    public class BagPanel:UnrealLuaPanel
    {
        /// <summary> 金豆 </summary>
        UnrealTextField gold;

        /// <summary> 房卡 </summary>
        UnrealTextField cards;

        /// <summary> 钻石 </summary>
        UnrealTextField diamond;

        UnrealTableContainer container;

        [HideInInspector] public PropInfoView infoView;

        /// <summary> 初始数量 </summary>
        public int init_size;

        protected override void xinit()
        {
            base.xinit();
            this.gold = this.content.Find("gold").Find("num").GetComponent<UnrealTextField>();
            this.cards = this.content.Find("roomcard").Find("num").GetComponent<UnrealTextField>();
            this.diamond = this.content.Find("diamond").Find("num").GetComponent<UnrealTextField>();
            this.container = this.content.Find("content").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.infoView = this.content.Find("propinfo").GetComponent<PropInfoView>();
            this.infoView.init();
        }

        protected override void xrefresh()
        {
            this.cards.text = PlayerToken.instance.token+"";
            this.gold.text = MathKit.getDecimal(Player.player.money);
            this.diamond.text = Player.player.diamond+"";

            int size = Bag.bag.props.count>init_size? Bag.bag.props.count:init_size;
            this.container.resize(size);
            infoView.setVisible(false);
            for (int i=0;i<size;i++)
            {
                BagBar bar=(BagBar)this.container.bars[i];
                bar.index_space = i;
                bar.select(false);
                bar.prop = null;
                if(i< Bag.bag.props.count)
                {
                    bar.setProp(Bag.bag.props.get(i));
                    if (i == 0)
                    {
                        bar.select(true);
                        infoView.setProp(Bag.bag.props.get(i));
                        infoView.setVisible(true);
                        infoView.refresh();
                    }
                }
                bar.refresh();
            }
            this.container.resizeDelta();
        }

        public void select(int index)
        {
            for (int i = 0; i < this.container.size; i++)
            {
                BagBar bar = (BagBar)this.container.bars[i];
                bar.select(index == i);
            }
        }
    }
}
