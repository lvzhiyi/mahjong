using System;
using DG.Tweening;
using platform.spotred;
using UnityEngine;

namespace platform.mahjong
{
    public class MJHandDetailView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 方向
        /// </summary>
        public int direction;
        /// <summary>
        /// 手牌容器
        /// </summary>
        public UnrealDivTableContainer handContainer;
        /// <summary>
        /// 固定牌区容器
        /// </summary>
        public UnrealDivTableContainer fixedContainer;
        /// <summary>
        /// 
        /// </summary>
        protected float fixedinitx;

        public float handinitx;

        public float handinity;
        /// <summary>
        /// 是否需要缩放 true:不缩放
        /// </summary>
        public bool isScale;

        protected float scalex;

        protected int[] cards;

        protected override void xinit()
        {
            handContainer = transform.Find("hand").GetComponent<UnrealDivTableContainer>();
            handContainer.init();
            fixedContainer = transform.Find("fixed").GetComponent<UnrealDivTableContainer>();
            fixedContainer.init();
            if ((direction == 0 || direction == 2)&& !isScale)
            {
                scalex = UnrealRoot.root.addScaleX;
                if (scalex > 0)
                {
                    DisplayKit.setLocalScaleX(handContainer.transform, 1 + scalex);
                    DisplayKit.setLocalScaleX(fixedContainer.transform, 1 + scalex);

                    float scalew = fixedContainer.getWidth() * scalex / 2;

                    if (direction == 0)
                    {
                        DisplayKit.setLocalX(fixedContainer, fixedContainer.transform.localPosition.x + scalew);
                        scalew = handContainer.getWidth() * scalex/2;
                        DisplayKit.setLocalX(handContainer, handContainer.transform.localPosition.x +scalew);
                    }
                    else if (direction == 2)
                    {
                        DisplayKit.setLocalX(fixedContainer, fixedContainer.transform.localPosition.x - scalew);
                        scalew = handContainer.getWidth() * scalex / 2;
                        DisplayKit.setLocalX(handContainer, handContainer.transform.localPosition.x - scalew);
                    }
                }
            }
            handinitx = handContainer.transform.localPosition.x;
            handinity = handContainer.transform.localPosition.y;
            fixedinitx = fixedContainer.transform.localPosition.x;
        }

        protected override void xrefresh()
        {
            fixedContainer.resize(0);
            handContainer.resize(0);
        }

        public virtual void refreshFixedCard(FixedCards[] cards, int postion)
        {
            fixedContainer.resize(cards.Length);
            MJBasePlayerFixedBar bar;
            for (int i = 0; i < cards.Length; i++)
            {
                bar = (MJBasePlayerFixedBar)fixedContainer.bars[i];
                bar.index_space = i;
                bar.setCardValue((MJFixedCards)cards[i], postion);
                bar.refresh();
            }

            fixedContainer.resizeDelta();
        }

        public virtual void refreshBaoKong(int[] cards, int postion)
        {

        }

        /// <summary>
        /// 刷新自己点击的牌
        /// </summary>
        /// <param name="index"></param>
        public virtual void refreshUpHandCard(int index)
        {
            for (int i=0;i<handContainer.count;i++)
            {
                MJBasePlayerHandBar bar=(MJBasePlayerHandBar) handContainer.bars[i];
                bar.cardUp(i==index);
            }
        }

