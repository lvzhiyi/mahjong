using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJSelfPiaoView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 推荐定缺的类型
        /// </summary>
        protected int[] distypes;

        /// <summary>
        /// 已定缺
        /// </summary>
        protected Image bg;

        protected UnrealLuaSpaceObject piaoview;

        protected override void xinit()
        {
            if (transform.Find("piaoview") != null)
            {
                piaoview = transform.Find("piaoview").GetComponent<UnrealLuaSpaceObject>();
            }

            bg = transform.Find("bg").GetComponent<Image>();
        }

        public void showPiaoOver()
        {
            bg.gameObject.SetActive(true);
            if (piaoview != null)
                piaoview.setVisible(false);
        }

        /// <summary>
        /// 具体推荐的牌，需要走单独的方法
        /// </summary>
        public void showPiao()
        {
            bg.gameObject.SetActive(false);
            if (piaoview != null)
                piaoview.setVisible(true);
        }

        protected override void xrefresh()
        {
            if (piaoview == null) return;
            showPiao();
        }
    }
}
