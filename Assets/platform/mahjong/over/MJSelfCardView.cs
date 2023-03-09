using basef.player;
using cambrian.common;
using cambrian.uui;
using platform.spotred;
using platform.spotred.waitroom;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJSelfCardView : UnrealLuaSpaceObject
    {

        /// <summary>
        /// 玩家牌
        /// </summary>
        MJPlayerCards pcard;

        /// 手牌容器
        /// </summary>
        UnrealTableContainer handContainer;
        /// <summary>
        /// 固定牌容器
        /// </summary>
        UnrealTableContainer fixedContainer;

        UnrealLuaSpaceObject huCardView;

        Image hucard;

        Image hubg;

        /// <summary>
        /// 分数
        /// </summary>
        UnrealTextField pointwin;
        /// <summary>
        /// 分数
        /// </summary>
        UnrealTextField pointlose;

        protected override void xinit()
        {
            handContainer = transform.Find("hand").GetComponent<UnrealTableContainer>();
            handContainer.init();
            fixedContainer = transform.Find("fixed").GetComponent<UnrealTableContainer>();
            fixedContainer.init();
            huCardView = transform.Find("hucard").GetComponent<UnrealLuaSpaceObject>();
            hucard = huCardView.transform.Find("card").GetComponent<Image>();
            hubg = huCardView.transform.Find("bg").GetComponent<Image>();
            pointwin = transform.Find("pointwin").GetComponent<UnrealTextField>();
            pointwin.init();
            pointlose = transform.Find("pointlose").GetComponent<UnrealTextField>();
            pointlose.init();
        }

        public void setData(MJPlayerCards pcard)
        {
            this.pcard = pcard;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            showPoint();
            showFixedCards();
            showHandCards();
        }

        private void showPoint()
        {
            long point = pcard.point;
            pointwin.gameObject.SetActive(point >= 0);
            pointlose.gameObject.SetActive(point < 0);

            if (point > 0)
                pointwin.text = "+" + MathKit.getRound2LongStr(point);
            else if (point < 0)
                pointlose.text = MathKit.getRound2LongStr(point);
            else
                pointwin.text = MathKit.getRound2LongStr(point);
        }

        /// <summary>
        /// 显示手牌
        /// </summary>
        private void showHandCards()
        {
            float fixedWidth = fixedContainer.getWidth();
            ArrayList<int> list= pcard.handcards;

            int mocard = pcard.mocard;

            handContainer.cols = list.count;
            handContainer.resize(list.count);
            for (int i=0;i< list.count;i++)
            {
                MJOverHandBar bar=(MJOverHandBar)handContainer.bars[i];
                bar.setCard(list.get(i));
                bar.refresh();
            }

            handContainer.resizeDelta();

            float x = fixedContainer.transform.localPosition.x + fixedWidth;

            DisplayKit.setLocalX(handContainer, x);

            bool ishu = pcard.hasStatus(MJPlayerCards.STATUS_HU);

            huCardView.setVisible(ishu);

            if (ishu) hucard.sprite = MJWidgetManager.getInstance().getCard(mocard);

            DisplayKit.setLocalX(huCardView,handContainer.transform.localPosition.x+handContainer.getWidth()+ huCardView.getWidth());

        }

        private void showFixedCards()
        {
            ArrayList<FixedCards> list = pcard.fixCards;
            fixedContainer.cols = list.count;
            fixedContainer.resize(list.count);
            for (int i = 0; i < list.count; i++)
            {
                MJOverFixedBar bar = (MJOverFixedBar)fixedContainer.bars[i];
                bar.setFixedCards((MJFixedCards)list.get(i));
                bar.refresh();
            }

            fixedContainer.resizeDelta();
        }
    }
}
