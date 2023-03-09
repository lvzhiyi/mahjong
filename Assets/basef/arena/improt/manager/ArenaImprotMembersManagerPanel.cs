using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 管理界面 导入成员面板 </summary>
    public class ArenaImprotMembersManagerPanel : UnrealLuaPanel
    {
        /// <summary> 总人数 </summary>
        UnrealTextField membercount;

        /// <summary> 已是本赛场成员 </summary>
        UnrealTextField arenacount;

        /// <summary> 可导入成员 </summary>
        UnrealTextField improtcount;

        /// <summary> 内容刷新容器 </summary>
        ScrollContainer container;

        /// <summary> 数据 </summary>
        ArrayList<ArenaImprotMembersManagerContentData> list = new ArrayList<ArenaImprotMembersManagerContentData>();

        protected override void xinit()
        {
            base.xinit();

            membercount = this.content.Find("centers").Find("down").Find("membercount").GetComponent<UnrealTextField>();
            membercount.init();

            arenacount = this.content.Find("centers").Find("down").Find("improtcount").GetComponent<UnrealTextField>();
            arenacount.init();

            improtcount = this.content.Find("centers").Find("down").Find("container").GetComponent<UnrealTextField>();
            improtcount.init();

            container = this.content.Find("centers").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        /// <summary> 已是本赛场成员 </summary>
        int arenaMember;

        protected override void xrefresh()
        {
            base.xrefresh();

            arenaMember = 0;
            container.updateData(updateScrollData);
            container.resize(list.count);

            this.membercount.text = list.count.ToString();
            this.arenacount.text = arenaMember.ToString();
            this.improtcount.text = (list.count - arenaMember).ToString();
        }

        private void updateScrollData(ScrollBar bars,int index)
        {
            ArenaImprotMembersManagerContentBar bar = (ArenaImprotMembersManagerContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();

            if (list.get(index).arenaMember)
            {
                arenaMember++;
            }
        }

        public void setData(object obj = null)
        {
            list = (ArrayList<ArenaImprotMembersManagerContentData>)obj;
        }
    }
}
