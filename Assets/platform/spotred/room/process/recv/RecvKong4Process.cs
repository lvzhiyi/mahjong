using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 杠4张(除去划牌阶段),只有小家才会出现
    /// </summary>
    public class RecvKong4Process:Process
    {
        private Kong4Opreate op;

        private int operate;
        public RecvKong4Process(Kong4Opreate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch.match.step=op.step;
            CPMatch.match.setStage(op.stage);

            if (op.index == CPMatch.match.mindex)//自己吃
            {
                ArrayList<int> handcards = CPMatch.match.getPCards()[op.index].delHandCard(op.card, 4);
                panel.allHandView.selfView.getHandView().showHandCard(handcards.toArray(), op.disableCard);//刷新手牌

                panel.operateView.showButtonList(OperateView.getShowOperate(operate));
            }

            CPMatch.match.getPCards()[op.index].addFixCard(FixedCards.KONG, new[] { op.card, op.card, op.card, op.card });
            panel.refreshFixed(op.index);

            this.op.isOver = true;
        }
    }
}
