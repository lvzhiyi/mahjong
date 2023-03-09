using scene.game;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 单局结算界面(手牌和胡的牌)
    /// </summary>
    public class SingleBar:UnrealLuaSpaceObject
    {
        private Image img;

        private int card;

        //固定牌的数量
        private int count;

        protected override void xinit()
        {
            this.img = transform.Find("bg").GetComponent<Image>();
        }

        protected override void xrefresh()
        {

            this.img.sprite = WidgetManager.getInstance().getJieSanShowCard(card);
            
            DisplayKit.setLocalXY(this.transform,getWidth()*(index_space+count),0);

        }


        public void setCard(int card,int count)
        {
            this.card = card;
            this.count = count;
        }
    }
}
