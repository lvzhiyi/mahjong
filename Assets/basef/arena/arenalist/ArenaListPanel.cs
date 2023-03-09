using cambrian.common;
using cambrian.game;
using basef.player;
using scene.game;
using cambrian.unreal.scroll;
using UnityEngine.UI;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 赛场列表面板
    /// </summary>
    public class ArenaListPanel : UnrealLuaPanel
    {

        /// <summary>
        /// 自主创建赛场视图
        /// </summary>
        private ArenaListView create;
        /// <summary>
        /// 加入赛场视图
        /// </summary>
        private ArenaListView join;
        /// <summary>
        /// 赛场列表
        /// </summary>
        private ArrayList<Arena> createList;
        /// <summary>
        /// 赛场列表
        /// </summary>
        private ArrayList<Arena> joinList;


        protected override void xinit()
        {
            this.create = this.transform.Find("Canvas").Find("content").Find("create").GetComponent<ArenaListView>();
            this.create.init();
            this.join = this.transform.Find("Canvas").Find("content").Find("join").GetComponent<ArenaListView>();
            this.join.init();
        }

        public void setData(ArrayList<Arena> createList, ArrayList<Arena> joinList)
        {
            this.createList = createList;
            this.joinList = joinList;
        }

        protected override void xrefresh()
        {
            create.setData(createList);
            join.setData(joinList);
            create.refresh();
            join.refresh();
        }
    }
}
