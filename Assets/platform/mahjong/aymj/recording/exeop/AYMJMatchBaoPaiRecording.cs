using UnityEngine;

namespace platform.mahjong
{
    public class AYMJMatchBaoPaiRecording : Process
    {
        AYMJMatchBaoPaiOperate operate;

        int selfOperate;

        public AYMJMatchBaoPaiRecording(MJBaseOperate baseOperate)
        {
            operate = (AYMJMatchBaoPaiOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.getGameView<AYMJGameView>().getBaoCardView().setSingleDistype(operate.index);
            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.setBaoCard();


            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);

            operate.playOver();
        }
    }
}
