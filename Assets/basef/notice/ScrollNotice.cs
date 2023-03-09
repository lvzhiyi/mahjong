using cambrian.common;
using UnityEngine;

namespace basef.notice
{
    public class ScrollNotice 
    {
        public const int NOTICE_DEL = 1;

        public int id;
        public string content;
        /** 开始显示时间  */
        public int releaseTime;
        /** 结束显示时间  */
        public int removeTime;
        ///** 两次显示的间隔时间 */
        //public long spacetime;
        ///** 每次显示多久 */
        //public long showtime;
        ///** 最后修改时间  */
        //public long lasttime;
        public int status;


        public bool isEffective()
        {
            int time = (int)(TimeKit.currentTimeMillis/1000);
            if (status == NOTICE_DEL) return false;
            if (removeTime == 0) return true;
            if ((time > releaseTime && time < removeTime))
                return true;
            return false;
        }

        public void bytesRead(ByteBuffer data)
        {
            this.id = data.readInt();
            this.content = data.readUTF();
            this.releaseTime = data.readInt();
            this.removeTime = data.readInt();
            this.status = data.readInt();
        }

        //public Notice copy()
        //{
        //    Notice notic=new Notice();
        //    notic.info = info;
        //    notic.starttime = this.starttime;
        //    notic.stoptime = this.stoptime;
        //    notic.spacetime = this.spacetime;
        //    notic.showtime = this.showtime;
        //    notic.lasttime = this.lasttime;
        //    return notic;
        //}
    }
}
