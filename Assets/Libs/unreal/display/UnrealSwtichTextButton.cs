using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.display
{
    [Hotfix]
    public class UnrealSwtichTextButton : UnrealButton
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        public Text text;

        /// <summary>
        /// 一般状态的文字颜色（16进制）
        /// </summary>
        public string normal_color = null;

        public int normal_fontsize = 0;

        /// <summary>
        /// 选中状态的文字颜色（16进制）
        /// </summary>
        public string activie_color = null;

        public int activie_fontsize = 0;

        /// <summary>
        /// 禁用状态的文字颜色（16进制）
        /// </summary>
        public string disable_color = null;

        public int disable_fontsize = 0;


        /// <summary>
        /// 阴影启用状态（norml,active,disable）
        /// </summary>
        public bool[] outlineEables = new bool[3];

        /// <summary>
        /// 描边
        /// </summary>
        private Outline outline;

        /// <summary>
        /// 阴影
        /// </summary>
        private Shadow shadow;


        public Outline getOutLine()
        {
            return outline;
        }

        /// <summary>
        /// 设置描边颜色
        /// </summary>
        /// <param name="outline"></param>
        public void setOutLineColor(string color)
        {
            if (outline != null)
                outline.effectColor = ColorKit.getColor(color);
        }

        /// <summary>
        /// 描边是否启用
        /// </summary>
        /// <param name="b"></param>
        public void outlineEnable(bool b)
        {
            if (outline != null)
            {
                outline.enabled = b;
            }
        }



        public Shadow getShadow()
        {
            return shadow;
        }

        /// <summary>
        /// 设置阴影颜色
        /// </summary>
        /// <param name="outline"></param>
        public void setShadowColor(string color)
        {
            if (shadow != null)
                shadow.effectColor = ColorKit.getColor(color);
        }

        /// <summary>
        /// 阴影是否启用
        /// </summary>
        /// <param name="b"></param>
        public void shadowEnable(bool b)
        {
            if (shadow != null)
            {
                shadow.enabled = b;
            }
        }

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("text") != null)
            {
                this.text = this.transform.Find("text").GetComponent<Text>();
                this.outline = this.text.GetComponent<Outline>();
                this.shadow = this.text.GetComponent<Shadow>();
            }
        }

        public void outlineActive(int state)
        {
            switch (state)
            {
                case ACTIVED:
                    outlineEnable(outlineEables[1]);
                    break;
                case DISABLE:
                    outlineEnable(outlineEables[2]);
                    break;
                case NORMAL:
                    outlineEnable(outlineEables[0]);
                    break;
                default:
                    break;
            }
        }


        public override void setState(int state)
        {
           
            if (this.text != null)
            {
                switch (state)
                {
                    case ACTIVED:
                        if (activie_color != null&&!("".Equals(activie_color)))
                            this.text.color = ColorKit.getColor(activie_color);
                        if (activie_fontsize != 0)
                            this.text.fontSize = activie_fontsize;
                        break;
                    case DISABLE:
                        if (disable_color != null && !("".Equals(disable_color)))
                        {
                            this.text.color = ColorKit.getColor(disable_color);
                        }
                        if (disable_fontsize != 0)
                            this.text.fontSize = disable_fontsize;
                        break;
                    case NORMAL:
                        if (normal_color != null && !("".Equals(normal_color)))
                        {
                            this.text.color = ColorKit.getColor(normal_color);
                        }
                        if (normal_fontsize != 0)
                            this.text.fontSize = normal_fontsize;
                        break;
                    default:
                        break;
                }
            }
            outlineActive(state);
            base.setState(state);
        }
    }
}
