using cambrian.uui;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家定缺的牌视图
    /// </summary>
    public class MJDingQueCardView : UnrealLuaSpaceObject
    {
        private SpritesList down;

        private SpritesList right;

        private SpritesList top;

        private SpritesList left;

        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<SpritesList>();
            right = transform.Find("loc1").GetComponent<SpritesList>();
            top = transform.Find("loc2").GetComponent<SpritesList>();
            left = transform.Find("loc3").GetComponent<SpritesList>();
        }

        protected override void xrefresh()
        {
            down.setVisible(false);
            right.setVisible(false);
            top.setVisible(false);
            left.setVisible(false);
        }

        public void setDistypes(int[] distype)
        {
            for (int i = 0; i < distype.Length; i++)
            {
                int index = MahJongPanel.getPlayerIndex(i);
                int t = getIndex(distype[i]);
                bool b = (t != MJCard.CNO);
                switch (index)
                {
                    case MahJongPanel.LOC_DOWN:
                        if (b)
                            down.ShowIndex(t, false);
                        down.setVisible(b);
                        break;
                    case MahJongPanel.LOC_RIGHT:
                        if (b)
                            right.ShowIndex(t, false);
                        right.setVisible(b);
                        break;
                    case MahJongPanel.LOC_TOP:
                        if (b)
                            top.ShowIndex(t, false);
                        top.setVisible(b);
                        break;
                    case MahJongPanel.LOC_LEFT:
                        if (b)
                            left.ShowIndex(t, false);
                        left.setVisible(b);
                        break;
                }
            }
        }

        /// <summary>
        /// 显示单个玩家定缺的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="distype"></param>
        public void setSingleDistype(int index,int distype)
        {
           index = MahJongPanel.getPlayerIndex(index);
            int t = getIndex(distype);
            bool b = (t != MJCard.CNO);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    if (b)
                        down.ShowIndex(t, false);
                    down.setVisible(b);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    if (b)
                        right.ShowIndex(t, false);
                    right.setVisible(b);
                    break;
                case MahJongPanel.LOC_TOP:
                    if (b)
                        top.ShowIndex(t, false);
                    top.setVisible(b);
                    break;
                case MahJongPanel.LOC_LEFT:
                    if (b)
                        left.ShowIndex(t, false);
                    left.setVisible(b);
                    break;
            }
        }

        private int getIndex(int type)
        {
            if (type == MJCard.TYPE_DOT)
                return 0;
            else if (type == MJCard.TYPE_BAM)
            {
                return 1;
            }
            else if (type == MJCard.TYPE_CHA)
            {
                return 2;
            }

            return MJCard.CNO;
        }

    }
}
