using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 执行碰牌
    /// </summary>
    public class AYMJMatchPongRecording : Process
    {
        MJMatchPongOperate operate;

        int selfOperate;

        public AYMJMatchPongRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchPongOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.isTianhu = false;

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            match.ResetPlayerCard();

            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
            playerCards.removeLastDisbaleCard();
            panel.refreshDisAbleView(operate.from);
            panel.hideLastSendCard();


            playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.delHandCard(operate.card, 2);//

            MJFixedCards fixedcard = new MJFixedCards(MJFixedCards.MJPONG, new int[] { operate.card, operate.card, operate.card });
            fixedcard.source = operate.from;
            playerCards.addFixedCard(fixedcard);

            //刷新固定区的牌
            panel.refreshFixedCard(operate.index);

           
            panel.refreshHandCard(operate.index, null,false);

            SoundManager.playMJPong(Room.room.players[operate.index].player.sex);
            panel.playAnimation(operate.index,1);
            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates, operate.card);
            operate.playOver();
        }
    }
}
