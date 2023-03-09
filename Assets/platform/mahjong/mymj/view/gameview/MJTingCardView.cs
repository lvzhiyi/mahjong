using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将听牌视图
    /// </summary>
    public class MJTingCardView:UnrealLuaSpaceObject
    {
        TingCardsInfo cardsInfo;

        UnrealTextField count;

        UnrealTableContainer container;

        float initwidth;

        float containerinitwidth;

        protected override void xinit()
        {
            count = transform.Find("count").GetComponent<UnrealTextField>();
            count.init();
            container = transform.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
            containerinitwidth = container.getWidth();
            initwidth = getWidth();
        }

        public void setTingCardsInfo(TingCardsInfo info)
        {
            this.cardsInfo = info;
        }

        protected override void xrefresh()
        {
            count.text = cardsInfo.count.ToString();
            ArrayList<TingCard> list= cardsInfo.getTingList();
            container.cols = list.count;
            container.resize(list.count);
            for (int i=0;i<list.count;i++)
            {
                MJTingCardBar bar= (MJTingCardBar)container.bars[i];
                bar.setTingCard(list.get(i));
                bar.refresh();
            }
            container.resizeDelta();
            setWidth(initwidth+container.getWidth()-containerinitwidth);
        }
    }
}
