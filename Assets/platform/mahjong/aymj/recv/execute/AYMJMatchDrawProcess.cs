using System;

namespace platform.mahjong
{
    /// <summary>
    /// 摸牌
    /// </summary>
    public class AYMJMatchDrawProcess : Process
    {

        MJMatchDrawOperate operate;

        int selfOperate;

        public static Action back;

        public AYMJMatchDrawProcess(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDrawOperate) baseOperate;
            selfOperate = operate.getSelfOperate();
            back = callback;
        }

        AYMJRoomPanel panel;

        private AYMJMatch match;
        public override void execute()
        {
            match = AYMJMatch.match;
            if (match.mindex == operate.index)
            {
                if(!AYMJSelectCardProcess.isMoveing)
                    exe();
            }
            else
            {
                exe();
            }
        }

        public void callback()
        {
            exe();
        }

        private void exe()
        {
            back = null;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            match.isTianhu = false;

            if (match.kongIndex != operate.index) //杠牌人的索引不是自己
            {
                match.resetKongStatus();
            }
            match.ResetPlayerCard();
            match.setLastPlayerCard(operate.index, operate.card);

            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.setMOCard(operate.card);

            if (match.mindex == operate.index)
            {
                if (MJOperate.isCanHu(selfOperate))
                    panel.refreshHandCard(operate.index, null, true);
                else
                    panel.refreshHandCard(operate.index, match.getSelfTingCards(AYMJMatch.match.mindex), true);
            }
            else
            {
                panel.refreshHandCard(operate.index, null, false);
            }

            showSYCard();

            showOperate();
        }

        private void showSYCard()
        {
            if (match.getRoomRule().rule.sid == 2003)
            {
                if (match.paidui == 8)
                {
                    panel.gameView.showfour4(1);
                }
            }
            else if (match.paidui == 4)
            {
                panel.gameView.showfour4(0);
            }
        }

        private void showOperate()
        {
            if (selfOperate > 0)
            {
                panel.gameView.getOperateView()
                    .showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, operate.card, AYMJMatch.match.isKongSups(), AYMJMatch.match.mindex
                    ));
            }
            if (operate.index == AYMJMatch.match.mindex)
            {
                panel.gameView.setTingView(null);
            }
            operate.playOver();
        }
    }
}
