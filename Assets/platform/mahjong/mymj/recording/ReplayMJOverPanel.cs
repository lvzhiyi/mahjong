namespace platform.mahjong
{
    public class ReplayMJOverPanel:MJOverPanel
    {
        public override void refrshData(Room room, MJMatch match)
        {
            base.refrshData(room, match);
            button.text.text = "返   回";
        }
    }
}
