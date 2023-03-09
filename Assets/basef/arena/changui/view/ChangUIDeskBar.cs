using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.arena
{
    public class ChangUIDeskBar : ScorllTableBar
    {
        /// <summary>
        /// 选中图标
        /// </summary>
        Transform selected;
        /// <summary>
        /// 名称
        /// </summary>
        UnrealTextField nameText;

        SpritesList tableBg;
        SpritesList bd1;
        SpritesList bd2;
        SpritesList bd3;
        SpritesList bd4;

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool isSelect;
        /// <summary>
        /// 名称
        /// </summary>
        string deskname;

        protected override void xinit()
        {
            selected = this.transform.Find("selected");
            nameText = this.transform.Find("name").GetComponent<UnrealTextField>();
            tableBg = this.transform.Find("table").GetComponent<SpritesList>();
            bd1 = this.transform.Find("bd1").GetComponent<SpritesList>();
            bd2 = this.transform.Find("bd2").GetComponent<SpritesList>();
            bd3 = this.transform.Find("bd3").GetComponent<SpritesList>();
            bd4 = this.transform.Find("bd4").GetComponent<SpritesList>();
        }

        public void setData(int type,string name)
        {
            this.isSelect = type==index_space;
            this.deskname = name;
        }

        protected override void xrefresh()
        {
            selected.gameObject.SetActive(isSelect);
            nameText.text = deskname;
            tableBg.ShowIndex(index_space);
            bd1.ShowIndex(index_space);
            bd2.ShowIndex(index_space);
            bd3.ShowIndex(index_space);
            bd4.ShowIndex(index_space);
        }
    }
}
