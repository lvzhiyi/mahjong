using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    public class RuleInfoSingleBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 内容
        /// </summary>
        private Text text;
        /// <summary>
        /// 是否选中
        /// </summary>
        private Image selected;
        /// <summary>
        /// 值
        /// </summary>
        [HideInInspector] public int value;

        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
            this.selected = this.transform.Find("normal").GetComponent<Image>();
        }

        private string content;
        public void setData(int value,string content, bool ischeck)
        {
            this.content = content;

            if (ischeck)
                this.text.text = "<color=#c34a3d>" + content + "</color>";
            else
                this.text.text = "<color=#865e16>" + content + "</color>";

            this.selected.gameObject.SetActive(ischeck);
            this.value = value;
        }


        public void checkedImg(bool b)
        {
            if (b)
            {
                this.text.text = "<color=#c34a3d>" + content + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + content + "</color>";
            }
            this.selected.gameObject.SetActive(b);
        }
    }
}
