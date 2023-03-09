using DragonBones;
using UnityEngine.UI;

namespace platform.poker
{
    /// <summary> 玩家状态 </summary>
    public class PKPlayerDetailStatusView : UnrealLuaSpaceObject
    {
        public float delayTime = 0.5f;

        /// <summary> 牌数量 </summary>
        protected UnrealTextField cardNum;

        /// <summary> 地主 </summary>
        protected Image banker;

        /// <summary> 手牌提醒 </summary>
        protected UnityArmatureComponent cardwarning;

        protected string jingbaodeng = "jingbaodeng";

        protected override void xinit() { }

        public virtual void setCardNum(int cardnum) { }

        public virtual void showBanker(bool isShow) { }

        public virtual void showCardWarning(bool isShow) { }
    }
}
