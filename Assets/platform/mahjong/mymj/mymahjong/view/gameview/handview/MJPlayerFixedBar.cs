namespace platform.mahjong
{
    /// <summary>
    /// 固定牌区的牌
    /// </summary>
    public class MJPlayerFixedBar : MJBasePlayerFixedBar
    {
        protected override void xinit()
        {
            base.xinit();
        }

        public override  void setCardValue(MJFixedCards cardValue, int postion)
        {
            base.setCardValue(cardValue,postion);
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        public override int getdirection()
        {
            return base.getdirection();
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


        public override void showPribg()
        {
            base.showPribg();
        }
    }
}
