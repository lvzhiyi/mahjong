namespace platform.spotred.room
{
    /// <summary>
    /// 移除玩家(金币场中使用的，前端自定义(当玩家托管时,服务器端自动踢人))
    /// </summary>
    public class RemovePlayerProcess : Process
    {
        private RemovePlayerOperate op;


        public RemovePlayerProcess(RemovePlayerOperate op)
        {
            this.op = op;
        }

        public override void execute()
        {
            if(Room.room!=null)
                Room.room.removePlayer(op.userid);
            op.isOver = true;
        }
    }
}
