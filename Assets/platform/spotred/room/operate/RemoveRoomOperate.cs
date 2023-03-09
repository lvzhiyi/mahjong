namespace platform.spotred.room
{
    /// <summary>
    /// 金币场自己被移除房间
    /// </summary>
    public class RemoveRoomOperate:BaseOperate
    {
        public RemoveRoomOperate(int type= RecvCPMatchMsg.MSG_REMOVE_ROOM, int step=0, int[] operates=null, int selfIndex=0, int stage=0, int round=0, int paidui=0, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            
        }
       

        public override long getWaittime()
        {
            return 0;
        }
    }
}
