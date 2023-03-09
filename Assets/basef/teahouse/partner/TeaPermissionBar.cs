using UnityEngine.UI;

namespace basef.teahouse
{
    public class TeaPermissionBar:UnrealLuaSpaceObject
    {
        private Image img;

        private Text text;


        public bool status;
        /// <summary>
        /// 权限
        /// </summary>
        public int permission;

        protected override void xinit()
        {
            this.img = this.transform.Find("normal").GetComponent<Image>();
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        private string content;

        public void setData(string content, bool ischeck,int permission)
        {
            this.content = content;
            this.permission = permission;
            if (ischeck)
            {
                this.text.text = "<color=#1A71A5>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#9F6A38>" + content + "</color>";
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
                this.text.text = "<color=#9F6A38>" + content + "</color>";
            }
        }
    }
}
