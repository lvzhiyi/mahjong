using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    public class JueShuBar: UnrealLuaSpaceObject
    {
        /// <summary>
        /// 几局
        /// </summary>
        private Text text;

        private Image selected;

        /// <summary>
        /// 局数
        /// </summary>
        [HideInInspector] public int num;

        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
            this.selected = this.transform.Find("normal").GetComponent<Image>();
        }

        private string jushu;
        public void setData(int num,int card_num, bool ischeck)
        {
            jushu = num + "局<size=20>(" + card_num + "元宝)</size>";
            if (ischeck)
            {
                this.text.text = "<color=#c34a3d>" + jushu + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + jushu + "</color>";
            }
            this.selected.gameObject.SetActive(ischeck);
            this.num = num;
        }

        public void setDatas(string c, bool ischeck)
        {
            this.jushu = c;
            if (ischeck)
            {
                this.text.text = "<color=#c34a3d>" + jushu + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + jushu + "</color>";
            }
            this.selected.gameObject.SetActive(ischeck);
        }

        public void checkedImg(bool b)
        {
            this.selected.gameObject.SetActive(b);
            if (b)
            {
                this.text.text = "<color=#c34a3d>" + jushu + "</color>";
            }
            else
            {
                this.text.text = "<color=#865e16>" + jushu + "</color>";
            }
        }
    }
}
