using scene.game;
using System.Collections;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将值
    /// </summary>
    public class MJWidgetManager
    {
        private static MJWidgetManager instance;

        private Hashtable table;

        public MJWidgetManager()
        {
            this.table = new Hashtable(50);
            initTable();
        }

        private void initTable()
        {
            int[][] cards = MJCard.CARDS;
            int index = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards[i].Length; j++)
                {
                    table.Add(cards[i][j], index);
                    index++;
                }
            }
        }

        public static MJWidgetManager getInstance()
        {
            if (instance == null)
                instance = new MJWidgetManager();
            return instance;
        }

        /// <summary>
        /// 获取牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Sprite getCard(int card)
        {
            if (card == MJCard.CIN) return null;
            return CacheManager.mjcard1[(int)table[card]];
        }

        /// <summary>
        /// 获取牌背景
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite getCardBg(int index)
        {
            return CacheManager.mjbg[index];
        }
    }
}
