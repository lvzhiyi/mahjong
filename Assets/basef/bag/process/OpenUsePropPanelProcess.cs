using basef.prop;

namespace basef.bag
{
    public class OpenUsePropPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            BagPanel panel= getRoot<BagPanel>();
            Prop prop= panel.infoView.prop;
            PropUsePanel usePanle= UnrealRoot.root.getDisplayObject<PropUsePanel>();
            usePanle.setProp(prop,PropUsePanel.UseType.USE);
            usePanle.refresh();
            usePanle.setCVisible(true);
        }
    }
}
