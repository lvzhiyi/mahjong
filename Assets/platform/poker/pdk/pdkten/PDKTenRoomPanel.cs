namespace platform.poker
{
    public class PDKTenRoomPanel: PKRoomPanel
    {
        protected override void xinit()
        {
            base.xinit();
            topView = GetComponent<PDKTopView>("top");

            downView = GetComponent<PKDownView>("down");

            headView = GetComponent<PKHeadView>("heads");

            waitView = GetComponent<PKWaitView>("wait");

            gameView = GetComponent<PPDKTenGameView>("game");
        }

        protected override void xrefresh()
        {
            if (Room.room == null) return;
            base.xrefresh();
            topView.refresh();
            downView.refresh();
            headView.refresh();
        }

        public override void refreshWaitView()
        {
            xrefresh();
            waitView.refresh();
            waitView.setVisible(true);
            gameView.setVisible(false);
            showFullTimeView();

        }

        public override void refreshGameView()
        {
            gameView.refresh();
            gameView.setVisible(true);
            waitView.setVisible(false);
            xrefresh();
        }

        public override void addRecvOperate(PKBaseOperate operate)
        {
            recvtimer.addOperate(operate);
        }

        public override void clearBaseOperate()
        {
            recvtimer.clearBaseOperate();
        }

        public override void claerSelectHands()
        {
            PDKTenMatch.match.selectCard.clear();
        }

        public override void removeDeskCard(int pos)
        {
            PDKTenMatch.match.deskCard.remove(pos);
        }
    }
}
