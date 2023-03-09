using basef.prop;

namespace basef.bag
{
    /// <summary> 道具使用成功 </summary>
    public class PropUseSuccessPanel:UnrealLuaPanel
    {
        UnrealDivTableContainer container;

        Prop[] props;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.content.Find("content").Find("mask").Find("container").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        public void setProps(Prop[] props)
        {
            this.props = props;
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            this.container.resize(props.Length);

            for (int i=0;i< props.Length; i++)
            {
                PropUseSuccessBar bar= (PropUseSuccessBar)this.container.bars[i];
                bar.index_space = i;
                bar.setTotal(props.Length);
                bar.setProp(props[i]);
                bar.refresh();
            }

            this.container.resizeDelta();
        }
    }
}
