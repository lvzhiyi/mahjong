using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收报牌消息
    /// </summary>
    public class RecvBaoPaiProcess: Process
    {
        private int operate;

        private BaopaiOperate op;
        public RecvBaoPaiProcess(BaopaiOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            CPMatch match = CPMatch.match;
            panel.setOperate(operate);
            match.step=op.step;
            match.setStage(op.stage);
            match.getPlayerCardIndex<CPPlayerCards>(op.index).setCanplayCards(op.baocards);

            if (op.index == Room.room.indexOf())
            {
                int[] disableCard = match.getBaoDisableCards();
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), disableCard);
                panel.operateView.hideAllBtn();
            }

            SoundManager.playBaoPai(Room.room.players[op.index].player.sex);
            panel.playAnimation(op.index, MovieClipView.MC_TYPE_BAO);
            panel.refreshBaoPaiIndex(op.index);
            match.getPlayerCardIndex<CPPlayerCards>(op.index).setStatus(CPPlayerCards.STATUS_BAOPAI);
            panel.refreshClock(op.round);

            panel.operateView.showButtonList(OperateView.getShowOperate(operate));

            if (StatusKit.hasStatus(operate,OperateView.CAN_DISCARD))
                panel.allHandView.selfView.showHuaDong();
            else
                panel.allHandView.selfView.hideHuaDong();
            panel.refreshFuShu();
            panel.showTextinfo(false);

            op.isOver = true;
        }
    }
}
