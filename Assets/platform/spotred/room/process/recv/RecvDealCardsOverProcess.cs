using cambrian.common;

namespace platform.spotred.room
{
    public class RecvDealCardsOverProcess:Process
    {
        private DealCardsOverOperate op;

        private int operate;
        public RecvDealCardsOverProcess(DealCardsOverOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            ///开始比赛
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            CPMatch.match.setStage(op.stage);
            panel.allHandView.selfView.getHandView().showHandCard(CPMatch.match.GetMathPlayerHand(op.selfIndex), op.disablecard);

            if (operate > 0)
            {
                //自己有划牌的动作,如果是小家，另外处理

                if (panel.havOperate(OperateView.CAN_SLIP))
                {
                    if (CPMatch.match.isXiaoJia())
                    {
                        ArrayList<int> lls = new ArrayList<int>(new[] { OperateView.CAN_SLIP, OperateView.CAN_CANCEL }); //-1：小家的滑牌按钮,256:过
                        panel.operateView.showButtonList(lls);
                    }
                    else
                    {
                        if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                        {
                            panel.operateView.showButton(operate, op.paidui);
                        }
                    }
                }
                else
                {
                    panel.operateView.showButton(operate, op.paidui);
                }

                if (operate == OperateView.CAN_DISCARD)
                {
                    panel.allHandView.selfView.showHuaDong();
                }
                else
                {
                    panel.allHandView.selfView.hideHuaDong();
                }
            }

            panel.refreshClock(op.round);
            panel.refreshFuShu();

            op.isOver = true;
        }
    }
}
