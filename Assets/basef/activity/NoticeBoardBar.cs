using UnityEngine;
using UnityEngine.UI;

namespace basef.activity
{
    public class NoticeBoardBar : UnrealLuaSpaceObject
    {
        private NoticeBoard notice;

        [HideInInspector] public UnrealCheckBox btn;

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

        public void setNotice(NoticeBoard notice)
        {
            this.notice = notice;
        }

        public NoticeBoard getNotcie()
        {
            return notice;
        }

        protected override void xrefresh()
        {
            this.activetext.text = this.notice.getName();
            this.normaltext.text = this.notice.getName();
            this.activetext.gameObject.SetActive(false);
            this.normaltext.gameObject.SetActive(false);

            if (index_space == 0) this.diandian.gameObject.SetActive(false);
        }

        public void check(bool b)
        {
            btn.setState(b);
            this.activetext.gameObject.SetActive(b);
            this.normaltext.gameObject.SetActive(!b);
            if (b) hideDianDian();
        }

        public void hideDianDian()
        {
            this.diandian.gameObject.SetActive(false);
        }
    }
}
