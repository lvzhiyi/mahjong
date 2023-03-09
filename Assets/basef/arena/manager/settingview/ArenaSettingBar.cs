using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{

    public class ArenaSettingBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 内容
        /// </summary>
        private Text text;

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

        public void setData(int num, bool ischeck)
        {

//            if (ischeck)
//            {
//                this.text.text = "<color=#c34a3d>" + content + "</color>";
//            }
//            else
//            {
//                this.text.text = "<color=#865e16>" + content + "</color>";
//            }


            content = index_space == 0 ? "是" : "否";


            text.text = content;
            this.selected.gameObject.SetActive(ischeck);
            this.value = num;
        }

        public void checkedImg(bool b)
        {
            this.selected.gameObject.SetActive(b);
            text.text = content;
            //            if (b)
            //            {
            //                this.text.text = "<color=#c34a3d>" + content + "</color>";
            //            }
            //            else
            //            {
            //                this.text.text = "<color=#865e16>" + content + "</color>";
            //            }
        }
    }
}