        /// <summary>
        /// 刷新替换完牌后，自己的显示(单个人替换完牌后的显示)
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="postion"></param>
        /// <param name="replacenum"></param>
        public virtual void refreshReplaceOver(int[] cards, int postion, int replacenum)
        {
            int len = cards.Length;
            this.cards = cards;
            handContainer.resize(len);
            MJBasePlayerHandBar bar;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar) handContainer.bars[i];
                bar.index_space = i;
                bar.setCardRePlace(cards[i], postion, len, replacenum);
                bar.refresh();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex((len - 1) - i);
            }
        }

        /// <summary>
        /// 亮牌
        /// </summary>
        public virtual void brightHandCard(int[] cards, MJMOCard mocard, int postion, TingCardsInfo[] ting, int distype,
            bool hasDistypeCard, bool isMoCardMove)
        {
            this.cards = cards;

            int len = cards.Length;

            bool isHu = mocard != null ? mocard.isHu() : false;

            handContainer.resize(len + (mocard != null ? 1 : 0));

            MJBasePlayerHandBar bar;
            int sign_index = 0;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[i];
                bar.index_space = i;

                bar.setCard(isHu, cards[i], postion, null, false, getTingInfo(cards[i], ting), distype, hasDistypeCard,
                    getSignCount(cards), sign_index);
                if (cards[i] != MJCard.CIN && MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                    sign_index++;
                bar.refreshBright();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex((len - 1) - i);
            }

            if (mocard != null)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[len];
                bar.index_space = len;
                bar.setCard(isHu, mocard.card, postion, mocard, false, getTingInfo(mocard.card, ting), distype,
                    hasDistypeCard, getSignCount(cards), 0);
                bar.refresh();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex(0);

                if (!isHu && isMoCardMove)
                {
                    Vector3 init = bar.transform.localPosition;
                    DisplayKit.setLocalY(bar, init.y + bar.getHeight());
                    bar.transform.DOLocalMoveY(init.y, 0.1f);
                }
            }
            resetHandPostion(postion);
        }


        /// <summary>
        /// 刷新手牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="mo"></param>
        /// <param name="postion"></param>
        public virtual  void refreshHandCard(int[] cards, MJMOCard mocard, int postion, TingCardsInfo[] ting, int distype,
            bool hasDistypeCard, bool isMoCardMove)
        {
            int len = cards.Length;

            this.cards = cards;

            bool isHu = mocard != null ? mocard.isHu() : false;

            handContainer.resize(len + (mocard != null ? 1 : 0));

            MJBasePlayerHandBar bar;
            int sign_index = 0;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar) handContainer.bars[i];
                bar.index_space = i;

                bar.setCard(isHu, cards[i], postion, null, false, getTingInfo(cards[i], ting), distype, hasDistypeCard,
                    getSignCount(cards), sign_index);
                if (cards[i] != MJCard.CIN && MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                    sign_index++;
                bar.refresh();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex((len - 1) - i);
            }

            if (mocard != null)
            {
                bar = (MJBasePlayerHandBar) handContainer.bars[len];
                bar.index_space = len;
                bar.setCard(isHu, mocard.card, postion, mocard, false, getTingInfo(mocard.card, ting), distype,
                    hasDistypeCard, getSignCount(cards), 0);
                bar.refresh();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex(0);

                if (!isHu && isMoCardMove)
                {

                    Vector3 init = bar.transform.localPosition;
                    DisplayKit.setLocalY(bar, init.y + bar.getHeight());
                    bar.transform.DOLocalMoveY(init.y, 0.1f);
                }
            }
            
            resetHandPostion(postion);
        }



        /// <summary>
        /// 自己碰牌后刷新手牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="mo"></param>
        /// <param name="postion"></param>
        public virtual void pongRefreshHandCard(int[] cards, MJMOCard mocard, int postion, TingCardsInfo[] ting, int distype,
            bool hasDistypeCard, bool isMoCardMove)
        {
            int len = cards.Length;

            this.cards = cards;

            bool isHu = mocard != null ? mocard.isHu() : false;

            handContainer.resize(len + (mocard != null ? 1 : 0));

            MJBasePlayerHandBar bar;
            int sign_index = 0;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[i];
                bar.index_space = i;
                
                bar.setCard(isHu, cards[i], postion, null, false, getTingInfo(cards[i], ting), distype, hasDistypeCard,
                    getSignCount(cards), sign_index);
                bar.setTotalNum(len);
                if (cards[i] != MJCard.CIN && MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                    sign_index++;
                bar.pongRefreshHand();
            }
            resetHandPostion(postion);
        }




        Action action;
        /// <summary>
        /// 刷新换三张
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="mo"></param>
        /// <param name="postion"></param>
        public virtual void refreshHSZHandCard(int[] cards, MJMOCard mocard, int postion, int distype,
            bool hasDistypeCard,int[] hsz,Action action)
        {
            this.action = action;
            int len = cards.Length;

            this.cards = cards;

            bool isHu = mocard != null ? mocard.isHu() : false;

            handContainer.resize(len + (mocard != null ? 1 : 0));

            MJBasePlayerHandBar bar;
            int sign_index = 0;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[i];
                bar.index_space = i;

                bar.setCard(isHu, cards[i], postion, null, false,null, distype, hasDistypeCard,
                    getSignCount(cards), sign_index);
                if (cards[i] != MJCard.CIN && MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                    sign_index++;
                bar.refresh();
                if (postion == MahJongPanel.LOC_RIGHT)
                    bar.transform.SetSiblingIndex((len - 1) - i);
            }

            int j = 0;
            for (int i=0;i< hsz.Length;i++)
            {
                for (; j < len; j++)
                {
                    if (hsz[i] == cards[j])
                    {
                        bar = (MJBasePlayerHandBar)handContainer.bars[j];
                        DisplayKit.setLocalY(bar, 50);
                        bar.transform.DOLocalMoveY(0, 3).onComplete= hszover;
                        j++;
                        break;
                    }
                }
            }
            resetHandPostion(postion);
        }

        public void hszover()
        {
            if (action != null)
            {
                action();
                action = null;
            }
               
        }



        private int getSignCount(int[] cards)
        {
            int count = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i]!=MJCard.CIN&&MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                    count++;
            }
            return count;
        }


        protected Action moBack;


        public float movetime = 0.03f;//每张牌移动时间(如果有2个，就需要x2)
        public float cardwidth = 75;//每张牌的宽度
        /// <summary>
        /// 摸牌区的牌移动刷新
        /// </summary>
        public virtual  void refreshmoCardMove(int[] cards, MJMOCard mocard, int postion,int moveIndex,Action back)
        {
            int len = cards.Length;
            this.cards = cards;
            bool isHu = mocard != null ? mocard.isHu() : false;

            MJBasePlayerHandBar bar = null;

            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[i];
                bar.index_space = i;
                bar.setCard(isHu, cards[i], postion, null, false, null, MJCard.CNO, false, getSignCount(cards), 0);
                bar.refresh();
            }
            resetHandPostion(postion);
            back();

            //this.moBack = back;
            //int len = cards.Length;

            //this.cards = cards;

            //bool isHu = mocard != null ? mocard.isHu() : false;

            //handContainer.resize(len + (mocard != null ? 1 : 0));

            //MJBasePlayerHandBar bar = null;

            //bool ishide = false;

            //Vector3 end = Vector3.one;

            //for (int i = 0; i < len; i++)
            //{
            //    bar = (MJBasePlayerHandBar)handContainer.bars[i];
            //    bar.index_space = i;
            //    bar.setCard(isHu, cards[i], postion, null, false, null, MJCard.CNO, false, getSignCount(cards),0);
            //    bar.refresh();
            //    if (!ishide && i == moveIndex)
            //    {
            //        bar.setVisible(false);
            //        end = bar.transform.localPosition;
            //    }
            //}
            //if (mocard != null)
            //{
            //    bar = (MJBasePlayerHandBar)handContainer.bars[len];
            //    bar.index_space = len;
            //    bar.setCard(isHu, mocard.card, postion, mocard, false, null, MJCard.CNO, false, getSignCount(cards),0);
            //    bar.refresh();
            //}

            //resetHandPostion(postion);

            //Vector3 start = bar.transform.localPosition;

            //Sequence s = DOTween.Sequence();

            //if (moveIndex != cards.Length - 1)
            //{
            //    s.Append(bar.transform.DOLocalMove(new Vector2(start.x, start.y + bar.getHeight()), 0.1f)
            //        .SetEase(Ease.Linear));
            //    s.Insert(0, bar.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f));
            //    s.Append(bar.transform.DOLocalMove(new Vector2(end.x, start.y + bar.getHeight()), movetime * (start.x - end.x) / cardwidth)//75是每张牌的宽度
            //        .SetEase(Ease.Linear));
            //    s.Append(bar.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.05f).SetEase(Ease.Linear));
            //    s.Append(bar.transform.DOLocalMove(end, 0.1f).SetEase(Ease.Linear));
            //}
            //else
            //{
            //    s.Append(bar.transform.DOLocalMove(end, 0.1f).SetEase(Ease.Linear));
            //}

            //s.OnComplete(() => { moBack(); });
        }

        /// <summary>
        /// 重设手牌的位置
        /// </summary>
        private void resetHandPostion(int index)
        {
            float width = 0;
            float x = 0;
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    width = fixedContainer.getWidth() * fixedContainer.size * (1 + scalex);
                    x = handinitx;
                    showDownCard(width, x);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    width = fixedContainer.getHeight() * fixedContainer.size;
                    x = handinity;
                    DisplayKit.setLocalY(handContainer, x + width);
                    break;
                case MahJongPanel.LOC_TOP:
                    width = fixedContainer.getWidth() * fixedContainer.size * (1 + scalex);
                    if (width>0)
                        width += 8;
                    x = handinitx;
                    DisplayKit.setLocalX(handContainer, x - width);
                    break;
                case MahJongPanel.LOC_LEFT:
                    width = fixedContainer.getHeight() * fixedContainer.size;
                    if (width > 0)
                        width -= 5;
                    x = handinity;
                    DisplayKit.setLocalY(handContainer, x - width);
                    break;
            }
        }

        private void showDownCard(float width,float x)
        {
            int sid = Room.room.getRule().sid;
            if (Room.room.getRule().getIntAtribute("fangshu") == 1)
            {
                width = (cards.Length) * (1 + scalex) * handContainer.getWidth() / 2;

                float x1 = 0 - width - (1136 * (1 + scalex) / 2f + x);

                DisplayKit.setLocalX(handContainer, x1);
            }
            else
            {
                DisplayKit.setLocalX(handContainer, x + width);
            }


        }


        private TingCardsInfo getTingInfo(int card, TingCardsInfo[] ting)
        {
            if (ting == null) return null;
            for (int i = 0; i < ting.Length; i++)
            {
                if (ting[i].card == card)
                    return ting[i];
            }
            return null;
        }

        /// <summary>
        /// 显示选择的牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="mo"></param>
        /// <param name="selectCardsIndex">所选牌的索引</param>
        public virtual  void refreshSelectHandCard(int[] cards, MJMOCard mocard, int[] selectCardsIndex, TingCardsInfo[] ting, int distype,bool hasDistypeCard)
        {
            int len = cards.Length;

            this.cards = cards;

            handContainer.resize(len + (mocard!=null ? 1 : 0));
            MJBasePlayerHandBar bar;
            for (int i = 0; i < len; i++)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[i];
                bar.index_space = i;
                bar.setCard(false,cards[i], MahJongPanel.LOC_DOWN, null, isSameIndex(i, selectCardsIndex), getTingInfo(cards[i], ting), distype, hasDistypeCard,0,0);
                bar.refresh();
            }
            if (mocard!=null)
            {
                bar = (MJBasePlayerHandBar)handContainer.bars[len];
                bar.index_space = len;
                bar.setCard(false,mocard.card, MahJongPanel.LOC_DOWN, mocard, isSameIndex(len, selectCardsIndex), getTingInfo(mocard.card, ting), distype, hasDistypeCard, 0,0);
                bar.refresh();
            }
        }

        private bool isSameIndex(int dest, int[] src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if (dest == src[i]) return true;
            }
            return false;
        }
    }
}
