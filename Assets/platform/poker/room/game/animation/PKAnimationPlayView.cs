using UnityEngine;

namespace platform.poker
{
    /// <summary> 扑克 动画管理 </summary>
    public class PKAnimationPlayView : UnrealLuaSpaceObject
    {
        /// <summary> 龙骨动画管理器 </summary>
        protected PKAnimationPlayControl lefts, rights, downs,top;

        /// <summary> 播放相应动画 </summary>
        public virtual void animationPlay(int type, int[] cards, int pos) { }

        protected virtual void printInfo(int pos, int cardsType) { }

        /// <summary> 获得动画 type 值 </summary>
        protected virtual int getType(int type) { return -1; }

        /// <summary> 获得动画 type 名 </summary>
        protected virtual string getTypeName(int type) { return ""; }

        /// <summary> 播放动画 </summary>
        protected virtual void Play(Animator animator, int type) { }

        protected virtual void Play()
        {

        }
    }
}
