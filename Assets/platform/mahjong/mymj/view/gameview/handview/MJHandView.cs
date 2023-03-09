using platform.spotred;
using System;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家的手牌
    /// </summary>
    public class MJHandView:UnrealLuaSpaceObject
    {
        MJHandDetailView bottom;

        MJHandDetailView right;

        MJHandDetailView top;

        MJHandDetailView left;

        protected override void xinit()
        {
            bottom = transform.Find("loc0").GetComponent<MJHandDetailView>();
            bottom.init();

            right = transform.Find("loc1").GetComponent<MJHandDetailView>();
            right.init();

            top = transform.Find("loc2").GetComponent<MJHandDetailView>();
            top.init();

            left = transform.Find("loc3").GetComponent<MJHandDetailView>();
            left.init();
        }

        protected override void xrefresh()
        {
            bottom.refresh();
            right.refresh();
            top.refresh();
            left.refresh();
        }

        /// <summary>
        /// 刷新固定区的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        public void refreshFixedCard(int index, FixedCards[] cards)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.refreshFixedCard(cards,index);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.refreshFixedCard(cards,index);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.refreshFixedCard(cards,index);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.refreshFixedCard(cards,index);
                    break;
            }
        }

        /// <summary>
        /// 刷新固定区的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        public void refreshBaoKong(int[] cards,int index)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.refreshBaoKong(cards, index);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.refreshBaoKong(cards, index);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.refreshBaoKong(cards, index);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.refreshBaoKong(cards, index);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        /// <param name="mocard"></param>
        /// <param name="ting"></param>
        /// <param name="distype"></param>
        /// <param name="hasDistypeCard"></param>
        public void refreshHandCard(int index, int[] cards, MJMOCard mocard, TingCardsInfo[] ting,int distype,bool hasDistypeCard,bool isMoveMo)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.refreshHandCard(cards, mocard,index,ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.refreshHandCard(cards, mocard, index,ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.refreshHandCard(cards, mocard, index,ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.refreshHandCard(cards, mocard, index, ting, distype, hasDistypeCard, isMoveMo);
                    break;
            }
        }

        /// <summary>
        /// 自己碰牌后刷新手牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        /// <param name="mocard"></param>
        /// <param name="ting"></param>
        /// <param name="distype"></param>
        /// <param name="hasDistypeCard"></param>
        public void pongRefreshHandCard(int index, int[] cards, MJMOCard mocard, TingCardsInfo[] ting, int distype, bool hasDistypeCard)
        {
            bottom.pongRefreshHandCard(cards, mocard, index, ting, distype, hasDistypeCard, false);
        }

        /// <summary>
        /// 换三张
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        /// <param name="mocard"></param>
        /// <param name="ting"></param>
        /// <param name="distype"></param>
        /// <param name="hasDistypeCard"></param>
        public void refreshHSZHandCard(int index, int[] cards, MJMOCard mocard, int distype, bool hasDistypeCard,int[] hsz, Action action)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.refreshHSZHandCard(cards, mocard, index, distype, hasDistypeCard, hsz,action);
                    break;
            }
        }

        /// <summary>
        /// 明牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        /// <param name="mocard"></param>
        /// <param name="ting"></param>
        /// <param name="distype"></param>
        /// <param name="hasDistypeCard"></param>
        public void refreshBrightCard(int index, int[] cards, MJMOCard mocard, TingCardsInfo[] ting, int distype, bool hasDistypeCard, bool isMoveMo)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.brightHandCard(cards, mocard, index, ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.brightHandCard(cards, mocard, index, ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.brightHandCard(cards, mocard, index, ting, distype, hasDistypeCard, isMoveMo);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.brightHandCard(cards, mocard, index, ting, distype, hasDistypeCard, isMoveMo);
                    break;
            }
        }



        public void refreshMoMove(int[] cards, MJMOCard mocard,int moveindex,Action back)
        {
            bottom.refreshmoCardMove(cards, mocard, MahJongPanel.LOC_DOWN,moveindex,back);
        }

        /// <summary>
        /// 刷新替换完牌后的显示(自己的牌要显示盖牌)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        /// <param name="replacenum"></param>
        public void refreshRePlaceOverHandCard(int index,int[] cards,int replacenum)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    bottom.refreshReplaceOver(cards,index, replacenum);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.refreshReplaceOver(cards, index, replacenum);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.refreshReplaceOver(cards, index, replacenum);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.refreshReplaceOver(cards, index, replacenum);
                    break;
            }
        }


        /// <summary>
        /// 刷新自己点击的手牌
        /// </summary>
        /// <param name="index"></param>
        public void refreshSelfUpHandCard(int index)
        {
            bottom.refreshUpHandCard(index);
        }

        /// <summary>
        /// 刷新自己选中的牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="mocard"></param>
        /// <param name="selectCardsIndex">索引</param>
        public void refreshSelfSeclectCard(int[] cards, MJMOCard mocard, int[] selectCardsIndex, TingCardsInfo[] ting, int distype,bool hasDistypeCard)
        {
            bottom.refreshSelectHandCard(cards, mocard, selectCardsIndex,ting, distype, hasDistypeCard);
        }
    }
}
