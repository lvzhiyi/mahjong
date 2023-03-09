using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 发送托管消息
    /// </summary>
    public class SendTrusteeProcess:MouseClickProcess
    {
        public bool isTrustee;
        public override void execute()
        {

            int roomid = Room.room == null ? 0 : Room.room.getRoomIndex();

            CommandManager.addCommand(new SendTrusteeCommand(isTrustee,roomid));
        }
    }
}
