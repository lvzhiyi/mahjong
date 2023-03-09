using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 杠牌操作视图
    /// </summary>
    public class MJKongOperateView:UnrealLuaSpaceObject
    {
        UnrealTableContainer container;

        [HideInInspector] public MJOperateInfoBean bean;

        float containerInitWidth;

        float initwidth;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
            containerInitWidth = container.getWidth();
            initwidth = getWidth();
        }

        public void setBean(MJOperateInfoBean bean)
        {
            this.bean = bean;
        }

        protected override void xrefresh()
        {
            MJOperationCardBean[] cards= bean.cards;
            int len = cards.Length;
            container.cols = len>3?3:len;
            container.resize(len);
            for (int i=0;i< len; i++)
            {
                MJKongOperateBar bar=(MJKongOperateBar)container.bars[i];
                bar.setBean(cards[i]);
                bar.refresh();
            }
            container.resizeDelta();
            float cwidth = container.getWidth()- containerInitWidth;
            setWidth(initwidth+cwidth);
        }

    }
}
