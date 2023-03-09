using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaChangUITitleTypeBar : ScrollBar
    {
        /// <summary> 一般状态 </summary>
        private UnrealTextButton normalButton;

        /// <summary> 选中状态 </summary>
        private UnrealTextButton selectButton;

        /// <summary> 是否被选中 </summary>
        private bool isSeleced = false;

        private string nickname;

        protected override void xinit()
        {
            this.normalButton = this.transform.Find("normal").GetComponent<UnrealTextButton>();
            this.normalButton.init();
            this.selectButton = this.transform.Find("selected").GetComponent<UnrealTextButton>();
            this.selectButton.init();
        }

        public void setData(string name,int selecttype)
        {
            this.nickname = name;
            this.isSeleced = (index_space == selecttype);
        }

        protected override void xrefresh()
        {
            this.normalButton.setVisible(false);
            this.selectButton.setVisible(false);

            this.normalButton.text.text = nickname;
            this.selectButton.text.text = nickname;

            this.selectButton.setVisible(isSeleced);
            this.normalButton.setVisible(!isSeleced);
        }

        /// <summary> 设置是否选中 </summary>
        public void setSelected(int selecttype)
        {
            this.isSeleced = index_space == selecttype;
            xrefresh();
        }
    }
}
