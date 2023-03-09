using UnityEngine;

namespace platform.spotred.room
{
    public class BaseDisableView:UnrealLuaSpaceObject
    {
        protected int[] cards;
        public virtual void setCards(int[] cards)
        {
            this.cards = cards;
        }

        /// <summary>
        /// 获取上一张牌的位置
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 getLastCardPos()
        {
            return Vector3.zero;
        }
        /// <summary>
        /// 隐藏上一张牌
        /// </summary>
        public virtual void hideLastCards()
        {

        }
    }
}
