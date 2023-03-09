using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 打的牌,招吃,招碰....
    /// </summary>
    public class SendView : UnrealLuaSpaceObject
    {

        /// <summary>
        /// 掉坎,吃吐,招碰,招吃,偷成4张,撕对,巴牌,吃成坎,吃成4根,后对7
        /// </summary>
        public const int DIAOKAN = 2,
            CHOWTU = 3,
            ZHAOPONG = 4,
            ZHAOCHOW = 5,
            COUNT4 = 6,
            SHIDUI = 7,
            KONG = 8,
            CHOW3 = 9,
            PODUI = 10,
            CHOW4 = 11,
            AFTER7 = 12;

        private UnrealDivTableContainer container;

        private ArrayList<int> cards;

        protected override void xinit()
        {
            this.container = this.transform.Find("container").GetComponent<UnrealDivTableContainer>();
            this.container.init();

            cards = new ArrayList<int>();

        }

        public void sendCard(int card, int type)
        {
            if (cards.count != 0)
            {
                int last_card = cards.get(cards.count - 2);
                if (card != last_card)
                    this.cards.clear();
            }

            cards.add(card);
            cards.add(type);

            this.container.resize(cards.count/2);

            for (int i = 0; i < cards.count; i += 2)
            {
                SendViewBar bar = (SendViewBar) this.container.bars[i/2];
                bar.setData(cards.get(i), cards.get(i + 1));
                bar.index_space = i/2;
                bar.refresh();
            }

            this.starttime = TimeKit.currentTimeMillis;

            float w = getWidth();
            if (cards.count/2 > 1)
            {
                DisplayKit.setLocalX(this.container, -w*((cards.count/2)/2));
            }
            else
            {
                DisplayKit.setLocalX(this.container, 0);
            }
        }

        public void clearList()
        {
            this.cards.clear();
            this.starttime = 0;
            this.setVisible(false);
        }

        /// <summary>
        /// 显示时间ms
        /// </summary>
        long countdown = 1500;

        /// <summary>
        /// 显示时的时间
        /// </summary>
        long starttime;


        protected override void xupdate()
        {
            long time = TimeKit.currentTimeMillis;

            if (time <= 0 || starttime == 0) return;

            if (this.starttime > 0)
            {
                if (this.starttime + countdown > time) return;
                this.starttime = 0;
                clearList();
            }
        }
    }
}
