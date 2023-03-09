namespace platform.mahjong
{
    public class AYMJDisAbleBar:MJBaseDisAbleBar
    {
        protected override void xinit()
        {
            base.xinit();
        }

        public override void setData(int card,int postion,int selectcard=MJCard.CNO)
        {
            base.setData(card,postion, selectcard);
        }

        public override void setDataSelect(int selectcard)
        {
            base.setDataSelect(selectcard);
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }


        public override void down()
        {
            base.down();
        }

        public override void right()
        {
            base.right();
        }

        public override void top()
        {
            base.top();
        }

        public override void left()
        {
            base.left();
        }
    }
}
