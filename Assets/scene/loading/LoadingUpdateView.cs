using UnityEngine.UI;

namespace scene.loading
{
    /// <summary>
    /// 更新视图
    /// </summary>
    public class LoadingUpdateView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 进度条
        /// </summary>
        UnrealProgressBar bar;

        Text text;

        protected override void xinit()
        {
            this.bar = this.transform.Find("progress").GetComponent<UnrealProgressBar>();
            this.bar.init();
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            this.bar.setValue(0);
            this.text.text = "";
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        /// <param name="value"></param>
        public void setProgress(float value)
        {
            this.bar.setValue(value);
        }

        /// <summary>
        /// 设置文本信息
        /// </summary>
        /// <param name="str"></param>
        public void setText(string str)
        {
            this.text.text = str;
        }
    }
}
