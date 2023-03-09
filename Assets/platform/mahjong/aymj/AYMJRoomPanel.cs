using cambrian.common;
using platform.spotred;
using System;

namespace platform.mahjong
{
    /// <summary>
    /// 安岳麻将房间面板
    /// </summary>
    public class AYMJRoomPanel : MahJongPanel
    {
        protected override void xinit()
        {
            base.xinit();
        }


        /// <summary>
        /// 刷新桌布
        /// </summary>
        public override void refreshDeskTop()
        {
            base.refreshDeskTop();
            int index = MJSettingManager.getDeskTop();
            if(gameView.getVisible())
                gameView.getMjIndexCenterView().showpaizuo(index);
        }

        public void refreshCardImg()
        {
            if (!gameView.getVisible()) return;
            refreshHandCard(0, null, false, true);
            refreshHp();
            refreshDisAbleView(0, true);
            refreshFixedCard(0, true);
            refreshBaoKongCard(0,true);
        }

        /// <summary>
        /// 换牌阶段刷新改变牌面和牌底
        /// </summary>
        private void refreshHp()
        {
            if (AYMJMatch.match.isstage(AYMJMatch.STAGE_REPLACE))
            {
                if (AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().hasStatus(AYMJPlayerCards.STATUS_REPLACE))
                {
                    int[] c = new int[AYMJMatch.match.replace];
                    for (int i = 0; i < AYMJMatch.match.replace; i++)
                    {
                        c[i] = MJCard.CIN;
                    }
                    refreshSelfHuanSuccessHandCard(AYMJMatch.match.mindex, c);
                }
            }
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            refreshDeskTop();
        }


        public override void showIPView()
        {
            //ipview.refresh();
        }

        public override void setOperate(int operate)
        {
            base.setOperate(operate);
            if (AYMJMatch.match != null)
                AYMJMatch.match.operate = operate;
        }

        #region 比赛视图
        /// <summary>
        /// 显示游戏中视图
        /// </summary>
        public override void showGameView()
        {
            base.showGameView();
            headView.showMatchPlayers(AYMJMatch.match.banker);
            gameView.refresh();
            gameView.setVisible(true);
        }

        public override void showHuanOrDing(int index)
        {
            gameView.getMjIndexCenterView().showHuanOrDing(getPlayerIndex(index));
        }

        /// <summary>
        /// 刷新每个玩家换牌的方向显示
        /// </summary>
        public override void refreshHSZOrDQDerection(int status)
        {
            int len = Room.room.getPlayerCount();
            AYMJPlayerCards playerCard;
            for (int i = 0; i < len; i++)
            { 
                playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(i);
                if(StatusKit.hasStatus(playerCard.status,status))
                {
                    showHuanOrDing(i);
                }
            }
        }

        /// <summary>
        /// 刷新牌的数量
        /// </summary>
        public override void refreshCardNum()
        {
            this.gameView.getMjIndexCenterView().setCardNum(AYMJMatch.match.paidui);
            gameView.hideOperateView();
        }

        public override void refreshDisAbleView(int index,bool isAll=false)
        {
            if (!isAll)
            {
                ArrayList<int> list=AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index).disableCards;
                gameView.getDisAbleView().refreshDisableCards(getPlayerIndex(index), list.toArray());
            }
            else
            {
                AYMJPlayerCards[] playercards = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>();
                for (int i = 0; i < playercards.Length; i++)
                {
                    gameView.getDisAbleView().refreshDisableCards(getPlayerIndex(index), playercards[i].disableCards.toArray());
                    index = getIndex(index);
                }
            }
        }

      

