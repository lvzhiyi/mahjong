using cambrian.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收炸牌消息
    /// </summary>
    public class RecvZhaPaiProcess : Process
    {
        private int operate;

        private ZhaPaiOperate op;

        public RecvZhaPaiProcess(ZhaPaiOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            CPMatch match=CPMatch.match;
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.operateView.showButton(operate, op.paidui);
            if (op.isRemoveHandCard)
            {
                panel.showPlayCard(op.index, Card.INVISIBLE, false);
            }
            else
            {
                panel.showPlayCard(op.index, op.card, false);
            }

            if (match.mindex == op.index) //移除手牌
            {
                if (op.isRemoveHandCard)
                {
                    match.removeHandCard(op.index, op.card, 1);
                    panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);
                }
                else
                {
                    panel.setOperate(operate);
                    panel.setPMCard(op.card);
                    match.setLastPlayerCard(op.index, op.card);
                }
            }
            else if (!op.isRemoveHandCard)
            {
                panel.setOperate(operate);
                panel.setPMCard(op.card);
                match.setLastPlayerCard(op.index, op.card);
            }

            panel.allHandView.selfView.isShow(false, false);
            panel.allHandView.selfView.hideHuaDong();
            panel.refreshClock(op.round);
            panel.refreshFuShu();
            if (!op.isRemoveHandCard)
                SoundManager.playChangPai(Room.room.players[op.index].player.sex, Card.getName(op.card), CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));
            panel.showTextinfo(false);
            op.isOver = true;
        }
    }
}
