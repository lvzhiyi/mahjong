namespace basef.arena
{
    /// <summary> 竞赛场 报名 </summary>
    public class ArenaSignUpPanel : UnrealLuaPanel
    {
        UnrealTableContainer container;

        int[] count = new int[] { 50,100,200,500,1000 };

        protected override void xinit()
        {
            base.xinit();
            container = this.content.Find("centers").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            container.resize(count.Length);
            for (int i = 0; i < count.Length; i++)
            {
                ArenaSignUpBar bar = (ArenaSignUpBar)container.bars[i];
                bar.setData(count[i]);
            }
        }
    }
}
