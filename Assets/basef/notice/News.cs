using cambrian.common;

namespace basef.notice
{
    public class News
    {
        /** 标题/概述  */
        public string title;
        /** 具体内容  */
        public string info;
        /** 开始显示时间  */
        public long starttime;
        /** 结束显示时间  */
        public long stoptime;
        /** 最后修改时间  */
        public long lasttime;

        public string getTitle()
        {
            return this.title;
        }

        public string getInfo()
        {
            return this.info;
        }

        public string getTime()
        {
            return TimeKit.format(this.lasttime, "yyyy/MM/dd HH:mm:ss");
        }

        public virtual bool isEffective()
        {
            long time = TimeKit.currentTimeMillis;
            if (time > starttime && time < stoptime)
                return true;
            return false;
        }
    }
}
