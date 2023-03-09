using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 单独设置规则比例
    /// </summary>
    public class ArenaAloneSetRuleRadioPanel:UnrealLuaPanel
    {
        private ScrollTableContainer container;

        [HideInInspector] public RebateList list;


        private RebateList selfList;

        /// <summary>
        /// 是否是独立设置
        /// </summary>
        private Image img;

        public long dest;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("container").GetComponent<ScrollTableContainer>();
            container.init();
            img = content.Find("content").Find("minus").Find("img").GetComponent<Image>();
        }

        public void setRebate(RebateList list,RebateList selflist,long dest)
        {
            this.list = list;
            this.selfList = selflist;
            this.dest = dest;
        }

        public void setRebates(RebateList list)
        {
            this.list = list;
        }

        public void refreshImage()
        {
            img.gameObject.SetActive(list.special);
        }

        public void refreshSpeicial()
        {
            container.resize(list.list.count);
        }

        protected override void xrefresh()
        {
            refreshImage();
            container.updateData(back);
            container.resize(list.list.count);
        }

        public void back(ScorllTableBar bar,int index)
        {
            ArenaAloneSetRuleRadioBar radio=(ArenaAloneSetRuleRadioBar)bar;
            radio.setData(list.list.get(index),selfList.list.get(index),list.special);
            radio.index_space = index;
            radio.refresh();
        }
    }
}
