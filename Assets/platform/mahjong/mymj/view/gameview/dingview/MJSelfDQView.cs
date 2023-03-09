using cambrian.unreal.display;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 自己的定缺视图
    /// </summary>
    public class MJSelfDQView : UnrealLuaSpaceObject
    {
        UnrealSpriteAnimation tong;

        UnrealSpriteAnimation tiao;

        UnrealSpriteAnimation wan;

        /// <summary>
        /// 推荐定缺的类型
        /// </summary>
        int[] distypes;

        /// <summary>
        /// 已定缺
        /// </summary>
        private Image bg;

        UnrealLuaSpaceObject dqview;

        protected override void xinit()
        {
            if (transform.Find("dqview") != null)
            {
                dqview = transform.Find("dqview").GetComponent<UnrealLuaSpaceObject>();
                tong = dqview.transform.Find("tong").Find("bg").GetComponent<UnrealSpriteAnimation>();
                tiao = dqview.transform.Find("tiao").Find("bg").GetComponent<UnrealSpriteAnimation>();
                wan = dqview.transform.Find("wan").Find("bg").GetComponent<UnrealSpriteAnimation>();
            }

            bg = transform.Find("bg").GetComponent<Image>();

        }

        public void setDisTypes(int[] distypes)
        {
            this.distypes = distypes;
        }

        public void showDQOver()
        {
            bg.gameObject.SetActive(true);
            if (dqview != null)
                dqview.setVisible(false);
        }

        /// <summary>
        /// 具体推荐的牌，需要走单独的方法
        /// </summary>
        public void showDq()
        {
            bg.gameObject.SetActive(false);
            if (dqview != null)
                dqview.setVisible(true);
        }

        protected override void xrefresh()
        {

            if (distypes == null || dqview == null) return;
            showDq();
            tong.gameObject.SetActive(false);
            tiao.gameObject.SetActive(false);
            wan.gameObject.SetActive(false);

            for (int i = 0; i < distypes.Length; i++)
            {
                if (distypes[i] == MJCard.TYPE_DOT)
                {
                    tong.gameObject.SetActive(true);
                }
                else if (distypes[i] == MJCard.TYPE_BAM)
                {
                    tiao.gameObject.SetActive(true);
                }
                else if (distypes[i] == MJCard.TYPE_CHA)
                {
                    wan.gameObject.SetActive(true);
                }
            }
        }
    }
}
