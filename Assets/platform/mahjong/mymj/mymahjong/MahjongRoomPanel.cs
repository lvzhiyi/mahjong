using cambrian.common;
using platform.spotred;
using System;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将房间面板
    /// </summary>
    public class MahjongRoomPanel: MahJongPanel
    {
        /// <summary>
        /// 躺牌的操作
        /// </summary>
        public int tangOperate { set; get; }

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
        }

        /// <summary>
        /// 换牌阶段刷新改变牌面和牌底
        /// </summary>
        private void refreshHp()
        {
            if (MJMatch.match.isstage(MJMatch.STAGE_REPLACE))
            {
                if (MJMatch.match.getSelfPlayerCards<MJPlayerCards>().hasStatus(MJPlayerCards.STATUS_REPLACE))
                {
                    int[] c = new int[MJMatch.match.replace];
                    for (int i = 0; i < MJMatch.match.replace; i++)
                    {
                        c[i] = MJCard.CIN;
                    }
                    refreshSelfHuanSuccessHandCard(MJMatch.match.mindex, c);
                }
            }
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            refreshDeskTop();
            tangOperate = -1;
        }


        public override void showIPView()
        {
            
        }

        public override void setOperate(int operate)
        {
            base.setOperate(operate);
            if (MJMatch.match != null)
                MJMatch.match.operate = operate;
            tangOperate = -1;
        }

        #region 比赛视图
        /// <summary>
        /// 显示游戏中视图
        /// </summary>
        public override void showGameView()
        {
            base.showGameView();
            headView.showMatchPlayers(MJMatch.match.banker);
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
            MJPlayerCards playerCard;
            for (int i = 0; i < len; i++)
            { 
                playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(i);
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
            this.gameView.getMjIndexCenterView().setCardNum(MJMatch.match.paidui);
            gameView.hideOperateView();
        }

        public override void refreshDisAbleView(int index,bool isAll=false)
        {
            if (!isAll)
            {
                ArrayList<int> list=MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index).disableCards;
                gameView.getDisAbleView().refreshDisableCards(getPlayerIndex(index), list.toArray());
            }
            else
            {
                MJPlayerCards[] playercards = MJMatch.match.getPlayerCardss<MJPlayerCards>();
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
            MJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                gameView.getHuanSZView().refreshStatus(getPlayerIndex(index),playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                    gameView.getHuanSZView().refreshStatus(getPlayerIndex(index), playerCard.status);
                    index = getIndex(index);
                }
            }
        }

        /// <summary>
        /// 刷新玩家的手牌(排序好)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshHSZHandCard(int index, int[] hsz, Action action)
        {
            int len = Room.room.getPlayerCount();
            int[] distype = MJMatch.match.getPlayerDistypes();
            int[] cards;
            MJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo = MJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");

            for (int i = 0; i < len; i++)
            {
                playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                cards = playerCards.handcards.toArray();
                hasDisType = playerCards.hasDingCard();
                gameView.getHandView().refreshHSZHandCard(getPlayerIndex(index), cards, playerCards.getMJMoCard(isShowziMo), distype[index], hasDisType, hsz, action);
                index = getIndex(index);
            }
        }

        /// <summary>
        /// 刷新定缺状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public override void refreshDingQue(int index, bool isAll = false)
        {
            MJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                gameView.getDQView().refreshStatus(getPlayerIndex(index), playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
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
            MJPlayerCards playerCards;
            if (!isAll)
            {
                playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                gameView.getHuView().showHu(getPlayerIndex(index), playerCards.huIndex,playerCards.hutype, isplaying);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
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
                cards = MJMatch.match.getPlayerCardss<MJPlayerCards>()[index].fixCards.toArray();
                gameView.getHandView().refreshFixedCard(getPlayerIndex(index), cards);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    cards = MJMatch.match.getPlayerCardss<MJPlayerCards>()[index].fixCards.toArray();
                    gameView.getHandView().refreshFixedCard(getPlayerIndex(index), cards);
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
            int[] distype = MJMatch.match.getPlayerDistypes();
            int[] cards;
            MJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo=MJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");

            if (!isAll)
            {
                playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                cards = playerCards.handcards.toArray();
                hasDisType = playerCards.hasDingCard();
                gameView.getHandView().refreshHandCard(getPlayerIndex(index),cards,playerCards.getMJMoCard(isShowziMo),ting,distype[index], hasDisType, isMoveMoCard);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
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
            int[] distype = MJMatch.match.getPlayerDistypes();
            int[] cards;
            MJPlayerCards playerCards = null;
            bool hasDisType = false;

            bool isShowziMo = MJMatch.match.getRoomRule().rule.getRuleAtribute("zimoShowCard");


            playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
            cards = playerCards.handcards.toArray();
            hasDisType = playerCards.hasDingCard();
            gameView.getHandView().pongRefreshHandCard(getPlayerIndex(index), cards, playerCards.getMJMoCard(isShowziMo), ting, distype[index], hasDisType);
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
                MJPlayerCards playerCards = null;
                playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
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
            MJPlayerCards playerCards = null;
            playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
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
            MJPlayerCards playerCards = MJMatch.match.getSelfPlayerCards<MJPlayerCards>();
            int[] cards = playerCards.handcards.toArray();
            gameView.getHandView().refreshSelfSeclectCard(cards, playerCards.getMJMoCard(false),MJMatch.match.getHuanSZIndex(),null,MJCard.CNO,false);
        }

        /// <summary>
        /// 刷新躺牌选择的牌
        /// </summary>
        public void refreshTangSelectUpCard()
        {
            MJPlayerCards playerCards = MJMatch.match.getSelfPlayerCards<MJPlayerCards>();
            int[] cards = playerCards.handcards.toArray();
            gameView.getHandView().refreshSelfSeclectCard(cards, playerCards.getMJMoCard(false), MJMatch.match.getTangCardsIndexs().toArray(), null, MJCard.CNO, false);
        }


        /// <summary>
        /// 刷新玩家已经显示的状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public void refreshTangImage(int index, bool isAll= false)
        {
            int len = Room.room.getPlayerCount();
            MJPlayerCards playerCards;

            bool isTang = false;
            if (!isAll)
            {
                playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                isTang = playerCards.hasStatus(MJPlayerCards.STATUS_TANG);
                gameView.ShowTangImgView(getPlayerIndex(index), isTang);
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                    isTang = playerCards.hasStatus(MJPlayerCards.STATUS_TANG);
                    gameView.ShowTangImgView(getPlayerIndex(index), isTang);
                    index = getIndex(index);
                }
            }
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
            MJPlayerCards playerCard = null;
            if (!isAll)
            {
                playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                gameView.getPiaoView().refreshStatus(getPlayerIndex(index), playerCard.status);
            }
            else
            {
                int len = Room.room.getPlayerCount();
                for (int i = 0; i < len; i++)
                {
                    playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(index);
                    gameView.getPiaoView().refreshStatus(getPlayerIndex(index), playerCard.status);
                    index = getIndex(index);
                }
            }
        }


        #endregion

    }
}
