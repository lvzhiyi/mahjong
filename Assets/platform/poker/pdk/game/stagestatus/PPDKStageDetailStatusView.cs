using cambrian.game;

namespace platform.poker
{
    public class PPDKStageDetailStatusView : PKStageDetailStatusView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        public override void showText(int type, int index)
        {
            switch (type)
            {
                case PKStageStatus.PASS:
                    text.text = "要不起";
                    type = SoundManager.buyao;
                    break;
                case PKStageStatus.MING_PAI:
                    text.text = "明牌";
                    type = -1;
                    break;
            }
            if (type != -1) SoundManager.playPKOther(type, Room.room.players[index].player.sex);
        }
    }
}
