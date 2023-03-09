using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收吃牌消息
    /// </summary>
    public class RecvChowCardsProcess:Process
    {
        int operate;//操作类型

        private ChowOperate op;

        public RecvChowCardsProcess(ChowOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel= UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch.match.step=op.step;
            CPMatch.match.setStage(op.stage);


            SoundManager.playChi(Room.room.players[op.index].player.sex, CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));

            if (op.index == CPMatch.match.mindex)//自己吃
            {
                ArrayList<int> handcards= CPMatch.match.getPCards()[op.index].delHandCard(op.card, 1);
                panel.allHandView.selfView.getHandView().showHandCard(handcards.toArray(),new int[0]);//刷新手牌

                CPMatch.match.getPCards()[op.index].addFixCard(FixedCards.CHOW, new[] { op.destCard, op.card });
                
            }
            else//别人吃
            {
                CPMatch.match.getPCards()[op.index].addFixCard(FixedCards.CHOW, new []{ op.destCard , op.card });
                panel.allHandView.selfView.getHandView().showHandCard(CPMatch.match.getSelfHandCard(), new int[0]);//刷新手牌

            }

            panel.playAnimation(op.index, MovieClipView.MC_TYPE_CHI);
            panel.hideFanCard();
            panel.hideAllPlayCard();
            panel.refreshMoveFixed(op.index, animationOver);
        }

        public void animationOver()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.refreshFixed(op.index);
            panel.refreshDisCard(op.destindex);

            if (operate > 0)
            {
                panel.operateView.showButton(operate, op.paidui);
            }
            else
            {
                panel.operateView.hideAllBtn();
            }

            panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), op.disableCard);

            CPMatch.match.ResetPlayerCard();

            panel.refreshClock(op.round);

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
