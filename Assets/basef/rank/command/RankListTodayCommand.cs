using cambrian.common;
using cambrian.game;
using scene.game;

/// <summary>
/// 获取今日排行榜
/// </summary>
namespace basef.rank
{
    public class RankListTodayCommand : UserCommand
    {
        public RankListTodayCommand()
        {
            this.id = CommandConst.PORT_FKC_RANK_LIST_DAY;
        }

        public override void bytesRead(ByteBuffer data)
        {
            RankList list=new RankList();
            list.bytesRead(data);
            list.setJueShu(data.readLong());
            callbackobj = list;
        }
    }
}
