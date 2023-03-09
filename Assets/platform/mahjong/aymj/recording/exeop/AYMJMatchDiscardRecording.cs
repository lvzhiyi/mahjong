using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 出牌
    /// </summary>
    public class AYMJMatchDiscardRecording : Process
    {
        MJMatchDiscardOperate operate;

        int selfOperate;

        public AYMJMatchDiscardRecording(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDiscardOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }


        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            SoundManager.playMJEffect("card_out");
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
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

            playerCards.delHandCard(operate.card, 1);
            panel.refreshHandCard(operate.index, null, false);

            panel.showPlayerCard(operate.card, operate.index);
            panel.showPlayerLastCard(operate.index);
          

            SoundManager.playMJCard(Room.room.players[operate.index].player.sex,operate.card);
            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates, operate.card);
            operate.playOver();
        }
    }
}
