using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 杠牌
    /// </summary>
    public class AYMJMatchKongRecording : Process
    {
        MJMatchKongOperate operate;

        int selfOperate;

        public AYMJMatchKongRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchKongOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.isTianhu = false;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            match.setKong(true);

            match.setKongType(operate.kongType);

            match.kongIndex = operate.index;

            SoundManager.playMJGang(Room.room.players[operate.index].player.sex);

            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);

            int deletecount = 3;
            int type = MJFixedCards.MJKONG_PUB;

            if (operate.kongType == MJMatchKongOperate.KONG_SUP_WAIT_ROB)//巴杠，等待别人抢杠
            {
                //这里只播放声音 
                //这里要标记是否是等待别人巴杠
                deletecount = 1;
                type = MJFixedCards.MJONG_SUP_WAIT;
                
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_SUP_EXIT_ROB)//巴杠继续
            {
                deletecount = 0;
                type = MJFixedCards.MJKONG_SUP;
                panel.playAnimation(operate.index, 0);
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_PUB)//明杠
            {
                playerCards.removeLastDisbaleCard();

                SoundManager.playMJEffect("guafeng");

                panel.refreshDisAbleView(operate.from);
                panel.playAnimation(operate.index, 0);
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_PRI)//暗杠
            {
                deletecount = 4;
                type = MJFixedCards.MJKONG_PRI;
                SoundManager.playMJEffect("xiayu");
                panel.playAnimation(operate.index, 2);
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_SUP)//巴杠成功
            {
                deletecount = 1;
                type = MJFixedCards.MJKONG_SUP;
                SoundManager.playMJEffect("guafeng");
                panel.playAnimation(operate.index, 0);
            }

            playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            if (deletecount > 0)
            {
                int[] baoKong=playerCards.baoKangCard;
                if (baoKong.Length > 0)
                {
                    playerCards.removeBaoKong(operate.card);
                    panel.refreshBaoKongCard(0, true);

                    if (operate.kongType == MJMatchKongOperate.KONG_PRI)
                    {
                        playerCards.delHandCard(operate.card, 1);
                    }
                }
                else
                    playerCards.delHandCard(operate.card, deletecount);
            }
               

            MJFixedCards fixedCards=null;
            if (operate.kongType == MJMatchKongOperate.KONG_PUB || operate.kongType == MJMatchKongOperate.KONG_PRI)
            {
                fixedCards = new MJFixedCards(type, new int[] { operate.card, operate.card, operate.card, operate.card });
                fixedCards.source = operate.from;
                playerCards.addFixedCard(fixedCards);
            }

            else if (operate.kongType == MJMatchKongOperate.KONG_SUP)//巴杠成功
            {
                fixedCards = (MJFixedCards)playerCards.getSameFixedCards(operate.card, 3);
                fixedCards.type = type;
                fixedCards.addSameCards(operate.card);
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_SUP_WAIT_ROB)//巴杠等待
            {
                fixedCards = (MJFixedCards)playerCards.getSameFixedCards(operate.card, 3);
                fixedCards.type = MJFixedCards.MJONG_SUP_WAIT;
                fixedCards.addSameCards(operate.card);
            }
            else if (operate.kongType == MJMatchKongOperate.KONG_SUP_EXIT_ROB)//巴杠继续
            {
                fixedCards = (MJFixedCards)playerCards.getSameFixedCards(operate.card, 4);
                fixedCards.type = type;
                fixedCards.addSameCards(operate.card);
            }

            //刷新固定区的牌
            panel.refreshFixedCard(operate.index);

            if (match.mindex == operate.index)
            {
                panel.refreshHandCard(operate.index, match.getSelfTingCards(AYMJMatch.match.mindex),false);
            }
            else
            {
                panel.refreshHandCard(operate.index, null,false);
            }

            panel.hideLastSendCard();

            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates, operate.card);
            operate.playOver();
        }
    }
}
