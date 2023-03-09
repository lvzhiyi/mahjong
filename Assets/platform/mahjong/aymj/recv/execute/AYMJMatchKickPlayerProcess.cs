namespace platform.mahjong
{
    public class AYMJMatchKickPlayerProcess : Process
    {
        MJKickPlayerOperate operate;

        public AYMJMatchKickPlayerProcess(MJBaseOperate operate)
        {
            this.operate = (MJKickPlayerOperate)operate;
        }

        public override void execute()
        {
            if (Room.room != null)
                Room.room.removePlayer(operate.userid);
            operate.playOver();
        }
    }
}
