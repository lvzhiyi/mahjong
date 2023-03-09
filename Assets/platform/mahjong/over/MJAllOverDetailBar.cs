using cambrian.unreal.scroll;

namespace platform.mahjong
{
    public class MJAllOverDetailBar : ScrollBar
    {
        /// <summary>
        /// 一般状态
        /// </summary>
        private UnrealTextButton normalButton;
        /// <summary>
        /// 选中状态
        /// </summary>
        private UnrealTextButton selectButton;

        private bool isSeleced = false;
        /// <summary>
        /// 类型
        /// </summary>
        public int type;

        private string nickname;

        protected override void xinit()
        {
            this.normalButton = this.transform.Find("normal").GetComponent<UnrealTextButton>();
            this.normalButton.init();
            this.selectButton = this.transform.Find("selected").GetComponent<UnrealTextButton>();
            this.selectButton.init();
        }

        public void setData(int type, string name,int selecttype)
        {
            this.type = type;
            this.nickname = name;
            isSeleced = (type == selecttype);
        }

        protected override void xrefresh()
        {
            this.normalButton.setVisible(false);
            this.selectButton.setVisible(false);

            this.normalButton.text.text = nickname;
            this.selectButton.text.text = nickname;

            if (isSeleced)
                this.selectButton.setVisible(true);
            else
                this.normalButton.setVisible(true);
        }

        /// <summary>
        /// 设置是否选中
        /// </summary>
        /// <param name="selectsid"></param>
        public void setSelected(int type)
        {
            this.isSeleced = (this.type == type);
        }

       
    }
}
