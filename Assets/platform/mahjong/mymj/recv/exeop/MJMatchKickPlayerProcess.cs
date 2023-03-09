namespace platform.mahjong
{
    public class MJMatchKickPlayerProcess : Process
    {
        MJKickPlayerOperate operate;

        public MJMatchKickPlayerProcess(MJBaseOperate operate)
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
