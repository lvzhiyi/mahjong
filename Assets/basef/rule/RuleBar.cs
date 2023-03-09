using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    public class RuleBar: UnrealLuaSpaceObject
    {
        private Rule rule;

        private Image add_new;

        private UnrealCheckBox checkbox;
        
        protected Text txtchecked;

        protected Text txtnormal;
        /// <summary>
        /// 免费图标
        /// </summary>
        private Image free;
        /// <summary>
        /// 免费图标位置
        /// </summary>
        private Vector3 freepos;

        protected override void xinit()
        {
            this.add_new = this.transform.Find("newadd").GetComponent<Image>();
            this.checkbox = this.transform.Find("checkbox").GetComponent<UnrealCheckBox>();

            this.txtchecked = this.transform.Find("text_1").GetComponent<Text>();
            this.txtnormal = this.transform.Find("text").GetComponent<Text>();

            this.free = this.transform.Find("free").GetComponent<Image>();
            this.freepos = free.transform.localPosition;
        }


        public void setRule(Rule rule)
        {
            this.rule = rule;
        }

        public Rule getRule()
        {
            return this.rule;
        }

        /// <summary>
        /// 选中
        /// </summary>
        public void selected()
        {
            this.checkbox.setState(UnrealCheckObject.ACTIVED);
            this.txtchecked.gameObject.SetActive(true);
            this.txtnormal.gameObject.SetActive(false);
            freepos.y = 4;
            if (free != null)
                free.transform.localPosition = freepos;
        }

        public void selectNormal()
        {
            this.checkbox.setState(UnrealCheckObject.NORMAL);
            this.txtchecked.gameObject.SetActive(false);
            this.txtnormal.gameObject.SetActive(true);
            if (free != null)
                free.transform.localPosition = freepos;
        }

        protected override void xrefresh()
        {
            txtchecked.text = rule.name;
            txtnormal.text = rule.name;

            txtnormal.gameObject.SetActive(true);
            txtchecked.gameObject.SetActive(false);

            if (free != null)
                free.gameObject.SetActive(rule.isFree);

            this.add_new.gameObject.SetActive(rule.newadd);

            checkbox.setState(UnrealCheckObject.NORMAL);
        }
    }
}
