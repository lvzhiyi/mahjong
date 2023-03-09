using cambrian.unreal.display;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class CardBoxBar:UnrealLuaSpaceObject
    {
        private Image img;

        private int card;

        protected override void xinit()
        {
            this.img = this.transform.Find("bg").GetComponent<Image>();
        }


        public void setCard(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
           DisplayKit.setLocalXY(this.transform,0,index_space*2);  
        }
    }
}
