using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
namespace basef.arena
{
    public class ArenaAuxiliaryScoreContentBar : ScrollBar
    {
        AuxiliaryScoreEventRecord data;

        /// <summary> 时间 </summary>
        private UnrealTextField time;

        /// <summary> 用户名 </summary>
        private UnrealTextField username;

        /// <summary> 用户ID </summary>
        private UnrealTextField usersid;
        
        /// <summary> 变动方式 </summary>
        private UnrealTextField changetype;

        /// <summary> 头像 </summary>
        private PlayerHeadView head;

        protected override void xinit()
        {
            head = transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();

            username = transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            usersid = transform.Find("usersid").GetComponent<UnrealTextField>();
            usersid.init();
            
            changetype = transform.Find("type").GetComponent<UnrealTextField>();
            changetype.init();
        }

        protected override void xrefresh()
        {
            setTime();
            usersid.text = data.dest + "";
            username.text = data.name;
            head.setData(data.head, data.dest);
            head.refresh();
            setType();
        }
        private void setTime()
        {
            string t = TimeKit.format(data.time, "yyyy-MM-dd HH:mm:ss");
            string yymmdd = t.Substring(0, 10);
            string hhmmss = t.Substring(11, 8);

            this.time.text = yymmdd + "\n" + hhmmss;
        }
        private void setType()
        {
            if (data.type == 0)
            {
                changetype.text = "【" + data.cause + "】" + "设置积分预警值:" + data.warning/100 + "、" + "积分值:" + data.fufen/100;
                changetype.color = new Color(1f, 0f, 0f, 1f);
            }
            else if (data.type == 1)
            {
                changetype.text = "积分低于设置值，名下成员禁止游戏";
                changetype.color= new Color(1f, 0f, 0f, 1f);
            }
            else
            {
                changetype.text = "积分低于预警值";
                changetype.color = new Color(0.34f, 0.19f, 0.13f, 1f);
            }
        }
        public void setData(AuxiliaryScoreEventRecord record)
        {
            data = record;
        }

    }
}