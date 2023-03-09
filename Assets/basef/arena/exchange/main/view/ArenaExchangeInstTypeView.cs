namespace basef.arena
{
    /// <summary> 兑换页面 选择 </summary>
    public class ArenaExchangeInstTypeView : UnrealLuaSpaceObject
    {
        UnrealRadioList context;

        int index = ArenaExchange.Prize_Match;

        string[] typeName = new string[] { "比赛奖品","实物奖品" };

        protected override void xinit()
        {
            context = this.transform.Find("content").GetComponent<UnrealRadioList>();
            context.init();

            setTypeListName();
        }

        private void setTypeListName()
        {
            context.resize(typeName.Length);
            ArenaExchangeTypeBtnBar bar = null;
            for (int i = 0; i < typeName.Length; i++)
            {
                bar = (ArenaExchangeTypeBtnBar)this.context.bars[i];
                bar.index_space = i;
                bar.setTitle(typeName[i]);
            }
        }

        protected override void xrefresh()
        {
            this.context.selectedIndex(this.index);
        }

        /// <summary> 刷新按钮 </summary>
        public void setIndex(int index)
        {
            this.index = index;
        }
    }
}
