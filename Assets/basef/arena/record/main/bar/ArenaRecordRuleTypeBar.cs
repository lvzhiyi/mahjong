using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 赛场战绩 时间选择 Bar </summary>
    public class ArenaRecordRuleTypeBar : ScrollBar
    {
        /// <summary> 一般状态 </summary>
        private UnrealTextButton normalButton;

        /// <summary> 选中状态 </summary>
        private UnrealTextButton selectButton;

        /// <summary> 类型 </summary>
        [HideInInspector] public long time;

        /// <summary> 是否被选中 </summary>
        private bool isSeleced = false;

        private string nickname;

        protected override void xinit()
        {
            this.normalButton = this.transform.Find("normal").GetComponent<UnrealTextButton>();
            this.normalButton.init();
            this.selectButton = this.transform.Find("selected").GetComponent<UnrealTextButton>();
            this.selectButton.init();
        }

        public void setData(long time,long selecttime)
        {
            this.time = time;
            this.nickname = TimeKit.format(time,"MM月dd日");
            this.isSeleced = (time == selecttime);
        }

        protected override void xrefresh()
        {
            this.normalButton.setVisible(false);
            this.selectButton.setVisible(false);

            this.normalButton.text.text = nickname;
            this.selectButton.text.text = nickname;

            this.selectButton.setVisible(isSeleced);
            this.normalButton.setVisible(!isSeleced);
        }

        /// <summary> 设置是否选中 </summary>
        public void setSelected(long time)
        {
            this.isSeleced = (this.time == time);
            xrefresh();
        }
    }
}
