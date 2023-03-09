using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 所有玩家定缺完成
    /// </summary>
    public class AYMJMatchAllDisTypeProcess : Process
    {
        MJMatchAllDisTypeOperate operate;

        int selfOperate;

        public AYMJMatchAllDisTypeProcess(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllDisTypeOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui,operate.round);
            AYMJPlayerCards[] playerCards = match.getPlayerCardss<AYMJPlayerCards>();
            for (int i = 0; i < playerCards.Length; i++)
                playerCards[i].setDistype(operate.distypes[i]);
            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
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
            if (selfOperate > 0)
            {
                int card = match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                panel.gameView.getOperateView()
                    .showOperate(match.getMJOperateInfos(selfOperate, card, false,match.mindex));
                TingCardsInfo[] tinginfos = match.getSelfTingCards(match.mindex);
                panel.refreshHandCard(match.mindex, tinginfos, false);
                if (MJOperate.isDisCard(selfOperate))
                    panel.gameView.showDisCardRedmine(true);
            }
            else
            {
                panel.refreshSingleHandCard(match.mindex);
            }
            operate.playOver();
        }
    }
}
