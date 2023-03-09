using System;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 所有玩家操作牌的区域
    /// </summary>
    public class AllHandView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 自己的牌区
        /// </summary>
        [HideInInspector] public SelfView selfView;

        /// <summary>
        /// 右边的牌区
        /// </summary>
        [HideInInspector] public RightView rightView;
        /// <summary>
        /// 顶部的牌区
        /// </summary>
        [HideInInspector] public TopView topView;
        /// <summary>
        /// 左边的牌区
        /// </summary>
        [HideInInspector] public LeftView leftView;

        protected override void xinit()
        {
            base.xinit();
            this.selfView = this.transform.Find("loc0").GetComponent<SelfView>();
            this.selfView.init();

            this.rightView = this.transform.Find("loc1").GetComponent<RightView>();
            this.rightView.init();

            this.leftView = this.transform.Find("loc3").GetComponent<LeftView>();
            this.leftView.init();

            this.topView = this.transform.Find("loc2").GetComponent<TopView>();
            this.topView.init();
        }

        protected override void xrefresh()
        {
            this.selfView.refresh();
            

            switch (Room.room.getPlayerCount())
            {
                case 2:
                    rightView.setVisible(false);
                    leftView.setVisible(false);
                    topView.refresh();
                    topView.setVisible(true);
                    break;
                case 3:
                    leftView.refresh();
                    leftView.setVisible(true);
                    rightView.refresh();
                    rightView.setVisible(true);
                    topView.setVisible(false);
                    break;
                default:
                    topView.refresh();
                    topView.setVisible(true);
                    rightView.refresh();
                    rightView.setVisible(true);
                    leftView.refresh();
                    leftView.setVisible(true);
                    break;
            }
        }


        /// <summary>
        /// 显示报牌图标
        /// </summary>
        /// <param name="index"></param>
        public void refreshBaoPai(int index)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.isShowBaoPai(true);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.isShowBaoPai(true);
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.isShowBaoPai(true);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.isShowBaoPai(true);
                    break;
            }
        }

        public void hideBaoPai()
        {
            this.selfView.isShowBaoPai(false);
            this.rightView.isShowBaoPai(false);
            this.topView.isShowBaoPai(false);
            this.leftView.isShowBaoPai(false);
        }

        /// <summary>
        /// 刷新固定牌区
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cards"></param>
        public void refreshFixed(int index, FixedCards[] cards)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.fixedView.setCards(cards);
                    this.selfView.fixedView.refresh();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.fixedView.setCards(cards);
                    this.rightView.fixedView.refresh();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.fixedView.setCards(cards);
                    this.topView.fixedView.refresh();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.fixedView.setCards(cards);
                    this.leftView.fixedView.refresh();
                    break;
            }
        }

        /// <summary>
        /// 刷新玩家的出牌区
        /// </summary>
        /// <param name="index"></param>
        public void refreshDisCard(int index,int[] cards)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.disableView.setCards(cards);
                    this.selfView.disableView.refresh();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.disableView.setCards(cards);
                    this.rightView.disableView.refresh();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.disableView.setCards(cards);
                    this.topView.disableView.refresh();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.disableView.setCards(cards);
                    this.leftView.disableView.refresh();
                    break;
            }
        }

        public void refreshDisable(int index)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.disableView.refresh();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.disableView.refresh();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.disableView.refresh();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.disableView.refresh();
                    break;
            }
        }

        private BaseHandView handView;

        private Action callback;
        /// <summary>
        /// 刷新玩家需要移动的固定牌
        /// </summary>
        /// <param name="index"></param>
        public void refreshMoveFixed(int index, FixedCards[] cards, Action obj)
        {
            this.callback = obj;
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.handView = this.selfView;
                    this.selfView.fixedView.setOperateCards(cards, action);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.handView = this.rightView;
                    this.rightView.setSlibingIndex(5);
                    this.rightView.fixedView.setOperateCards(cards, action);
                   
                    break;
                case RoomPanel.LOC_TOP:
                    this.handView = this.topView;
                    this.topView.setSlibingIndex(5);
                    this.topView.fixedView.setOperateCards(cards, action);
                   
                    break;
                case RoomPanel.LOC_LEFT:
                    this.handView = this.leftView;
                    this.leftView.setSlibingIndex(5);
                    this.leftView.fixedView.setOperateCards(cards, action);
                    break;
            }
        }

        public void action()
        {
            handView.resetSlibingIndex();
            this.callback();
        }


        public void refreshClock(int index)
        {
            this.selfView.isShow(false,false);
            this.topView.isShow(false,false);
            this.rightView.isShow(false,false);
            this.leftView.isShow(false,false);

            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.isShow(true,true);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.isShow(true,false);
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.isShow(true,false);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.isShow(true,false);
                    break;
            }
        }

        /// <summary>
        /// 显示打,吃,招碰,招吃,吃吐...
        /// </summary>
        public void showCard(int index, int card, int type)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.sendCardView(card, type);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.sendCardView(card, type);
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.sendCardView(card, type);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.sendCardView(card, type);
                    break;
            }
        }


        /// <summary>
        /// 显示某人打的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void showPlayCard(int index, int card, bool isAnamition)
        {
            hideAllPlayCard();

            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.setPlayCard(card, isAnamition);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.setPlayCard(card, isAnamition);
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.setPlayCard(card, isAnamition);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.setPlayCard(card, isAnamition);
                    break;
            }
        }


        /// <summary>
        /// 设置偷的牌
        /// </summary>
        public void setTouCard(int pos,int toucard,int index, Action<int> tou)
        {
            switch (pos)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.setTouCard(toucard, index, tou);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.setTouCard(toucard, index, tou);
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.setTouCard(toucard, index, tou);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.setTouCard(toucard, index, tou);
                    break;
            }
        }


        /// <summary>
        /// 隐藏偷的牌
        /// </summary>
        public void hideTouCard()
        {
            this.selfView.hideTouCard();
            this.rightView.hideTouCard();
            this.topView.hideTouCard();
            this.leftView.hideTouCard();
        }

        /// <summary>
        /// 显示胡
        /// </summary>
        /// <param name="index"></param>
        public void showHu(int index)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.showHu();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.showHu();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.showHu();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.showHu();
                    break;
            }
        }
        /// <summary>
        /// 隐藏胡牌
        /// </summary>
        public void hideHu()
        {
            this.selfView.hideHu();
            this.rightView.hideHu();
            this.topView.hideHu();
            this.leftView.hideHu();
        }


        /// <summary>
        /// 隐藏所有人打的牌
        /// </summary>
        public void hideAllPlayCard()
        {
            this.selfView.hidePlayCard();
            this.rightView.hidePlayCard();
            this.topView.hidePlayCard();
            this.leftView.hidePlayCard();
        }

        /// <summary>
        /// 隐藏某人打的牌
        /// </summary>
        /// <param name="index"></param>
        public void hidePlayCard(int index)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.hidePlayCard();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.hidePlayCard();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.hidePlayCard();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.hidePlayCard();
                    break;
            }
        }


        /// <summary>
        /// 隐藏玩家的出牌区最后一张牌
        /// </summary>
        /// <param name="index"></param>
        public void hideDisLastCard(int index,int[] cards)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    this.selfView.disableView.setCards(cards);
                    this.selfView.disableView.hideLastCards();
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.rightView.disableView.setCards(cards);
                    this.rightView.disableView.hideLastCards();
                    break;
                case RoomPanel.LOC_TOP:
                    this.topView.disableView.setCards(cards);
                    this.topView.disableView.hideLastCards();
                    break;
                case RoomPanel.LOC_LEFT:
                    this.leftView.disableView.setCards(cards);
                    this.leftView.disableView.hideLastCards();
                    break;
            }
        }
    }
}
