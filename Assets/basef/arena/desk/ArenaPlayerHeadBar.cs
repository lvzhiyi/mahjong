using basef.player;
using cambrian.unreal.display;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaPlayerHeadBar : UnrealLuaSpaceObject
    {
        SimplePlayer player;

        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView headView;

        /// <summary>
        /// 默认头像
        /// </summary>
        protected UnrealUICircle head;
        /// <summary>
        /// 头像显示的图片
        /// </summary>
        protected UnrealUICircle head_1;
        /// <summary>
        /// 头像背景
        /// </summary>
        public Image bg;

        protected override void xinit()
        {

            this.headView = transform.GetComponent<PlayerCircleHeadView>();
            this.headView.init();
            this.head = transform.Find("head").GetComponent<UnrealUICircle>();
            this.head_1 = transform.Find("head_1").GetComponent<UnrealUICircle>();
            this.bg = transform.Find("head_kuang").GetComponent<Image>();
        }

        public void setMatchPlayer(SimplePlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            head.gameObject.SetActive(player != null);
            head_1.gameObject.SetActive(player != null);
            bg.gameObject.SetActive(player != null);

            if (player != null)
            {
                headView.setData(player.head, player.userid);
                headView.refresh();
            }
        }
    }
}
