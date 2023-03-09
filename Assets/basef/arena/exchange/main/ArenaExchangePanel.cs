namespace basef.arena
{
    public class ArenaExchange
    {
        /// <summary> 比赛奖品 </summary>
        public const int Prize_Match = 0;
        /// <summary> 实物奖品 </summary>
        public const int Prize_Physical = 1;
    }

    /// <summary> 兑换界面 </summary>
    public class ArenaExchangePanel : UnrealLuaPanel
    {
        /// <summary> 页面选择 </summary>
        ArenaExchangeInstTypeView instview;

        /// <summary> 按钮显示 </summary>
        ArenaExchangeBtnTypeView btnview;

        /// <summary> 内容显示 </summary>
        ArenaExchangeContentView contentview;

        protected override void xinit()
        {
            base.xinit();
            instview = this.content.Find("centers").Find("instview").GetComponent<ArenaExchangeInstTypeView>();
            instview.init();

            btnview = this.content.Find("centers").Find("btnview").GetComponent<ArenaExchangeBtnTypeView>();
            btnview.init();

            contentview = this.content.Find("centers").Find("contentview").GetComponent<ArenaExchangeContentView>();
            contentview.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            contentview.refresh();
            instview.refresh();
            btnview.refresh();
        }

        public void showView(int index,object obj = null)
        {
            instview.setIndex(index);
            contentview.setData(index,obj);
            btnview.setIndex(index);
            xrefresh();
        }
    }
}
