using cambrian.uui;
using UnityEngine.UI;

namespace basef.rank
{
    public class RankTypeBar : UnrealLuaSpaceObject
    {
        private SpritesList bg;

        private Text normal;

        private Text actived;

        public string typename;

        public int rankType;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool isSelected;

        protected override void xinit()
        {
            this.bg = this.transform.Find("bg").GetComponent<SpritesList>();
            this.normal = this.transform.Find("normal").GetComponent<Text>();
            this.actived = this.transform.Find("actived").GetComponent<Text>();
        }

        public void select(bool isSelected)
        {
            this.isSelected = isSelected;
        }

        protected override void xrefresh()
        {
            this.normal.text = typename;
            this.normal.gameObject.SetActive(false);
            this.actived.text = typename;
            this.actived.gameObject.SetActive(false);
            if (isSelected)
            {
                this.bg.ShowIndex(1,false);
                this.actived.gameObject.SetActive(true);
            }
            else
            {
                this.bg.ShowIndex(0,false);
                this.normal.gameObject.SetActive(true);
            }
        }
    }
}
