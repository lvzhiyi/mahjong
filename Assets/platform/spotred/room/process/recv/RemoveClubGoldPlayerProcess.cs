using platform.spotred;
using basef.player;

namespace platform.spotred.room
{
    /// <summary>
    /// 移除玩家(金币场中使用的，前端自定义(当玩家托管时,服务器端自动踢人))
    /// </summary>
    public class RemoveClubGoldPlayerProcess : Process
    {
        private RemoveClubGoldPlayerOperate op;


        public RemoveClubGoldPlayerProcess(RemoveClubGoldPlayerOperate op)
        {
            this.op = op;
        }

        public override void execute()
        {
            if(Room.room!=null)
            {
                Room.room.removePlayer(op.userid);
                if (op.userid == Player.player.userid)
                {
                    ClubGoldOverPanel panel = UnrealRoot.root.checkDisplayObject<ClubGoldOverPanel>();
                    if (panel != null)
                    {
                        Room.clear();
                    }
                }
            }
                
            op.isOver = true;
        }
    }
}
