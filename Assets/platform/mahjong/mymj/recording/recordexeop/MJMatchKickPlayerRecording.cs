namespace platform.mahjong
{
    public class MJMatchKickPlayerRecording : Process
    {
        MJKickPlayerOperate operate;

        public MJMatchKickPlayerRecording(MJBaseOperate operate)
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
