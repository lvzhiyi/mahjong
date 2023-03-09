

using cambrian.common;
using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 出牌
    /// </summary>
    public class AYMJMatchDiscardProcess:Process
    {
        MJMatchDiscardOperate operate;

        int selfOperate;

        public AYMJMatchDiscardProcess(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDiscardOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }


        AYMJRoomPanel panel;

        public override void execute()
        {
            SoundManager.playMJEffect("card_out");
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            match.getPlayerCardIndex<AYMJPlayerCards>(operate.index).delStatus(AYMJPlayerCards.STATUS_TIAN_DI_HU);

            if (match.kongIndex != operate.index)
            {
                match.resetKongStatus();
            }
                
            match.ResetPlayerCard();
            match.setLastPlayerCard(operate.index,operate.card);

            AYMJPlayerCards playerCards= match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
           
            playerCards.addDisableCard(operate.card);
            panel.refreshDisAbleView(0,true);

            if (operate.index !=match.mindex)
            {
                playerCards.delHandCard(operate.card, 1);
                panel.refreshSingleHandCard(operate.index); //某人出的牌，需要刷新某人出的手牌，不需要显示定缺的牌，也不需要听牌
            }
            else
            {
                panel.gameView.showDisCardRedmine(false);
                if (!AYMJSelectCardProcess.isChuCard)
                {
                    playerCards.delHandCard(operate.card, 1);
                    panel.refreshSingleHandCard(operate.index);
                }
                else
                {
                    AYMJSelectCardProcess.isChuCard = false;
                }

                panel.gameView.showTingDeng(match.getDengTingCards(match.mindex) != null);
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
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, operate.card, false, AYMJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }
            operate.playOver();

            AYMJPlayerCards playerCards= AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>();
           if ((playerCards.handcards.count - 1) % 3 != 0)
           {
                CommandManager.addCommand(new RoomRfreshDataCommand());
           }
        }
    }
}
