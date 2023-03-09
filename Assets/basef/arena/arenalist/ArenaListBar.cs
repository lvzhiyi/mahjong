using basef.player;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaListBar : ScrollBar
    {
        /// <summary>
        /// 名字
        /// </summary>
        private Text arenaName;
        /// <summary>
        /// id
        /// </summary>
        private Text arenaId;
        /// <summary>
        /// 人数
        /// </summary>
        private Text num;

        /// <summary>
        /// 
        /// </summary>
        [HideInInspector] public Arena arena;

        protected override void xinit()
        {
            this.arenaName = this.transform.Find("name").GetComponent<Text>();
            this.arenaId = this.transform.Find("id").GetComponent<Text>();
            this.num = this.transform.Find("num").GetComponent<Text>();
        }

        public void setData(Arena club)
        {
            this.arena = club;
        }

        protected override void xrefresh()
        {
            this.arenaName.text = this.arena.name;
            this.arenaId.text = "ID："+this.arena.getId();
            this.num.text = "<color=#23a006>" + this.arena.members + "</color>/" + this.arena.maxMembers;
            this.num.gameObject.SetActive(arena.getMaster() == Player.player.userid);
        }
    }
}
