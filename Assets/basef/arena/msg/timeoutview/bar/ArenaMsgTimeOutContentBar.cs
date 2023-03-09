using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 房间超时 内容 bar </summary>
    public class ArenaMsgTimeOutContentBar : ScrollBar
    {
        ArenaRoomEvent data;

        /// <summary> 房间号 </summary>
        private UnrealTextField roomid;

        /// <summary> 房间超时时间 </summary>
        private UnrealTextField longago;

        /// <summary> 备注 </summary>
        private UnrealTextField remake;

        protected override void xinit()
        {
            roomid = transform.Find("roomid").GetComponent<UnrealTextField>();
            roomid.init();

            longago = transform.Find("longago").GetComponent<UnrealTextField>();
            longago.init();

            remake = transform.Find("remake").GetComponent<UnrealTextField>();
            remake.init();
        }

        protected override void xrefresh()
        {
            if (data != null)
            {
                roomid.text = data.room.ToString();

                //当前时间减去房间创建时间 小时向下取整 并显示
                getTimeAgo();

                remake.text = "房间创建于 " +
                    TimeKit.format(data.createTime,"yyyy-MM-dd HH:mm") + ",已超时!";
            }
        }

        private void getTimeAgo()
        {
            long intertime = TimeKit.currentTimeMillis - data.createTime;

            if (intertime > TimeKit.DAY_MILLS)
            {
                this.longago.text = "(1天以前)";
            }
            else if (intertime > TimeKit.HOUR_MILLS)
            {
                this.longago.text = "(1小时以前)";
            }
            else if (intertime > TimeKit.MIN_MILLS)
            {
                this.longago.text = "(1分钟以前)";
            }
            else
            {
                this.longago.text = "";
            }
        }



        public void setData(object obj = null)
        {
            data = (ArenaRoomEvent)obj;
        }

        public ArenaRoomEvent getData()
        {
            return data;
        }
    }
}
