using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.activity
{
    /// <summary> 获取活动列表 </summary>
    public class ActivityListCommand : UserCommand
    {
        public ActivityListCommand()
        {
            this.id = CommandConst.PORT_PROMOTION_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();

            ArrayList<Activity> list = new ArrayList<Activity>();

            for (int i = 0; i < len; i++)
            {
                Activity activity = new Activity();
                activity.bytesRead(data);
                list.add(activity);
            }

            callbackobj = list;

        }
    }
}
