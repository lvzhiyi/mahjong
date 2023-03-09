namespace basef.bin
{
    public class BinAccountDeleteProcess : MouseClickProcess
    {
        public override void execute()
        {
            string str = this.getRoot<BinAccountPanel>().text.text;
            char[] strs = str.ToCharArray();
            if (strs.Length == 0) return;
            if (strs.Length == 1)
            {
                this.getRoot<BinAccountPanel>().text.text = "";
                return;
            }
            str = "";
            for (int i = 0; i < strs.Length - 1; i++)
            {
                str += strs[i];
            }

            this.getRoot<BinAccountPanel>().text.text = str;
        }
    }
}
