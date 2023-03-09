using platform.spotred;
using System.Collections;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 根据后台传过来牌的值,取对应的图片
    /// </summary>
    public class WidgetManager
    {
        public static WidgetManager instance;

        private Hashtable table;


        public WidgetManager()
        {
            this.table=new Hashtable(50);
            initTable();
        }

        private void initTable()
        {
            this.table.Add(Card.INVISIBLE,0);
        
            this.table.Add(Card.C11,1);
            this.table.Add(Card.C12,2);
            this.table.Add(Card.C13, 3);
            this.table.Add(Card.C22, 7);
            this.table.Add(Card.C14, 4);
            this.table.Add(Card.C23, 8);
            this.table.Add(Card.C15, 5);
            this.table.Add(Card.C24, 9);
            this.table.Add(Card.C33, 12);
            this.table.Add(Card.C16, 6);
            this.table.Add(Card.C25, 10);
            this.table.Add(Card.C34, 13);
            this.table.Add(Card.C26, 11);
            this.table.Add(Card.C35, 14);
            this.table.Add(Card.C44, 16);
            this.table.Add(Card.C36, 15);
            this.table.Add(Card.C45, 17);
            this.table.Add(Card.C46, 18);
            this.table.Add(Card.C55, 19);
            this.table.Add(Card.C56, 20);
            this.table.Add(Card.C66, 21);
            this.table.Add(Card.C01, 22);//财神
            this.table.Add(Card.C0D, 23);//听用
        }

        public static WidgetManager getInstance()
        {
            if (instance==null)
                instance=new WidgetManager();
            return instance;
        }

        /// <summary>
        /// 获取手里的牌sprite
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite getHandCard(int index)
        { 
            int card_index=(int)this.table[index];
            return CacheManager.cards[card_index];
        }


        /// <summary>
        /// 获取手里的牌sprite
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite getNowHandCard(int index)
        {
            int card_index = (int)this.table[index];
            return CacheManager.handcards[card_index];
        }

        /// <summary>
        /// 获取显示的牌sprite
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite getShowCard(int index)
        {
            int card_index = (int)this.table[index];
            return CacheManager.cardsShow[card_index];
        }

        /// <summary>
        /// 获取显示的牌sprite
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite getJieSanShowCard(int index)
        {
            int card_index = (int)this.table[index];
            return CacheManager.jiesuancards[card_index];
        }
    }
}
