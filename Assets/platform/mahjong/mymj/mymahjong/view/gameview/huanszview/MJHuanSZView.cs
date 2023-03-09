using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 换三张视图
    /// </summary>
    public class MJHuanSZView : UnrealLuaSpaceObject
    {
        protected UnrealLuaSpaceObject right;

        protected UnrealLuaSpaceObject top;

        protected UnrealLuaSpaceObject left;

        protected MJSelfHuanSZView down;

        protected override void xinit()
        {
            right = transform.Find("loc1").GetComponent<UnrealLuaSpaceObject>();
            top = transform.Find("loc2").GetComponent<UnrealLuaSpaceObject>();
            left = transform.Find("loc3").GetComponent<UnrealLuaSpaceObject>();
            down = transform.Find("loc0").GetComponent<MJSelfHuanSZView>();
            down.init();
        }

        protected override void xrefresh()
        {
            show(false);
        }

        protected void show(bool b)
        {
            right.setVisible(b);
            top.setVisible(b);
            left.setVisible(b);
            down.setVisible(b);
        }

        /// <summary>
        /// 刷新玩家换牌状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isReplace"></param>
        public void refreshStatus(int index, int status)
        {
            bool isReplace = StatusKit.hasStatus(status, MJPlayerCards.STATUS_REPLACE);
            switch (index)
            {
                case MahjongRoomPanel.LOC_DOWN:
                    if (isReplace)
                        down.showHpicon();
                    else
                        down.showHuanOperate();
                    down.setVisible(true);
                    break;
                case MahjongRoomPanel.LOC_RIGHT:
                    right.setVisible(isReplace);
                    break;
                case MahjongRoomPanel.LOC_TOP:
                    top.setVisible(isReplace);
                    break;
                case MahjongRoomPanel.LOC_LEFT:
                    left.setVisible(isReplace);
                    break;
            }
        }
    }
}
