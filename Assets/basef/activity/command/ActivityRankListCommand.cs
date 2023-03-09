using cambrian.common;
using cambrian.game;
using scene.game;
using basef.rank;

namespace basef.activity
{
    /// <summary> 活动排名 </summary>
    public class ActivityRankListCommand:UserCommand
    {
        private int activity_id;
        public ActivityRankListCommand(int sid)
        {
            this.id = CommandConst.PORT_PROMOTION_RESULT;
            this.activity_id = sid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(activity_id);
        }

        public override void bytesRead(ByteBuffer data)
        {
            RankList list = new RankList();
            list.bytesRead(data);
           
            callbackobj = list;
        }
    }
}
