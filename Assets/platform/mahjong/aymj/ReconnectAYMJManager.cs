using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    public class ReconnectAYMJManager : BytesSerializable
    {
        // ----------------------亮牌类型字段----------------------------
        /// <summary>
        /// 亮牌类型：普通出牌
        /// </summary>
        public const int OPTION_NORMAL = 0;

        /// <summary>
        /// 亮牌类型：杠后出牌
        /// </summary>
        public const int OPTION_KONG = 1;

        /// <summary>
        /// 亮牌类型：补杠牌
        /// </summary>
        public const int OPTION_KONG_SUP = 2;

        private static ReconnectAYMJManager sceneManager;

        public static ReconnectAYMJManager manager
        {
            get
            {
                if (sceneManager == null)
                    sceneManager = new ReconnectAYMJManager();
                return sceneManager;
            }
        }

        /// <summary>
        /// 当前亮出的牌 如果没有就是-1 换三张，定缺，
        /// </summary>
        private int card; //

        /// <summary>
        /// 亮牌类型
        /// </summary>
        private int optionCardType;

        /// <summary>
        /// 杠牌者索引
        /// </summary>
        int kongIndex;

        /// <summary>
        /// 谁的回合
        /// </summary>
        private int round;

        /// <summary>
        /// 回合剩余时间
        /// </summary>
        private long roundTime;

        /// <summary>
        /// 自己的操作
        /// </summary>
        private int operate;

        public void bytesReadInfos(ByteBuffer data)
        {
            card = data.readInt();
            optionCardType = data.readInt();
            kongIndex = data.readInt();

            round = data.readInt();
            roundTime = data.readLong();
            operate = data.readInt();
        }

        AYMJRoomPanel panel;

        public override void bytesRead(ByteBuffer data)
        {
            Room.room.cancelReady();
            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.clearBaseOperate();
            panel.refresh();
            panel.showGameView();

            panel.gameView.getMjIndexCenterView()
                .setJuShu(Room.room.roomRule.getNowsPlayNum(), Room.room.roomRule.getMatchCount());

            AYMJMatch match = AYMJMatch.match;
            match.setRound(round);

            panel.setOperate(operate);
            panel.refreshCardNum();

            panel.refreshBaoKongCard(0, true);
            panel.refreshFixedCard(0, true); //先刷新固定牌(因为，在刷新手牌的时候，会去获取固定牌的宽高)
            panel.refreshHandCard(0, null, false, true);
            panel.refreshDisAbleView(0, true);

            panel.gameView.getPiaoStatusView().showpiao(AYMJMatch.match.getPlayerCardsStatus());
            panel.getGameView<AYMJGameView>().getBaoCardView().setBaoPai(AYMJMatch.match.getPlayerCardsStatus());



            if (match.isstage(AYMJMatch.STAGE_REPLACE)) //换牌阶段
            {
                replaceCard();
                if (operate > 0)
                {
                    showOperate(match);
                }
            }
            else if (match.isstage(AYMJMatch.STAGE_DISTYPE)) //定缺阶段
            {
                dingQueCard();
                if (operate > 0)
                {
                    showOperate(match);
                }
            }
            else if (match.isstage(AYMJMatch.STAGE_PIAO))//飘阶段
            {
                piaoStage();
                if (operate > 0)
                {
                    showOperate(match);
                }
            }
            else
            {
                panel.showCountTime(round);

                if (card != MJCard.CNO)
                {
                    if (optionCardType == OPTION_NORMAL)
                    {
                        panel.showPlayerCard(card, round);
                        panel.showPlayerLastCard(round);
                        match.setLastPlayerCard(round, card);
                    }
                    else if (optionCardType == OPTION_KONG) //如果该牌是杠牌后打出来的时候，有什么事情发生
                    {
                        panel.showPlayerCard(card, round);
                        panel.showPlayerLastCard(round);
                        match.setKongSup(round, card, true);
                        match.setKongType(MJMatchKongOperate.KONG_PUB);
                    }
                    else if (optionCardType == OPTION_KONG_SUP) //如果该牌是巴杠的牌，巴杠的这张牌，应该是透明的。等待其他玩家胡(其他玩家胡的时候，番数应该加番)
                    {
                        match.setKongSup(round, card, true);
                        MJPlayerCards playerCards = match.getPlayerCardIndex<MJPlayerCards>(kongIndex);
                        MJFixedCards fixedcard = (MJFixedCards) playerCards.getSameFixedCards(card, 4);
                        fixedcard.type = MJFixedCards.MJONG_SUP_WAIT;
                        panel.refreshFixedCard(0, true);
                        match.setKongType(MJMatchKongOperate.KONG_SUP_WAIT_ROB);
                    }
                }
                

                if (kongIndex >= 0) //杠牌人的索引
                {
                    match.setKong(true);
                    match.kongIndex = kongIndex;
                }


                panel.gameView.showDistypeCardView(match.getPlayerDistypes());

                panel.refreshHuVew(0, false, true);
               

                if (operate > 0)
                {
                    showOperate(match);
                }
                else
                {
                    panel.refreshSingleHandCard(match.mindex);
                }
                panel.gameView.showTingDeng(match.getDengTingCards(match.mindex) != null);
            }
            UnrealRoot.root.showPanel<AYMJRoomPanel>();
        }

        private void showOperate(AYMJMatch match)
        {
            if (MJOperate.isDisCard(operate))
            {
                panel.refreshHandCard(match.mindex, match.getSelfTingCards(match.mindex), false);
                panel.gameView.showDisCardRedmine(true);
            }

            if (round != match.mindex)
                panel.gameView.getOperateView()
                    .showOperate(match.getMJOperateInfos(operate, card, match.isKongSups(), match.mindex));
            else
            {
                AYMJPlayerCards playerCards = match.getSelfPlayerCards<AYMJPlayerCards>();
                panel.gameView.getOperateView()
                    .showOperate(match.getMJOperateInfos(operate, playerCards.mocard,
                        match.isKongSups(), match.mindex));
                TingCardsInfo[] tinginfos = match.getSelfTingCards(match.mindex);
                panel.refreshHandCard(match.mindex, tinginfos, false);
            }
        }

        /// <summary>
        /// 换三张阶段
        /// </summary>
        private void replaceCard()
        {
            panel.refreshHuanSZ(0, true);
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(AYMJPlayerCards.STATUS_REPLACE);
            if (MJOperate.isCanReplace(operate))
            {
                panel.refreshHuanSZUpCard();
            }

            AYMJPlayerCards[] all = AYMJMatch.match.getPlayerCardss<AYMJPlayerCards>();

            int[] replacesCard = new int[AYMJMatch.match.replace];
            for (int i = 0; i < replacesCard.Length; i++)
            {
                replacesCard[i] = MJCard.CIN;
            }

            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].hasStatus(AYMJPlayerCards.STATUS_REPLACE))
                {
                    panel.refreshSelfHuanSuccessHandCard(i, replacesCard);
                }
            }
        }

        private void dingQueCard()
        {
            panel.refreshDingQue(0, true); //先刷新所有人的定缺状态
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(AYMJPlayerCards.STATUS_DISTYPE);
            int distype = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().distype;
            if (distype != MJCard.CNO)
                panel.gameView.showSingleDistypeCard(AYMJMatch.match.mindex, distype);

            if (MJOperate.isDisType(operate))
            {
                panel.gameView.getDQView().showDingQue(AYMJMatch.match.getRecommendType());
            }
        }

        private void piaoStage()
        {
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(AYMJPlayerCards.STATUS_PIAO_SELECT);
            AYMJPlayerCards playerCards = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>();
            panel.refreshSelectPiao(0, true);
            if (playerCards.hasStatus(AYMJPlayerCards.STATUS_PIAO))
                panel.gameView.getPiaoStatusView().showIndexPiao(AYMJMatch.match.mindex);
        }
    }
}
