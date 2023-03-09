namespace platform.spotred.room
{
    /// <summary>
    /// 金币房卡场玩家移除房间
    /// </summary>
    public class RemoveClubGoldPlayerOperate : BaseOperate
    {
        public long userid;
        public RemoveClubGoldPlayerOperate(long userid, int type = RecvCPMatchMsg.MSG_CLUB_GOLD_ROOM_REMOVE_PLAYER, int step = 0, int[] operates = null, int selfIndex = 0, int stage = 0, int round = 0, int paidui = 0, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.userid = userid;
        }


        public override long getWaittime()
        {
            return 0;
        }
    }
}
