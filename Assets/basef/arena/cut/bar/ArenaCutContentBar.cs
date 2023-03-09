using basef.player;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 竞赛场 切换赛场 内容 Bar </summary>
    public class ArenaCutContentBar : ScrollBar
    {
        Arena data;

        /// <summary> 赛场ID </summary>
        UnrealTextField arenasid;

        /// <summary> 赛场名称 </summary>
        UnrealTextField arenaname;

        /// <summary> 场主名称 </summary>
        UnrealTextField mastername;

        /// <summary> 进行等待人数 </summary>
        UnrealTextField allnum;

        /// <summary> 赛场头像 </summary>
        PlayerHeadView head;

        SpritesList bg;

        UnrealLuaSpaceObject selfarena;

        bool selected = false;

        protected override void xinit()
        {
            arenasid = this.transform.Find("arenasid").GetComponent<UnrealTextField>();
            arenasid.init();

            arenaname = this.transform.Find("arenaname").GetComponent<UnrealTextField>();
            arenaname.init();

            mastername = this.transform.Find("mastername").GetComponent<UnrealTextField>();
            mastername.init();

            allnum = this.transform.Find("allnum").GetComponent<UnrealTextField>();
            allnum.init();

            head = this.transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            bg = this.transform.Find("bg").GetComponent<SpritesList>();

            selfarena = this.transform.Find("selfarena").GetComponent<UnrealLuaSpaceObject>();
            selfarena.init();
        }

        protected override void xrefresh()
        {
            if (data.getMaster() == Player.player.userid)
            {
                head.GetComponent<RectTransform>().transform.localPosition = new Vector2(-205,-52.5f);
            }
            else
            {
                head.GetComponent<RectTransform>().transform.localPosition = new Vector2(-205,-70);
            }
            head.setData(data.getIcon(),data.getId());
            head.refresh();
            selfarena.setVisible(data.getMaster() == Player.player.userid);
            arenasid.text = data.getId().ToString();
            mastername.text = data.masterName;
            arenaname.text = data.name;
            allnum.text = data.members + "/" + data.maxMembers;
            allnum.setVisible(data.getMaster()==Player.player.userid);

            if (selected)
            {
                bg.ShowIndex(1,false);
            }
            else bg.ShowIndex(0,false);
        }

        public void setData(Arena data)
        {
            this.data = data;
        }

        public Arena getArena()
        {
            return data;
        }

        public void setData(Arena data,bool selected)
        {
            this.data = data;
            this.selected = selected;
        }
    }
}
