using UnityEngine;

namespace platform.mahjong
{
    public class AYMJMatchBaoPaiProcess: Process
    {
        AYMJMatchBaoPaiOperate operate;

        int selfOperate;

        public AYMJMatchBaoPaiProcess(MJBaseOperate baseOperate)
        {
            operate = (AYMJMatchBaoPaiOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.getGameView<AYMJGameView>().getBaoCardView().setSingleDistype(operate.index);
            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.setBaoCard();


            if (selfOperate > 0)
            {
                int card = match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                Debug.Log("=============="+card);
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }

            operate.playOver();
        }
    }
}
