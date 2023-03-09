using scene.game;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.regions
{
    public class RegionBar: ScrollBar
    {
        [HideInInspector] public Region region;
        /// <summary>
        /// 一般状态
        /// </summary>
        private UnrealTextButton normalButton;
        /// <summary>
        /// 选中状态
        /// </summary>
        private UnrealTextButton selectButton;

        private bool isSeleced = false;

        protected override void xinit()
        {
            this.normalButton = this.transform.Find("normal").GetComponent<UnrealTextButton>();
            this.selectButton = this.transform.Find("selected").GetComponent<UnrealTextButton>();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.normalButton.setVisible(false);
            this.selectButton.setVisible(false);

            

            if (isSeleced)
                this.selectButton.setVisible(true);
            else
                this.normalButton.setVisible(true);
        }

        public void setRegion(Region region, int selectid)
        {
            this.region = region;
            this.isSeleced = (region.id == selectid);
        }

        /// <summary>
        /// 设置是否选中
        /// </summary>
        /// <param name="selectsid"></param>
        public void setSelected(int selectid)
        {
            this.isSeleced = (region.id == selectid);
        }
    }
}
