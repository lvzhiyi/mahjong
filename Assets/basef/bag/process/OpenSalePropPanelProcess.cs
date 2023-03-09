using basef.prop;

namespace basef.bag
{
    public class OpenSalePropPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            BagPanel panel = getRoot<BagPanel>();
            Prop prop = panel.infoView.prop;
            PropUsePanel usePanle = UnrealRoot.root.getDisplayObject<PropUsePanel>();
            usePanle.setProp(prop,PropUsePanel.UseType.SALE);
            usePanle.refresh();
            usePanle.setCVisible(true);
        }
    }
}
