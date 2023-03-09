using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 其他设置
    /// </summary>
    public class ArenaSettingOtherBar : UnrealLuaSpaceObject
    {
        private Image img;

        private Text text;

        public bool status;

        protected override void xinit()
        {
            this.img = this.transform.Find("normal").GetComponent<Image>();
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        private string content;

        public void setData(string content, bool ischeck)
        {
            this.content = content;
            if (ischeck)
            {
                this.text.text = "<color=#1A71A5>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#A06A38>" + content + "</color>";
            }
            this.img.gameObject.SetActive(ischeck);
            this.status = ischeck;
        }

        public void checkedImg(bool b)
        {
            this.img.gameObject.SetActive(b);
            this.status = b;
            if (b)
            {
                this.text.text = "<color=#1A71A5>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#A06A38>" + content + "</color>";
            }
        }

    }
}
