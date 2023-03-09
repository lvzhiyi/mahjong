using System.Text;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场玩法显示bar
    /// </summary>
    public class ArenaRuleWFShowBar : ScrollBar
    {
        private Image normal;
        private Text text;
        public ArenaLockRule lockRule;
        public bool isSelect;
        protected override void xinit()
        {
            normal = transform.Find("normal").GetComponent<Image>();
            text = transform.Find("text").GetComponent<Text>();
        }

        public void setData(ArenaLockRule lockRule)
        {
            this.lockRule = lockRule;
        }

        protected override void xrefresh()
        {
            text.text = lockRule.name;
        }
        public void isSelected(bool b)
        {
            this.isSelect = b;
            this.normal.gameObject.SetActive(b);
        }
    }
}