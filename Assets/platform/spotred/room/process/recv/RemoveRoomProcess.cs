using platform.spotred;

namespace platform.spotred.room
{
    public class RemoveRoomProcess:Process
    {
        private RemoveRoomOperate op;

       
        public RemoveRoomProcess(RemoveRoomOperate op)
        {
            this.op = op;
        }

        public override void execute()
        {
            Room.clear();
            op.isOver = true;
        }
    }
}
