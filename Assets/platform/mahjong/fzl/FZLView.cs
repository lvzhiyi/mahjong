using cambrian.common;
using UnityEngine.UI;

namespace platform.mahjong.guizhou
{
    /// <summary>
    /// 防暂离视图
    /// </summary>
    public class FZLView:UnrealLuaSpaceObject
    {
        //Image quan;
        Text time_text;
        long lasttime = 0, overtime = 0, leaveTimeout = 0;
        int t;

        protected override void xinit()
        {
            //quan = transform.Find("quan").GetComponent<Image>();
            time_text = transform.Find("time").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            Room room = Room.room;
            long fulltime = room.fulltime;
            if (fulltime == 0) return;
            leaveTimeout = room.getRule().getIntAtribute("leaveTimeout");
            if (leaveTimeout == 0) return;
            overtime = fulltime + leaveTimeout;
            
        }
        private void Update()
        {
            if (overtime == 0) return;
            if (TimeKit.currentTimeMillis - lasttime>= 1000)
            {
                lasttime = TimeKit.currentTimeMillis;
                show();
            }
        }

        private void show()
        {
            t = (int)(overtime - TimeKit.currentTimeMillis) / 1000;
            int temp = (int)(leaveTimeout / 1000);
            if (t == 0)
            {
                overtime = 0;
                setVisible(false);
            }
            time_text.text = t.ToString();
}
        
    }
}
