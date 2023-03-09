using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.arena
{
    public class ChangUIBgBar : ScorllTableBar
    {
        /// <summary>
        /// 背景图片
        /// </summary>
        SpritesList bg;
        /// <summary>
        /// 选中图标
        /// </summary>
        Transform selected;
        /// <summary>
        /// 名称
        /// </summary>
        UnrealTextField nameText;

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool isSelect;
        /// <summary>
        /// 名称
        /// </summary>
        string bgname;

        protected override void xinit()
        {
            bg = this.transform.Find("bg").GetComponent<SpritesList>();
            selected = this.transform.Find("selected");
            nameText = this.transform.Find("name").GetComponent<UnrealTextField>();
        }

        public void setData(int type,string name)
        {
            this.isSelect = index_space==type;
            this.bgname = name;
        }

        public void updateSelect(bool isSelect)
        {
            this.isSelect = isSelect;
            selected.gameObject.SetActive(isSelect);
        }

        protected override void xrefresh()
        {
            bg.ShowIndex(index_space,false);
            selected.gameObject.SetActive(isSelect);
            nameText.text = bgname;
        }
    }
}
