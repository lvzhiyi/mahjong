using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    public class AYMJBaoKongView:UnrealLuaSpaceObject
    {
        UnrealTableContainer container;

        [HideInInspector] public ArrayList<int> selectCard = new ArrayList<int>();


        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
        }

        /// <summary>
        /// 显示需要报杠的牌
        /// </summary>
        /// <param name="card"></param>
        public void showBaoKong(int[] card)
        {
            container.resize(card.Length);
            for (int i=0;i<card.Length;i++)
            {
                AYMJBaoKongBar bar=(AYMJBaoKongBar)container.bars[i];
                bar.setCard(card[i]);
                bar.refresh();
            }
            selectCard.clear();
        }

        public void removeBaoCard(int card)
        {
            for (int i=0;i< selectCard.count;i++)
            {
                if (selectCard.get(i) == card)
                {
                    selectCard.removeAt(i);
                    break;
                }
            }
        }
    }
}
