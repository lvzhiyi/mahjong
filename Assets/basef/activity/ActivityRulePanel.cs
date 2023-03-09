using UnityEngine;
using UnityEngine.UI;

namespace basef.activity
{
    /// <summary> 文本面板 </summary>
    public class ActivityRulePanel : UnrealLuaPanel
    {
        string value;

        UnrealContainer container;

        UnrealTextField text;

        private Text t;

        ResetTextRectTransform resetText;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("Canvas").Find("centers").Find("mask").Find("container").GetComponent<UnrealContainer>();
            this.container.init();
            this.text = this.container.transform.Find("text").GetComponent<UnrealTextField>();
            this.t = this.text.transform.Find("text").GetComponent<Text>();
            this.resetText = this.container.transform.Find("text").GetComponent<ResetTextRectTransform>();
            this.resetText.callback = this.relayoutContainer;
        }

        public void setValue(string value)
        {
            this.value = value;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.text.text = this.value;
        }

        public void relayoutContainer(Vector2 size)
        {
            Vector2 c = this.container.transform.GetComponent<RectTransform>().sizeDelta;
            Vector2 rest = this.resetText.GetComponent<RectTransform>().sizeDelta;
            if (c.y > rest.y) return;

            this.t.GetComponent<RectTransform>().sizeDelta = rest;

            this.container.transform.GetComponent<RectTransform>().sizeDelta = rest;
            Vector3 pos = this.container.transform.localPosition;
            this.container.transform.localPosition = new Vector3(pos.x,-rest.y / 2f,pos.z);
            this.container.drag.verticalNormalizedPosition = 1;
        }
    }
}
