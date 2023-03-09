namespace basef.bag
{
    public class BagSelectPropProcess : MouseClickProcess
    {
        public override void execute()
        {
            BagBar bar = GetComponent<BagBar>();
            if (bar.prop == null) return;
            BagPanel panel = getRoot<BagPanel>();
            panel.select(bar.index_space);
            if (bar.prop != null)
            {
                panel.infoView.setProp(bar.prop);
                panel.infoView.refresh();
            }
        }
    }
}
