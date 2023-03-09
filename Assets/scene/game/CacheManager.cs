using cambrian.common;
using UnityEngine;

namespace scene.game
{
    public class CacheManager
    {
        /// <summary>
        /// (长牌)手里的牌
        /// </summary>
        public static Sprite[] cards;
        /// <summary>
        /// 现在用的手牌
        /// </summary>
        public static Sprite[] handcards;
        /// <summary>
        /// (长牌)出的牌(包括碰,吃,偷的牌)
        /// </summary>
        public static Sprite[] cardsShow;
        /// <summary>
        /// 出的牌
        /// </summary>
        public static Sprite[] jiesuancards;
        /// <summary>
        /// 表情
        /// </summary>
        public static Sprite[] emojis;
        /// <summary>
        /// 通用图标
        /// </summary>
        public static Sprite[] common;
        /// <summary>
        /// 桌面背景图
        /// </summary>
        public static Sprite[] desktop;
        /// <summary>
        /// 商城物品图标
        /// </summary>
        public static Sprite[] shopimg;
        /// <summary>
        /// 男生头像
        /// </summary>
        public static Sprite[] boyRobotHead;
        /// <summary>
        /// 女生头像
        /// </summary>
        public static Sprite[] girlRobotHead;
        /// <summary>
        /// 麻将牌1
        /// </summary>
        public static Sprite[] mjcard1;

        /// <summary> 
        /// 扑克牌 
        /// </summary>
        public static Sprite[] pokercard;
        /// <summary>
        /// 麻将背景
        /// </summary>
        public static Sprite[] mjbg;


        public static void init()
        {
            initArray();
            cardsCache();
            cardsShowCache();
            handcardsCache();
            jiesuanCardsShowCache();
            emojiCache();
            commonCache();
            deskTopCache();
            boyHeadCache();
            girlHeadCache();
            mjCardBgCache();
            mjCard1Cache();
            pokercard = CacheAdd("pokercard");
        }


        private static void initArray()
        {
            cards = new Sprite[24];
            handcards = new Sprite[24];
            cardsShow = new Sprite[24];
            jiesuancards = new Sprite[24];
            emojis = new Sprite[15];
            common = new Sprite[23];
            desktop = new Sprite[13];
            shopimg = new Sprite[40];
            boyRobotHead=new Sprite[100];
            girlRobotHead=new Sprite[100];
            mjcard1 = new Sprite[42];
            mjbg = new Sprite[34];
        }
		
		/// <summary>
        /// 添加Sprite
        /// </summary>
        private static Sprite[] CacheAdd(string path)
        {
            Transform tran = UnrealRoot.root.transform.Find(path);
            Sprite[] obj = new Sprite[tran.childCount];
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                obj[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
            return obj;
        }

        /// <summary>
        /// 自己的牌
        /// </summary>
        private static void cardsCache()
        {
           
            Transform tran = UnrealRoot.root.transform.Find("CardsWidget");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                cards[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
             
        }

        /// <summary>
        /// 手牌
        /// </summary>
        private static void handcardsCache()
        {
            Transform tran = UnrealRoot.root.transform.Find("handcards");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                handcards[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 显示出的牌
        /// </summary>
        private static void cardsShowCache()
        {
            Transform tran = UnrealRoot.root.transform.Find("CardsShowWidget");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                cardsShow[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 结算界面牌
        /// </summary>
        private static void jiesuanCardsShowCache()
        {
            
            Transform tran = UnrealRoot.root.transform.Find("jiesuancard");

            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                jiesuancards[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }




        /// <summary>
        /// 表情
        /// </summary>
        private static void emojiCache()
        {
           
            Transform tran = UnrealRoot.root.transform.Find("Emoji");
            foreach (Transform child in tran)
            {
                int i = getSpriteIndex(child.name)-1;
                emojis[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        private static int getSpriteIndex(string str)
        {
            return StringKit.parseInt(str.Substring(str.LastIndexOf('_') + 1));
        }


        /// <summary>
        /// 表情
        /// </summary>
        private static void commonCache()
        {
            
            Transform tran = UnrealRoot.root.transform.Find("common");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                common[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 桌面
        /// </summary>
        private static void deskTopCache()
        {
            
            Transform tran = UnrealRoot.root.transform.Find("desktop");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                desktop[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 女生头像
        /// </summary>
        private static void girlHeadCache()
        {
            Transform tran = UnrealRoot.root.transform.Find("robotheadgirl");



            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                girlRobotHead[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 男生头像
        /// </summary>
        private static void boyHeadCache()
        {
            Transform tran = UnrealRoot.root.transform.Find("robothead");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                boyRobotHead[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 麻将牌缓存
        /// </summary>
        private static void mjCard1Cache()
        {
            Transform tran = UnrealRoot.root.transform.Find("mjcard1");
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                mjcard1[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

        /// <summary>
        /// 麻将背景牌缓存
        /// </summary>
        private static void mjCardBgCache()
        {
            Transform tran = UnrealRoot.root.transform.Find("mjbg");
            
            foreach (Transform child in tran)
            {
                int i = StringKit.parseInt(child.name);
                mjbg[i] = child.GetComponent<SpriteRenderer>().sprite;
            }
        }

    }
}