using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    public class ArenaPersonalJournalBar : ScrollBar
    {
        PersonalJournalEventRecord data;

        /// <summary> 时间 </summary>
        private UnrealTextField time;
        
        /// <summary> 事件 </summary>
        private UnrealTextField type;
        

        protected override void xinit()
        {

            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();
            

            type = transform.Find("type").GetComponent<UnrealTextField>();
            type.init();
        }

        protected override void xrefresh()
        {
            setTime();
            type.text =Player.player.name+"(ID:"+data.userid+")"+ PersonalJournalEventRecord.getType(data.type)+"\n"+"房间ID:"+data.cause;
        }
        private void setTime()
        {
            string t = TimeKit.format(data.time, "yyyy-MM-dd HH:mm:ss");
            string yymmdd = t.Substring(0, 10);
            string hhmmss = t.Substring(11, 8);

            this.time.text = yymmdd + "\n" + hhmmss;
        }
       
        public void setData(PersonalJournalEventRecord record)
        {
            data = record;
        }

    }
}