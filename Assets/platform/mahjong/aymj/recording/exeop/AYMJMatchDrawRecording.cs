using System;

namespace platform.mahjong
{
    /// <summary>
    /// 摸牌
    /// </summary>
    public class AYMJMatchDrawRecording : Process
    {

        MJMatchDrawOperate operate;

        int selfOperate;
        

        public AYMJMatchDrawRecording(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDrawOperate) baseOperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        private AYMJMatch match;
        public override void execute()
        {
            match = AYMJMatch.match;
            exe();
        }

        private void exe()
        {
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
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

            panel.refreshHandCard(operate.index, null, false);

            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates, operate.card);
            operate.playOver();
        }
    }
}
