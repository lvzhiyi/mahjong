using basef.player;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaChangTableUserBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 背景图片
        /// </summary>
        PlayerCircleHeadView head;
        /// <summary>
        /// 名称
        /// </summary>
        Image outgame;

        SimplePlayer player;

        protected override void xinit()
        {
            head = this.transform.Find("head").GetComponent<PlayerCircleHeadView>();
            head.init();
            outgame = this.transform.Find("outgame").GetComponent<Image>();
        }

        public void setData(SimplePlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            head.gameObject.SetActive(false);
            outgame.gameObject.SetActive(false);
            if (player != null)
            {
                head.setData(player.head, player.userid);
                head.refresh();
                head.gameObject.SetActive(true);
                outgame.gameObject.SetActive(!player.hasStatus(1));
            }
        }
    }
}
