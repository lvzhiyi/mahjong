using cambrian.game;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 执行碰牌
    /// </summary>
    public class AYMJMatchPongProcess : Process
    {
        MJMatchPongOperate operate;

        int selfOperate;

        public AYMJMatchPongProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchPongOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.isTianhu = false;

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
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

            if (match.mindex == operate.index)
            {
                panel.pongrefreshHandCard(operate.index, match.getSelfTingCards(AYMJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }
            else
            {
                panel.refreshHandCard(operate.index, null,false);
            }

            SoundManager.playMJPong(Room.room.players[operate.index].player.sex);
            panel.playAnimation(operate.index,1);
            showOperate();
        }

        private void showOperate()
        {
            if (selfOperate > 0)
            {
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, operate.card, false, AYMJMatch.match.mindex));
            }
            operate.playOver();
        }
    }
}
