using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 出牌
    /// </summary>
    public class MJMatchDiscardRecording : Process
    {
        MJMatchDiscardOperate operate;

        public MJMatchDiscardRecording(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDiscardOperate)baseOperate;
        }


        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            SoundManager.playMJEffect("card_out");
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            if (match.kongIndex != operate.index)
            {
                match.resetKongStatus();
            }
                
            match.ResetPlayerCard();
            match.setLastPlayerCard(operate.index,operate.card);

            MJPlayerCards playerCards= match.getPlayerCardIndex<MJPlayerCards>(operate.index);
           
            playerCards.addDisableCard(operate.card);
            panel.refreshDisAbleView(0,true);

            playerCards.delHandCard(operate.card, 1);
            panel.refreshHandCard(operate.index, null, false);

            panel.showPlayerCard(operate.card,operate.index);
            panel.showPlayerLastCard(operate.index);

            SoundManager.playMJCard(Room.room.players[operate.index].player.sex,operate.card);
            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates,operate.card);
            operate.playOver();
        }
    }
}