        /// <summary>
        /// 刷新换三张状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public void refreshHuanSZ(int index, bool isAll = false)
        {
            AYMJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                gameView.getHuanSZView().refreshStatus(getPlayerIndex(index),playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                    gameView.getHuanSZView().refreshStatus(getPlayerIndex(index), playerCard.status);
                    index = getIndex(index);
                }
            }
        }

        /// <summary>
        /// 刷新定缺状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshDingQue(int index, bool isAll = false)
        {
            AYMJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                gameView.getDQView().refreshStatus(getPlayerIndex(index), playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                    gameView.getDQView().refreshStatus(getPlayerIndex(index), playerCard.status);
                    index = getIndex(index);
                }
            }
        }

        public override void refreshSingleDingQue(int index)
        {
            gameView.getDQView().refreshSingleStatus(getPlayerIndex(index));
        }
        /// <summary>
        /// 刷新玩家胡
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public void refreshHuVew(int index,bool isplaying, bool isAll)
        {
            int len = Room.room.getPlayerCount();
            AYMJPlayerCards playerCards;
            if (!isAll)
            {
                playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                gameView.getHuView().showHu(getPlayerIndex(index), playerCards.huIndex,playerCards.hutype, isplaying);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                    gameView.getHuView().showHu(getPlayerIndex(index), playerCards.huIndex, playerCards.hutype, isplaying);
                    index = getIndex(index);
                }
            }
        }

        /// <summary>
        /// 刷新玩家的固定牌区
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshFixedCard(int index,bool isAll=false)
        {
            int len = Room.room.getPlayerCount();
            FixedCards[] cards;
            if (!isAll)
            {
                cards = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>()[index].fixCards.toArray();
                gameView.getHandView().refreshFixedCard(getPlayerIndex(index), cards);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    cards = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>()[index].fixCards.toArray();
                    gameView.getHandView().refreshFixedCard(getPlayerIndex(index), cards);
                    index = getIndex(index);
                }
            }
        }

        /// <summary>
        /// 刷新玩家报杠的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public void refreshBaoKongCard(int index, bool isAll = false)
        {
            int len = Room.room.getPlayerCount();
            int[] cards;
            if (!isAll)
            {
                cards = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>()[index].baoKangCard;
                gameView.getHandView().refreshBaoKong( cards, getPlayerIndex(index));
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    cards = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>()[index].baoKangCard;
                    gameView.getHandView().refreshBaoKong(cards, getPlayerIndex(index));
                    index = getIndex(index);
                }
            }
        }

        /// <summary>
        /// 刷新玩家的手牌(排序好)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshHandCard(int index, TingCardsInfo[] ting,bool isMoveMoCard,bool isAll=false)
        {
            int len = Room.room.getPlayerCount();
            int[] distype = AYMJMatch.match.getPlayerDistypes();
            int[] cards;
            AYMJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo=AYMJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");
            if (!isAll)
            {
                playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                cards = playerCards.handcards.toArray();
                hasDisType = playerCards.hasDingCard();
                gameView.getHandView().refreshHandCard(getPlayerIndex(index),cards,playerCards.getMJMoCard(isShowziMo),ting,distype[index], hasDisType, isMoveMoCard);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                    cards = playerCards.handcards.toArray();
                    hasDisType = playerCards.hasDingCard();
                    gameView.getHandView().refreshHandCard(getPlayerIndex(index), cards,playerCards.getMJMoCard(isShowziMo), ting,distype[index], hasDisType, isMoveMoCard);
                    index = getIndex(index);
                }
            }
        }


        /// <summary>
        /// 自己碰牌后，刷新玩家的手牌(排序好)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public void pongrefreshHandCard(int index, TingCardsInfo[] ting)
        {
            int len = Room.room.getPlayerCount();
            int[] distype = AYMJMatch.match.getPlayerDistypes();
            int[] cards;
            AYMJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo = AYMJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");

            playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
            cards = playerCards.handcards.toArray();
            hasDisType = playerCards.hasDingCard();
            gameView.getHandView().pongRefreshHandCard(getPlayerIndex(index), cards, playerCards.getMJMoCard(isShowziMo), ting, distype[index], hasDisType);
        }


        /// <summary>
        /// 刷新玩家的手牌(排序好)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshHSZHandCard(int index,int[] hsz, Action action)
        {
            int len = Room.room.getPlayerCount();
            int[] distype = AYMJMatch.match.getPlayerDistypes();
            int[] cards;
            AYMJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo = AYMJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");

            for (int i = 0; i < len; i++)
            {
                playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                cards = playerCards.handcards.toArray();
                hasDisType = playerCards.hasDingCard();
                gameView.getHandView().refreshHSZHandCard(getPlayerIndex(index), cards, playerCards.getMJMoCard(isShowziMo), distype[index], hasDisType,hsz,action);
                index = getIndex(index);
            }
        }

        /// <summary>
        /// 刷新单个人的手牌(只是刷新，不会有定缺的牌，置灰显示这些)
        /// </summary>
        /// <param name="index"></param>
        public override void refreshSingleHandCard(int index,int[] cards=null)
        {
            MJMOCard moCard=null;
            if (cards == null)
            {
                AYMJPlayerCards playerCards = null;
                playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                cards = playerCards.handcards.toArray();
                moCard = playerCards.getMJMoCard(false);
            }
            gameView.getHandView().refreshHandCard(getPlayerIndex(index), cards, moCard, null, MJCard.CNO, false,false);
        }

        /// <summary>
        /// 刷新已经换完牌后，手牌的显示(自己换的牌要显示背面)
        /// </summary>
        public void refreshSelfHuanSuccessHandCard(int index,int[] repalceCards)
        {
            AYMJPlayerCards playerCards = null;
            playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
            int[] cards = playerCards.handcards.toArray();

            ArrayList<int> handcard = new ArrayList<int>();
            handcard.add(cards);

            handcard.add(repalceCards);

            gameView.getHandView().refreshRePlaceOverHandCard(getPlayerIndex(index), handcard.toArray(), repalceCards.Length);
        }


        /// <summary>
        /// 刷新换三张默认牌
        /// </summary>
        public void refreshHuanSZUpCard()
        {
            AYMJPlayerCards playerCards = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>();
            int[] cards = playerCards.handcards.toArray();
            gameView.getHandView().refreshSelfSeclectCard(cards, playerCards.getMJMoCard(false),AYMJMatch.match.getHuanSZIndex(),null,MJCard.CNO,false);
        }

        /// <summary>
        /// 单个人选择飘的状态
        /// </summary>
        /// <param name="index"></param>
        public void refreshSinglePiao(int index)
        {
            gameView.getPiaoView().refreshSingleStatus(getPlayerIndex(index));
        }

        /// <summary>
        /// 刷新选择飘的状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll"></param>
        public void refreshSelectPiao(int index,bool isAll=false)
        {
            AYMJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                gameView.getPiaoView().refreshStatus(getPlayerIndex(index), playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(index);
                    gameView.getPiaoView().refreshStatus(getPlayerIndex(index), playerCard.status);
                    index = getIndex(index);
                }
            }
        }

        #endregion

    }
}
