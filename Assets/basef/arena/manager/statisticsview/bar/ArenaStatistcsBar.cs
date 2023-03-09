using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaStatistcsBar : UnrealLuaSpaceObject
    {
        private Text typename;

        private Text today;

        private Text yesterday;

        private Text week;

        private Text lastweek;

        //private Text moth;

        //private Text lastmoth;

        ArenaStatistcsBarData data;

        protected override void xinit()
        {
            typename = transform.Find("typename").GetComponent<Text>();
            today = transform.Find("today").GetComponent<Text>();
            yesterday = transform.Find("yesterday").GetComponent<Text>();
            week = transform.Find("week").GetComponent<Text>();
            lastweek = transform.Find("lastweek").GetComponent<Text>();
            //moth = transform.Find("moth").GetComponent<Text>();
            //lastmoth = transform.Find("lastmoth").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            switch (index_space)
            {
                case 0:
                    typename.text = "达标房间";
                    break;
                case 1:
                    typename.text = "未达标房间";
                    break;
                case 2:
                    typename.text = "消耗元宝";
                    break;
            }

            today.text = data.value[0].ToString();
            yesterday.text = data.value[1].ToString();
            week.text = data.value[2].ToString();
            lastweek.text = data.value[3].ToString();
            //moth.text = data.value[4].ToString();
            //lastmoth.text = data.value[5].ToString();
        }

        public void setData(ArenaStatistcsBarData data)
        {
            this.data = data;
        }
    }
}
