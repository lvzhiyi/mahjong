namespace basef.bin
{
    public class BinAccountClearProcess : MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<BinAccountPanel>().text.text = "";
        }
    }
}
