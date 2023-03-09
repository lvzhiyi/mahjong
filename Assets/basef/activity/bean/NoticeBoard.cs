
using cambrian.common;
using cambrian.game;

namespace basef.activity
{
    /// <summary> 公告 </summary>
    public class NoticeBoard : BytesSerializable
    {
        public int sid;
        /// <summary> 公告名字 </summary> 条目按钮上显示用
        string name;
        /// <summary> 活动标题 </summary>
        string title;
        /// <summary> 公告内容 </summary>
        string content;
        /// <summary> 活动图片 </summary>
        string img;
        /// <summary> 发布时间 </summary> 发布后才会在前端显示该公告
        int releaseTime;
        /// <summary> 移除时间 </summary> 到达该时间后，前端将不再看到该公告
        int removeTime;

        public int status;

        /// <summary> 获取发布时间 </summary>
        public long getReleaseTime()
        {
            return releaseTime * 1000L;
        }

        /// <summary> 获取移除时间 </summary>
        public long getRemoveTime()
        {
            return removeTime * 1000L;
        }

        public string getName()
        {
            return name;
        }

        public string getTitle()
        {
            return title;
        }

        public string getImg()
        {
            return ServerInfos.server.getHttPServerUrl() + img + "?" + MathKit.random(1,10000);
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.sid = data.readInt();//这里读取的是sid
            this.name = data.readUTF();
            this.title = data.readUTF();
            this.content = data.readUTF();
            this.img = data.readUTF();
            this.releaseTime = data.readInt();
            this.removeTime = data.readInt();
            status=data.readInt();// 状态
        }
    }
}
