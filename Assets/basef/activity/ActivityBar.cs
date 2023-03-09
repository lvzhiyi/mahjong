using UnityEngine;
using UnityEngine.UI;

namespace basef.activity
{
    public class ActivityBar : UnrealLuaSpaceObject
    {
        [HideInInspector] public UnrealCheckBox btn;

        private Activity activity;

        private Image diandian;

        private Text activetext;

        private Text normaltext;

        protected override void xinit()
        {
            this.btn = this.transform.Find("checkbox").GetComponent<UnrealCheckBox>();
            this.btn.init();
            this.diandian = this.transform.Find("weidu").GetComponent<Image>();
            this.activetext = this.transform.Find("text_1").GetComponent<Text>();
            this.normaltext = this.transform.Find("text").GetComponent<Text>();
        }

        public void setActivity(Activity activity)
        {
            this.activity = activity;
        }

        public Activity getActivity()
        {
            return activity;
        }

        protected override void xrefresh()
        {
            this.activetext.text = this.activity.getTitle();
            this.normaltext.text = this.activity.getTitle();
            this.activetext.gameObject.SetActive(false);
            this.normaltext.gameObject.SetActive(false);

            if (index_space == 0) this.diandian.gameObject.SetActive(false);
        }

        public void check(bool b)
        {
            btn.setState(b);
            this.activetext.gameObject.SetActive(b);
            this.normaltext.gameObject.SetActive(!b);
        }

        public void hideDianDian()
        {
            this.diandian.gameObject.SetActive(false);
        }
    }
}
