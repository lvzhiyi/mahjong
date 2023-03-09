namespace basef.regions
{
    public class SelectRegionEvent : MouseClickProcess
    {
        public override void execute()
        {
            int id = this.transform.parent.GetComponent<RegionBar>().region.id;
            RegionsPanel panel = getRoot<RegionsPanel>();
            panel.regionsView.updateBars(id);
            panel.setRegionRuleName(id);
        }
    }
}
