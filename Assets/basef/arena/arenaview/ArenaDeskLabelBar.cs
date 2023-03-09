using basef.rule;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场标签
    /// </summary>
    public class ArenaDeskLabelBar:UnrealLuaSpaceObject
    {
        Rule rule;

        private UnrealTextField selectedText;

        private Text normal;

        protected override void xinit()
        {
            selectedText = transform.Find("select").GetComponent<UnrealTextField>();
            selectedText.init();
            normal = transform.Find("normal").GetComponent<Text>();
        }

        public void setRule(Rule rule)
        {
            this.rule = rule;
        }

        public Rule getRule()
        {
            return rule;
        }

        protected override void xrefresh()
        {
            if (rule!=null)
            {
                normal.text = rule.name;
                selectedText.text = rule.name;
            }
            else
            {
                selectedText.text = "全部规则";
                normal.text = "全部规则";
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public void selected(bool b)
        {
            selectedText.setVisible(b);
            normal.gameObject.SetActive(!b);
        }
    }
}
