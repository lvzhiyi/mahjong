namespace platform.poker
{
    /// <summary> 玩家桌面状态 </summary> 叫地主 抢地主 要不起 加倍 等
    public class PKStageDetailStatusView : UnrealLuaSpaceObject
    {
        protected UnrealTextField text;

        protected override void xinit()
        {
            text = transform.GetComponent<UnrealTextField>();
        }

        public virtual void showText(int type, int index) { }
    }
}