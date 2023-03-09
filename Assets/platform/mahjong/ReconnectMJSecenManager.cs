using cambrian.common;

namespace platform.mahjong
{
    public class ReconnectMJSecenManager : BytesSerializable
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

        private static ReconnectMJSecenManager sceneManager;

        public static ReconnectMJSecenManager manager
        {
            get
            {
                if (sceneManager == null)
                    sceneManager = new ReconnectMJSecenManager();
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

        MahjongRoomPanel panel;

        public override void bytesRead(ByteBuffer data)
        {
            Room.room.cancelReady();
            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.clearBaseOperate();
            panel.refresh();
            panel.showGameView();

            panel.gameView.getMjIndexCenterView()
                .setJuShu(Room.room.roomRule.getNowsPlayNum(), Room.room.roomRule.getMatchCount());

            MJMatch match = MJMatch.match;
            match.setRound(round);

            panel.setOperate(operate);
            panel.refreshCardNum();


            panel.refreshFixedCard(0, true); //先刷新固定牌(因为，在刷新手牌的时候，会去获取固定牌的宽高)
            panel.refreshHandCard(0, null, false, true);
            panel.refreshDisAbleView(0, true);

            panel.gameView.getPiaoStatusView().showpiao(MJMatch.match.getPlayerCardsStatus());

            if (match.isstage(MJMatch.STAGE_REPLACE)) //换牌阶段
            {
                replaceCard();
            }
            else if (match.isstage(MJMatch.STAGE_DISTYPE)) //定缺阶段
            {
                dingQueCard();
            }
            else if (match.isstage(MJMatch.STAGE_PIAO))//飘阶段
            {
                piaoStage();
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

                panel.refreshTangImage(0,true);

                if (operate > 0)
                {
                    if (MJOperate.isDisCard(operate))
                    {
                        panel.refreshHandCard(match.mindex, match.getSelfTingCards(match.mindex), false);
                        panel.gameView.showDisCardRedmine(true);
                    }

                    if (round != match.mindex)
                        panel.gameView.getOperateView()
                            .showOperate(match.getMJOperateInfos(operate, card, MJMatch.match.isKongSups(),match.mindex));
                    else
                    {
                        MJPlayerCards playerCards = match.getSelfPlayerCards<MJPlayerCards>();
                        panel.gameView.getOperateView()
                            .showOperate(match.getMJOperateInfos(operate, playerCards.mocard,
                                MJMatch.match.isKongSups(),match.mindex));
                        TingCardsInfo[] tinginfos = MJMatch.match.getSelfTingCards(MJMatch.match.mindex);
                        panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);
                    }
                }
                else
                {
                    panel.refreshSingleHandCard(match.mindex);
                }
                panel.gameView.showTingDeng(match.getDengTingCards(MJMatch.match.mindex) != null);
            }
            UnrealRoot.root.showPanel<MahjongRoomPanel>();
        }

        /// <summary>
        /// 换三张阶段
        /// </summary>
        private void replaceCard()
        {
            panel.refreshHuanSZ(0, true);
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(MJPlayerCards.STATUS_REPLACE);
            if (MJOperate.isCanReplace(operate))
            {
                panel.refreshHuanSZUpCard();
            }

            MJPlayerCards[] all = MJMatch.match.getPlayerCardss<MJPlayerCards>();

            int[] replacesCard = new int[MJMatch.match.replace];
            for (int i = 0; i < replacesCard.Length; i++)
            {
                replacesCard[i] = MJCard.CIN;
            }

            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].hasStatus(MJPlayerCards.STATUS_REPLACE))
                {
                    panel.refreshSelfHuanSuccessHandCard(i, replacesCard);
                }
            }
        }

        private void dingQueCard()
        {
            panel.refreshDingQue(0, true); //先刷新所有人的定缺状态
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(MJPlayerCards.STATUS_DISTYPE);
            int distype = MJMatch.match.getSelfPlayerCards<MJPlayerCards>().distype;
            if (distype != MJCard.CNO)
                panel.gameView.showSingleDistypeCard(MJMatch.match.mindex, distype);

            if (MJOperate.isDisType(operate))
            {
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
        }

        private void piaoStage()
        {
            panel.gameView.getMjIndexCenterView().showAllDirection();
            panel.refreshHSZOrDQDerection(MJPlayerCards.STATUS_PIAO_SELECT);
            MJPlayerCards playerCards = MJMatch.match.getSelfPlayerCards<MJPlayerCards>();
            panel.refreshSelectPiao(0, true);
            if (playerCards.hasStatus(MJPlayerCards.STATUS_PIAO))
                panel.gameView.getPiaoStatusView().showIndexPiao(MJMatch.match.mindex);
        }
    }
}
