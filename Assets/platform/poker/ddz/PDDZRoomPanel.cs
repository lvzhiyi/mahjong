using DragonBones;

namespace platform.poker
{
    /// <summary> 斗地主 房间 </summary>
    public class PDDZRoomPanel : PKRoomPanel
    {
        public UnityArmatureComponent overAniamtion { get; private set; }

        protected override void xinit()
        {
            base.xinit();

            topView = GetComponent<PKTopView>("top");

            downView = GetComponent<PKDownView>("down");

            headView = GetComponent<PKHeadView>("heads");

            waitView = GetComponent<PKWaitView>("wait");

            gameView = GetComponent<PDDZGameView>("game");

            overAniamtion = content.Find("overAniamtion").GetComponent<UnityArmatureComponent>();
        }

        protected override void xrefresh()
        {
            overAniamtion.gameObject.SetActive(false);
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
            showFullTimeView();
            gameView.setVisible(false);
           
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
            DDZMatch.match.selectCard.clear();
        }

        public override void removeDeskCard(int pos)
        {
            DDZMatch.match.deskCard.remove(pos);
        }
    }
}
