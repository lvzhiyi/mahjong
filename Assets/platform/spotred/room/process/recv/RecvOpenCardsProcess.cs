using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 翻牌
    /// </summary>
    public class RecvOpenCardsProcess:Process
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        int operate;

        private OpenCardOperate op;

        public RecvOpenCardsProcess(OpenCardOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        /// <summary>
        /// 接收翻牌消息
        /// </summary>
        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            panel.setPMCard(op.card);
            CPMatch.match.step= op.step;

            if (CPMatch.match.addLastPlayerCardToDisable())
            {
                panel.hideDisLastCard(CPMatch.match.getLastPlayerIndex());
                panel.moveShowCardToDisable(CPMatch.match.getLastPlayerIndex(), ab);
                CPMatch.match.ResetPlayerCard();
            }
            else
            {
                ab(false);
            }
        }

        public void ab(bool b)
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
           if(b)
                CPMatch.match.ResetPlayerCard();


            panel.hideAllPlayCard();
            panel.hideFanCard();

            panel.showFanCard(op.card, op.index, action);
           
        }

        public void action(bool b)
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            CPMatch.match.paidui= op.paidui;
            CPMatch.match.setStage(op.stage);
            panel.refreshCardNum();

            CPMatch.match.setLastPlayerCard(op.index, op.card);
            panel.refreshClock(op.round);
            panel.showTextinfo(false);

            SoundManager.playChangPai(Room.room.players[op.index].player.sex, Card.getName(op.card), CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));
            int count = panel.operateView.showButton(operate, op.paidui);
            if (count > 0)
            {
                int[] disableCard = new int[0];
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), disableCard);
            }

            op.isOver = true;
        }
    }
}
