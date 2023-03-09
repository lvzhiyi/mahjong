namespace platform.poker
{
    /// <summary> 其他玩家手牌 </summary>
    public class PKTenOtherHandBar : PKOtherHandBar
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        /*------------------设置牌的位置------------------*/

        public float point_x = -220; //x位置
        public float point_y = 0;
        public int acrossnum = 10;//张数
        public float vertical = 40;
        public float horizontal = 40;//每张牌的间距

        float pointx1;

        float pointy1;

        protected override void computePos(int index)
        {
            if (index < acrossnum)
            {
                pointx1 = point_x + index * horizontal;
                pointy1 = point_y;
            }
            else
            {
                pointx1 = point_x + (index - acrossnum) * horizontal;
                pointy1 = point_y- vertical;
            }
            DisplayKit.setLocalX(this,pointx1);
            DisplayKit.setLocalY(this,pointy1);
        }
    }
}
