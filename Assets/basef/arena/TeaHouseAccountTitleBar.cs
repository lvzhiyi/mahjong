using UnityEngine.UI;

namespace basef.teahouse
{
    public class TeaHouseAccountTitleBar:IdBar
    {
        private Text text_normal;
        private Text text_active;
        protected override void xinit()
        {
            base.xinit();
            this.text_normal = this.transform.Find("text_normal").GetComponent<Text>();
            this.text_active = this.transform.Find("text_active").GetComponent<Text>();
        }

        public void setText(string title)
        {
            this.text_active.text = title;
            this.text_normal.text = title;
        }

        protected override void xrefresh()
        {
            if (index_space == 0)
            {
                this.text_active.gameObject.SetActive(true);
                this.text_normal.gameObject.SetActive(false);
            }
            else
            {
                this.text_active.gameObject.SetActive(false);
                this.text_normal.gameObject.SetActive(true);
            }
        }

        public override void setState(bool state)
        {
            base.setState(state);
            if (state)
            {
                this.text_active.gameObject.SetActive(true);
                this.text_normal.gameObject.SetActive(false);
            }
            else
            {
                this.text_normal.gameObject.SetActive(true);
                this.text_active.gameObject.SetActive(false);
            }
        }
    }
}
