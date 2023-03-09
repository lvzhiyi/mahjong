using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.arena
{
    public class ChangUIDeskBgBar : ScorllTableBar
    {
        /// <summary>
        /// 桌面图片
        /// </summary>
        SpritesList deskbg;
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
        string deskbgname;

        protected override void xinit()
        {
            deskbg = this.transform.Find("deskbg").GetComponent<SpritesList>();
            selected = this.transform.Find("selected");
            nameText = this.transform.Find("name").GetComponent<UnrealTextField>();
        }

        public void setData(int select,string name)
        {
            this.isSelect = index_space == select;
            this.deskbgname = name;
        }

        public void updateSelect(bool isSelect)
        {
            this.isSelect = isSelect;
            selected.gameObject.SetActive(isSelect);
        }

        protected override void xrefresh()
        {
            deskbg.ShowIndex(index_space, false);
            selected.gameObject.SetActive(isSelect);
            nameText.text = deskbgname;
        }
    }
}
