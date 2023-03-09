

using cambrian.common;
using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 出牌
    /// </summary>
    public class MJMatchDiscardProcess:Process
    {
        MJMatchDiscardOperate operate;

        int selfOperate;

        public MJMatchDiscardProcess(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDiscardOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }


        MahjongRoomPanel panel;

        public override void execute()
        {
            SoundManager.playMJEffect("card_out");
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            match.getPlayerCardIndex<MJPlayerCards>(operate.index).delStatus(MJPlayerCards.STATUS_TIAN_DI_HU);

            if (match.kongIndex != operate.index)
            {
                match.resetKongStatus();
            }
                
            match.ResetPlayerCard();
            match.setLastPlayerCard(operate.index,operate.card);

            MJPlayerCards playerCards= match.getPlayerCardIndex<MJPlayerCards>(operate.index);
           
            playerCards.addDisableCard(operate.card);
            panel.refreshDisAbleView(0,true);

            if (operate.index != MJMatch.match.mindex)
            {
                playerCards.delHandCard(operate.card, 1);
                panel.refreshSingleHandCard(operate.index); //某人出的牌，需要刷新某人出的手牌，不需要显示定缺的牌，也不需要听牌
            }
            else
            {
                panel.gameView.showDisCardRedmine(false);
                if (!MJSelectCardProcess.isChuCard)
                {
                    playerCards.delHandCard(operate.card, 1);
                    panel.refreshSingleHandCard(operate.index);
                }
                else
                {
                    MJSelectCardProcess.isChuCard = false;
                }

                panel.gameView.showTingDeng(match.getDengTingCards(MJMatch.match.mindex) != null);
                panel.gameView.hideLieView();
            }

            panel.showPlayerCard(operate.card,operate.index);
            panel.showPlayerLastCard(operate.index);

            SoundManager.playMJCard(Room.room.players[operate.index].player.sex,operate.card);
            showOperate();
        }

        private void showOperate()
        {
            if (selfOperate > 0)
            {
                panel.gameView.getOperateView().showOperate(MJMatch.match.getMJOperateInfos(selfOperate, operate.card, false, MJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }
            operate.playOver();

            MJPlayerCards playerCards= MJMatch.match.getSelfPlayerCards<MJPlayerCards>();
           if ((playerCards.handcards.count - 1) % 3 != 0)
           {
                CommandManager.addCommand(new RoomRfreshDataCommand());
           }
        }
    }
}
