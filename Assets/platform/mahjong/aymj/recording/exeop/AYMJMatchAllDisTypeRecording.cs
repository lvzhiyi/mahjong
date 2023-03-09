using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 所有玩家定缺完成
    /// </summary>
    public class AYMJMatchAllDisTypeRecording : Process
    {
        MJMatchAllDisTypeOperate operate;

        int selfOperate;

        public AYMJMatchAllDisTypeRecording(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllDisTypeOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui,operate.round);
            AYMJPlayerCards[] playerCards = match.getPlayerCardss<AYMJPlayerCards>();
            for (int i = 0; i < playerCards.Length; i++)
                playerCards[i].setDistype(operate.distypes[i]);
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            panel.gameView.getDQView().setVisible(false);
            panel.gameView.showDistypeCardView(operate.distypes);
            showOperate();
        }

        private void showOperate()
        {
            AYMJMatch match = AYMJMatch.match;
            match.getSelfPlayerCards<AYMJPlayerCards>().handCardSort(MJOperate.isDisCard(selfOperate));
            if(match.banker==match.mindex)
                match.getSelfPlayerCards<AYMJPlayerCards>().handCardSort1();
            panel.refreshHandCard(0, null, false, true);
            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }
    }
}
