using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    /// <summary>
    /// 封顶bar
    /// </summary>
    public class FengDingBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 几番封顶
        /// </summary>
        private Text text;

        private Image selected;


        [HideInInspector] public int fanshu;

        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
            this.selected = this.transform.Find("normal").GetComponent<Image>();
        }

        private string fanshutext;
        public void setData(int fanshu, bool ischeck)
        {
            this.fanshutext= fanshu + "番";

            if (ischeck)
            {
                this.text.text = "<color=#c34a3d>" + fanshutext + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + fanshutext + "</color>";
            }
            
            this.selected.gameObject.SetActive(ischeck);
            this.fanshu = fanshu;
        }

        public void checkedImg(bool b)
        {
            if (b)
            {
                this.text.text = "<color=#c34a3d>" + fanshutext + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + fanshutext + "</color>";
            }
            this.selected.gameObject.SetActive(b);
        }
    }
}

