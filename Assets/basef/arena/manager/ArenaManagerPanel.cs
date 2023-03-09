using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场管理界面
    /// </summary>
    public class ArenaManagerPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 赛场设置1，赛场管理2，赛场玩法3，会员管理4，推广管理5，门票管理6，开桌统计7
        /// </summary>
        public const int
            ARENA_SETTING = 1,
            ARENA_MANAGE = 2,
            ARENA_WANFA = 3,
            ARENA_MEMBER = 4,
            ARENA_EXTENSION = 5,
            ARENA_TICKET = 6,
            ARENA_OPEN_TABLE = 7;


        string[] nametype = new string[]
        {
            "休闲场设置","休闲场管理","休闲场玩法","会员管理","推广管理","奖励管理","开桌统计"
        };

        int[] types = new int[]
        {
            ARENA_SETTING,ARENA_MANAGE,ARENA_WANFA,ARENA_MEMBER,ARENA_EXTENSION,ARENA_TICKET,ARENA_OPEN_TABLE
        };

        private int selectType = 0;

        private ScrollContainer container;

        [HideInInspector] public ArenaManagerView managerView;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("typeshow").GetComponent<ScrollContainer>();
            container.init();
            managerView = content.Find("content").Find("view").GetComponent<ArenaManagerView>();
            managerView.init();
        }

        public void setShowType()
        {
            if (Arena.arena.getMember().isMaster())
            {
                nametype = new string[] { "休闲场设置","休闲场管理","休闲场玩法","会员管理","推广管理","奖励管理","开桌统计" };
                types = new int[] { ARENA_SETTING,ARENA_MANAGE,ARENA_WANFA,ARENA_MEMBER,ARENA_EXTENSION,ARENA_TICKET,ARENA_OPEN_TABLE };
            }
            else
            {
                nametype = new string[] { "会员管理","推广管理","奖励管理" };
                types = new int[] { ARENA_MEMBER,ARENA_EXTENSION,ARENA_TICKET };
            }
        }

        protected override void xrefresh()
        {
            setShowType();
            selectType = types[0];
            container.updateData(callback);
            container.resize(nametype.Length);
            container.resizeDelta();

            managerView.refresh();
        }



        public void callback(ScrollBar bar,int index)
        {
            ArenaManagerBar managerbar = (ArenaManagerBar)bar;
            managerbar.setData(types[index],nametype[index],selectType);
            managerbar.index_space = index;
            managerbar.refresh();
        }

        public void updateBars(int type)
        {
            selectType = type;
            for (int i = 0; i < this.container.bars.count; i++)
            {
                ArenaManagerBar bar = (ArenaManagerBar)this.container.bars.get(i);
                bar.setSelected(type);
                bar.refresh();
            }
        }
    }
}
