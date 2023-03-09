using scene.game;
using platform.spotred;
using UnityEngine.UI;

namespace platform.spotred.playback
{
    public class ReplayHandCardBar : UnrealLuaSpaceObject
    {


        /// <summary>
        /// 牌图标
        /// </summary>
        Image card_img;

        /// <summary>
        /// 牌
        /// </summary>
        int card;

        /// <summary>
        /// 位置
        /// </summary>
        public int postion;

        /// <summary>
        /// 数量
        /// </summary>
        public int count;

        protected override void xinit()
        {
            this.card_img = transform.Find("card").GetComponent<Image>();

        }

        public void setData(int card, int count)
        {
            this.card = card;
            this.count = count;
        }


        protected override void xrefresh()
        {
            this.card_img.sprite = WidgetManager.getInstance().getShowCard(card);

            int y = (count - index_space)*30;


            if (postion == ReplayHandColumBar.RIGHT || postion == ReplayHandColumBar.LEFT)
                DisplayKit.setLocalRoateXYZ(this.transform, 0, 0, 90);

            switch (postion)
            {
                case ReplayHandColumBar.BOTTOM:
                    //下
                    DisplayKit.setLocalXY(this.transform, 0, y);
                    break;
                case ReplayHandColumBar.RIGHT:
                    //右
                    DisplayKit.setLocalXY(this.transform, -y, 0);
                    break;
                case ReplayHandColumBar.TOP:
                    //上
                    DisplayKit.setLocalXY(this.transform, 0, -y);
                    break;
                case ReplayHandColumBar.LEFT:
                    //左
                    DisplayKit.setLocalXY(this.transform, y, 0);
                    break;
            }
        }
    }
}
