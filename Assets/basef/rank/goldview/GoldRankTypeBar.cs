using UnityEngine;

namespace basef.rank
{
    public class GoldRankTypeBar:UnrealLuaSpaceObject
    {
        private UnrealTextScaleButton normal;

        private UnrealTextScaleButton selected;
        /// <summary>
        /// 单个组
        /// </summary>
        [HideInInspector] public GoldRankGroup group;

        /// <summary>
        /// 是否选中
        /// </summary>
        private bool isSelected;

        protected override void xinit()
        {
            this.normal = this.transform.Find("normal").GetComponent<UnrealTextScaleButton>();
            this.selected = this.transform.Find("selected").GetComponent<UnrealTextScaleButton>();
        }

        public void setGoldRandGroup(GoldRankGroup group,bool isSelected)
        {
            this.group = group;
            this.isSelected = isSelected;
        }

        protected override void xrefresh()
        {
            normal.text = this.group.name;
            selected.text = this.group.name;
            normal.setVisible(false);
            selected.setVisible(false);
            if(isSelected)
                selected.setVisible(true);
            else
                normal.setVisible(true);
        }
    }
}
