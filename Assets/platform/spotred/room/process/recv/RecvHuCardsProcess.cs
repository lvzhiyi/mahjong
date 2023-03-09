using cambrian.game;

namespace platform.spotred.room
{
    public class RecvHuCardsProcess:Process
    {
        private HuOperate op;
        public RecvHuCardsProcess(HuOperate op)
        {
            this.op = op;
        }

        public override void execute()
        {
            if (op.indexs == null || op.indexs.Length == 0) return;

            SpotRoomPanel panel= UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            CPMatch.match.hu_card = op.hu_card;
            CPMatch.match.step= op.step;
            CPMatch.match.setStage(op.stage);

            //播放胡的动画
            for (int i = 0; i < op.indexs.Length ; i++)
            {
                panel.showHu(op.indexs[i]);
            }

            panel.showTextinfo(false);
            if (op.indexs != null) SoundManager.playHu(Room.room.players[op.indexs[0]].player.sex, CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));

            panel.allHandView.selfView.hideHuaDong();
            op.isOver = true;
        }
    }
}
