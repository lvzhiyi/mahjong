namespace platform.mahjong
{
    /// <summary>
    /// 换三张视图
    /// </summary>
    public class AYMJHuanSZView : MJHuanSZView
    {
        protected override void xinit()
        {
            right = transform.Find("loc1").GetComponent<UnrealLuaSpaceObject>();
            top = transform.Find("loc2").GetComponent<UnrealLuaSpaceObject>();
            left = transform.Find("loc3").GetComponent<UnrealLuaSpaceObject>();
            down = transform.Find("loc0").GetComponent<AYMJSelfHuanSZView>();
            down.init();
        }

        protected override void xrefresh()
        {
            show(false);
        }
    }
}
