using cambrian.game;
using basef.player;
using scene.game;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.teahouse
{
    public class TeaHousePlayerHeadBar : UnrealLuaSpaceObject
    {
        SimplePlayer player;
        /// <summary>
        /// 头像显示的图片
        /// </summary>
        protected Image head_1;
        /// <summary>
        /// 头像背景
        /// </summary>
        public Image bg;

        protected override void xinit()
        {
            this.head_1 = transform.Find("head").GetComponent<Image>();
            this.bg = transform.Find("head_kuang").GetComponent<Image>();
        }

        public void setMatchPlayer(SimplePlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            head_1.gameObject.SetActive(player != null);

            if (player != null)
            {
                if (player.head == null || player.head.Length <= 2)
                {
                    head_1.sprite = CacheManager.common[0];
                }
                else
                {
                    Sprite head_icon = IconManager.getHeadPic(player.userid);
                    if (head_icon != null)
                        head_1.sprite = head_icon;
                    else
                    {
                        Loader.getLoader()
                            .load<long, Sprite>(new Url(player.head), player.userid, this.refreshHeadIcon);
                    }
                }
            }

            if (player!=null&&IconManager.getHeadPic(player.userid) == null)
            {
                IconManager.saveHeadPic(player.userid, CacheManager.common[0]);
            }
        }

        void refreshHeadIcon(long userid, Sprite icon)
        {
            if (icon != null && userid > 0)
                IconManager.saveHeadPic(userid, icon);
            if (player != null && userid == player.userid && head_1 != null)
                head_1.sprite = icon;
        }
    }
}
