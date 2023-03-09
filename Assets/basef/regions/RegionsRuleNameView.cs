namespace basef.regions
{
    /// <summary>
    /// 规则名视图
    /// </summary>
    public class RegionsRuleNameView:UnrealLuaSpaceObject
    {
        private UnrealTableContainer container;


        private string[] rulename;

        public void setRuleName(string[] name)
        {
            this.rulename = name;
        }


        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("scrollrect").Find("content").GetComponent<UnrealTableContainer>();
            this.container.init();
        }

        protected override void xrefresh()
        {
            this.container.resize(rulename.Length);
            for (int i = 0; i < rulename.Length; i++)
            {
                RegionRuleBar bar= (RegionRuleBar)this.container.bars[i];
                bar.setText(rulename[i]);
            }
            this.container.resizeDelta();
        }
    }
}
