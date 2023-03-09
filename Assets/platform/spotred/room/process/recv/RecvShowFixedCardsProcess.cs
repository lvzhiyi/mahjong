using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收显示偷牌(这个是完成后,显示所有玩家首轮滑下来的牌)
    /// </summary>
    public class RecvShowFixedCardsProcess:Process
    {
        private int operate;

        private ShowFixedOperate op;
        public RecvShowFixedCardsProcess(ShowFixedOperate op)
        {
            this.operate = op.operates[op.selfIndex];
            this.op = op;
        }

        public override void execute()
        {
            SpotRoomPanel panel=UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch match = CPMatch.match;
            match.step=op.step;
            match.setStage(op.stage);
            for (int i = 0; i < op.cards.Length; i++)
            {
                if (i== match.mindex) continue;
                match.getPCards()[i].fixCards.clear();
                match.getPCards()[i].addFixCard(op.cards[i]);
                panel.refreshFixed(i);
            }

            if (operate > 0)
            {
                panel.operateView.showButton(operate, op.paidui);
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), op.disableCard);
            }

            if (operate == OperateView.CAN_DISCARD)
            {
                panel.allHandView.selfView.showHuaDong();
            }
            else
            {
                panel.allHandView.selfView.hideHuaDong();
            }

            panel.refreshFuShu();

            panel.showTextinfo(false);

            this.op.isOver = true;
        }
    }
}
