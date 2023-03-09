using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.display
{
    /// <summary>
    /// 支持两种文字图片切换
    /// </summary>
    [Hotfix]
    public class UnrealSwtichTwoButton : UnrealButton
    {
        private Transform actived_icon;

        /// <summary>
        /// 文本消息
        /// </summary>
        public Text text;

        /// <summary>
        /// 一般状态的文字颜色（16进制）
        /// </summary>
        public string normal_color = null;

        /// <summary>
        /// 选中状态的文字颜色（16进制）
        /// </summary>
        public string activie_color = null;

        /// <summary>
        /// 禁用状态的文字颜色（16进制）
        /// </summary>
        public string disable_color = null;


        protected override void xinit()
        {
            base.xinit();
            this.actived_icon = this.transform.Find("actived_icon");
            if(this.transform.Find("text")!=null)
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        public override void setState(int state)
        {
            base.setState(state);

            if (this.text != null)
            {
                switch (state)
                {
                    case ACTIVED:
                        if (activie_color != null && !("".Equals(activie_color)))
                            this.text.color = ColorKit.getColor(activie_color);
                        break;
                    case DISABLE:
                        if (disable_color != null && !("".Equals(disable_color)))
                            this.text.color = ColorKit.getColor(disable_color);
                        break;
                    case NORMAL:
                        if (normal_color != null && !("".Equals(normal_color)))
                            this.text.color = ColorKit.getColor(normal_color);
                        break;
                    default:
                        break;
                }
            }


            if (this.actived_icon == null) return;
            switch (state)
            {
                case ACTIVED:
                    this.actived_icon.gameObject.SetActive(true);
                    break;
                case DISABLE:
                    this.actived_icon.gameObject.SetActive(false);
                    break;
                case NORMAL:
                    this.actived_icon.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
