using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 执行飘结果
    /// </summary>
    public class AYMJMatchAllPiaoOverRecording:Process
    {
        MJMatchAllPiaoOverOperate operate;

        int selfOperate;

        public AYMJMatchAllPiaoOverRecording(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllPiaoOverOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            AYMJPlayerCards[] playerCards = match.getPlayerCardss<AYMJPlayerCards>();
           
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (operate.hadPiao(i))
                {
                    playerCards[i].setPiao();
                }
            }

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

           
            panel.gameView.getPiaoStatusView().showpiao(match.getPlayerCardsStatus());
            

            panel.gameView.getPiaoView().setVisible(false);
            

            showOperate();
        }

        private void showOperate()
        {
            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }

       
    }
}
