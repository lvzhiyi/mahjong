using cambrian.game;
using basef.player;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收出牌消息
    /// </summary>
    public class RecvDisCardsProcess : Process
    {
        int operate;

        private DisCardOperate op;
        public RecvDisCardsProcess(DisCardOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        CPMatch match;
        public override void execute()
        {
            match = CPMatch.match;
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            panel.setPMCard(op.card);
            match.step = op.step;
            match.setStage(op.stage);

            if (match.addLastPlayerCardToDisable())
            {
                panel.hideDisLastCard(match.getLastPlayerIndex());
                panel.moveShowCardToDisable(match.getLastPlayerIndex(), ab);
                match.ResetPlayerCard();
            }
            else
            {
                ab(false);
            }
        }


        public void ab(bool b)
        {

            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            Room.room.players[op.index].player.setStatus(SimplePlayer.STATUS_ONLINE, true);
            panel.headView.ShowAllMatchPlayer(Room.room, match.dangjia,
                          match.xiaojia);
            if (b)
                match.ResetPlayerCard();

            match.setLastPlayerCard(op.index, op.card);


            panel.showPlayCard(op.index, op.card, true);

            int count = panel.operateView.showButton(operate, op.paidui);

            if (match.mindex == op.index)
            {
                match.removeHandCard(op.index, op.card, 1);
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);

                panel.allHandView.selfView.isShow(false,false);
                panel.allHandView.selfView.hideHuaDong();
            }
            else if (count > 0)
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);
            else
                panel.refreshClock(op.round);

            panel.refreshFuShu();
            SoundManager.playChangPai(Room.room.players[op.index].player.sex, Card.getName(op.card), CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));
            panel.showTextinfo(false);

            op.isOver = true;
        }
    }
}
