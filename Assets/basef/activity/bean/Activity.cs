using basef.award;
using cambrian.common;
using cambrian.game;

namespace basef.activity
{
    /// <summary> 活动 </summary>
    public class Activity : Sample
    {
        /// <summary> 活动标题 </summary>
        string title;

        /// <summary> 活动描述 </summary>
        string description;

        /// <summary> 活动图片 </summary>
        string img;

        /// <summary> 活动奖励 </summary>
        Award[] awards;

        /// <summary> 发布时间 </summary> 发布后才会在前端显示该活动
        int releaseTime;

        /// <summary> 开始时间 </summary>
        int beginTime;

        /// <summary> 结束时间 </summary> 结束后前端仍然可以看到该活动，同时可以看到活动结果数据
        int overTime;

        /// <summary> 移除时间 </summary> 到达该时间后，前端将不再看到该活动
        int removeTime;

        /// <summary> 获取发布时间 </summary>
        public long getReleaseTime()
        {
            return releaseTime * 1000L;
        }

        /// <summary> 开始时间 </summary>
        public long getBeginTime()
        {
            return beginTime * 1000L;
        }

        /// <summary> 获取结束时间 </summary>
        public long getOverTime()
        {
            return overTime * 1000L;
        }

        /// <summary> 获取移除时间 </summary>
        public long getRemoveTime()
        {
            return removeTime * 1000L;
        }

        public string getTitle()
        {
            return title;
        }

        public string getDescription()
        {
            return description;
        }

        public string getImg()
        {
            return ServerInfos.server.getHttPServerUrl() + img + "?" + MathKit.random(1,10000);
        }

        public Award[] getAwards()
        {
            return awards;
        }

        public override void bytesRead(ByteBuffer data)
        {
            sid = data.readInt();
            this.title = data.readUTF();
            this.description = data.readUTF();
            this.img = data.readUTF();
            this.releaseTime = data.readInt();
            this.beginTime = data.readInt();
            this.overTime = data.readInt();
            this.removeTime = data.readInt();

            int len = data.readInt();
            awards = new Award[len];
            for (int i = 0; i < len; i++)
            {
                awards[i] = new Award();
                awards[i].bytesRead(data);
            }
        }
    }
}
