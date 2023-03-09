using basef.rule;
using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 单张偷牌
    /// </summary>
    public class RecvSingleProcess:Process
    {
        private SingleOperate op;

        public int operate;

        public RecvSingleProcess(SingleOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.index];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);

            CPMatch match = CPMatch.match;

            match.step= op.step;
            match.setStage(op.stage);

            Rule rule = match.getRoomRule().rule;
            if (rule.sid == 1017)
                SoundManager.playChangPai(Room.room.players[op.index].player.sex, Card.getName(op.card), rule.getIntAtribute("soundType"));

            if (op.index == match.mindex)//自己偷牌
            {
                if (op.ishandcard)
                {
                    ArrayList<int> handcards =match.getPCards()[op.index].delHandCard(op.card, 1);
                    panel.allHandView.selfView.getHandView().showHandCard(handcards.toArray(), new int[0]); //刷新手牌
                }

                match.getPCards()[op.index].addFixCard(FixedCards.SINGLE, new[] { op.card });
                panel.refreshFixed(op.index);
            }
            else//别人偷牌
            {
                match.getPCards()[op.index].addFixCard(FixedCards.SINGLE, new[] { op.card });

                panel.refreshFixed(op.index);

                panel.allHandView.selfView.getHandView().showHandCard(match.getSelfHandCard(), new int[0]);//刷新手牌
            }

            if (operate > 0)
            {
                panel.operateView.showButton(operate, op.paidui);
            }
            else
            {
                panel.operateView.hideAllBtn();
            }

            panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(),new int[0]);

            match.ResetPlayerCard();

            panel.refreshClock(op.round);

            panel.hideFanCard();

            panel.hideAllPlayCard();

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
            op.isOver = true;
        }
    }
}
