using platform.spotred;

namespace platform.spotred.room
{
    public class RecvUpdateStateProcess:Process
    {
        private int operate;//自己的操作
   
        private StateOperate op;
        public RecvUpdateStateProcess(StateOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }


        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch match = CPMatch.match;
            match.step=op.step;
            match.setStage(op.stage);

            if (operate == OperateView.CAN_DISCARD)
            {
                panel.refreshHandCard(op.disableCard);
                panel.allHandView.selfView.showHuaDong();
            }
            else
            {
                panel.allHandView.selfView.hideHuaDong();
            }


            if (match.addLastPlayerCardToDisable())
            {
                panel.refreshDisCard(match.getLastPlayerIndex());
                match.ResetPlayerCard();
            }
           
            int count = panel.operateView.showButton(operate, op.paidui);
            if (count > 0)
            {
                panel.refreshHandCard(op.disableCard);
            }

            panel.swtichDangIcon(op.dangjia);
            match.dangjia = op.dangjia;

            panel.refreshClock(op.round);
            panel.refreshFuShu();
            panel.showTextinfo(false);



            this.op.isOver = true;
        }
    }
}
