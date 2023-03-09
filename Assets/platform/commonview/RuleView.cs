namespace platform
{
    /// <summary>
    /// 规则view
    /// </summary>
    public class RuleView : UnrealLuaSpaceObject
    {
        private UnrealTableContainer container;

        protected override void xinit()
        {
            this.container = this.transform.Find("content").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
        }


        protected override void xrefresh()
        {
            string[] info = Room.room.getRule().getRuleInfo();
            int len = info.Length;
            if (len > 10)
                container.cols = 2;
            else
                container.cols = 1;
            this.container.resize(len + 1);
            for (int i = 0; i < len + 1; i++)
            {
                RuleViewBar bar = (RuleViewBar)this.container.bars[i];
                if (i == info.Length)
                {
                    bar.setText("");
                }
                else
                {
                    bar.setText(info[i]);
                }

            }
            this.container.resizeDelta();
            container.relayout();
        }
    }
}
