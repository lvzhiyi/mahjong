namespace platform.spotred.room
{
    public class SelectCheckBoxSlipCardProcess:MouseClickProcess
    {
        public override void execute()
        {
            SlipCardBar bar=GetComponent<SlipCardBar>();
            bool b=this.getRoot<SelectSlipCardPanel>().isCanSelect(bar.card,bar.count);
            if (b) this.getRoot<SelectSlipCardPanel>().setSelected(bar.index_space,bar.card,bar.count,!bar.isselected);
            else
                Alert.show("牌堆里的牌不足,当前剩余："+this.getRoot<SelectSlipCardPanel>().cardnum+"张");
        }
    }
}
