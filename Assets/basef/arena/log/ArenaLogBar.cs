using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 赛场日志BAR
    /// </summary>
    public class ArenaLogBar : ScrollBar
    {
        ArenaLog data;

        /// <summary> 时间 </summary>
        private UnrealTextField time;

        /// <summary> 信息 </summary>
        private UnrealTextField text;

        protected override void xinit()
        {
            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();

            text = transform.Find("name").GetComponent<UnrealTextField>();
            text.init();
        }
        public void setData(ArenaLog record)
        {
            data = record;
        }

        protected override void xrefresh()
        {
            text.text = data.getText();
            setTime();
        }

        private void setTime()
        {  
            this.time.text = TimeKit.format(data.time, "yyyy-MM-dd HH:mm:ss");
        }
    }
}
