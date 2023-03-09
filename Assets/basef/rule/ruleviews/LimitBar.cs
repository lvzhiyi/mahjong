using UnityEngine.UI;

namespace basef.rule
{
    /// <summary>
    /// 限制bar
    /// </summary>
    public class LimitBar : UnrealLuaSpaceObject
    {
        private Image img;

        private Text text;

        public bool status;

        public bool isLocked;

        private Image lockicon;

        protected override void xinit()
        {
            this.img = this.transform.Find("normal").GetComponent<Image>();
            this.text = this.transform.Find("text").GetComponent<Text>();
            if (transform.Find("lock") != null)
                lockicon=transform.Find("lock").GetComponent<Image>();
        }

        private string content;

        public void setData(string content, bool ischeck,bool islock)
        {
            this.content = content;
            if (ischeck)
            {
                this.text.text = "<color=#c34a3d>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + content + "</color>";
            }
            this.img.gameObject.SetActive(ischeck);
            this.status = ischeck;
            this.isLocked = islock;
            if (lockicon != null)
            {
                lockicon.gameObject.SetActive(ischeck&&islock);
            }
        }

        public void checkedImg(bool b)
        {
            this.img.gameObject.SetActive(b);
            this.status = b;
            if (b)
            {
                this.text.text = "<color=#c34a3d>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + content + "</color>";
            }
        }

    }
}
