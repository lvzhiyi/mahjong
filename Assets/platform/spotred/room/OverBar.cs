using basef.player;
using cambrian.common;
using platform.spotred.waitroom;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 单局结束
    /// </summary>
    public class OverBar:UnrealLuaSpaceObject
    {
        SimplePlayer player;
        PlayerCards pcard;
        Room room;
        /// <summary>
        /// 胡的牌
        /// </summary>
        private int card;
        /// <summary>
        /// 总积分
        /// </summary>
        private SpotRedCount count;
        /// <summary>
        /// 手牌
        /// </summary>
        UnrealDivTableContainer handsCards;
        /// <summary>
        /// 固定牌
        /// </summary>
        UnrealDivTableContainer groupCards;
        /// <summary>
        /// 玩家信息
        /// </summary>
        PlayerBar playerbar;
        /// <summary>
        /// 本局得分
        /// </summary>
        UnrealTextField point;

        private Image img;

        private Image bu_ke_chu;


        bool isBanker = false;

        private bool show_bu_ke_chu;

        private Text info;
        /// <summary>
        /// 番数
        /// </summary>
        private UnrealTextField fanshu;

        public void setInfo(Room room, SimplePlayer player, PlayerCards pcard, bool isBanker, SpotRedCount count,int card,bool show_bu_ke_chu)
        {
            this.room = room;
            this.player = player;
            this.pcard = pcard;
            this.isBanker = isBanker;
            this.count = count;
            this.card = card;
            this.show_bu_ke_chu = show_bu_ke_chu;
        }

        protected override void xinit()
        {
            this.point = this.transform.Find("point").GetComponent<UnrealTextField>();
            this.point.init();
            this.handsCards = this.transform.Find("single").GetComponent<UnrealDivTableContainer>();
            this.handsCards.init();
            this.groupCards = this.transform.Find("group").GetComponent<UnrealDivTableContainer>();
            this.groupCards.init();
            this.playerbar = this.transform.Find("player").GetComponent<PlayerBar>();
            this.playerbar.init();
            this.img = this.transform.Find("hu").GetComponent<Image>();
            this.bu_ke_chu = this.transform.Find("bukechu").GetComponent<Image>();
            this.fanshu = this.transform.Find("fanshu").GetComponent<UnrealTextField>();

            this.info = this.transform.Find("info").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            this.showHandsCards();
            this.showGroupCards();
            this.showPoint();

            this.playerbar.setPlayer(this.player,count.score);
            this.playerbar.refresh();
            if (this.isBanker) this.playerbar.showDang();

            this.bu_ke_chu.gameObject.SetActive(show_bu_ke_chu);
            string str = "";
            if (pcard.hasStatus(CPPlayerCards.STATUS_HU))
                str += Account.getInfo(pcard.hu_type);
            else
            {
                if (pcard.hasStatus(CPPlayerCards.STATUS_BAOPAI))
                    str += str.Length > 0 ? ",报牌" : "报牌";
                if (pcard.hasStatus(CPPlayerCards.STATUS_PIAO))
                    str += str.Length > 0 ? ",加飘" : "加飘";
            }
            if (pcard.root_count > 0)
            {
                if (str.Length > 0)
                    str += ",";
                str +=  "根x"+ pcard.root_count;
            }

            if (pcard.fu_count>0)
            {
                if (str.Length > 0)
                    str += ",";
                str += pcard.fu_count + "福";
            }

            this.fanshu.gameObject.SetActive(false);
            if (pcard.fanshu > 0)
            {
                this.fanshu.text = pcard.fanshu + "番";
                this.fanshu.gameObject.SetActive(true);
            }

            this.info.text = str;
        }

        private void showPoint()
        {
            this.point.text = this.pcard.point + "";
            if (this.pcard.point > 0)
            {
                this.point.text = "<color=#2D9007>" + MathKit.getRound2LongStr(this.pcard.point) + "</color>";
                this.img.gameObject.SetActive(true);
            }
            else if (this.pcard.point < 0)
            {
                this.point.text = "<color=#D8631D>" + MathKit.getRound2LongStr(this.pcard.point) + "</color>";
                this.img.gameObject.SetActive(false);
            }
            else
            {
                this.point.text = "<color=#895F3D>" + MathKit.getRound2LongStr(this.pcard.point) + "</color>";
                this.img.gameObject.SetActive(false);
            }
            
        }

        /// <summary>
        /// 显示手牌
        /// </summary>
        public void showHandsCards()
        {
            ArrayList<int> array = this.pcard.handcards;

            if (pcard.point > 0)
            {
                for (int i = 0; i < array.count; i++)
                {
                    if (array.get(i) == card)
                    {
                        array.removeAt(i);
                        break;
                    }
                }
            }

            ArrayList<int[]> list=CardsManager.getHand(array.toArray(),room.roomRule.rule.getRuleAtribute("is25single"));

            ArrayList<int> ll = new ArrayList<int>();

            for (int i=0;i<list.count;i++)
            {
                int[] c=list.get(i);
                for (int j=c.Length-1;j>=0;j--)
                {
                    ll.add(c[j]);
                }
            }

            handsCards.resize(ll.count);

            for (int i=0;i<ll.count;i++)
            {
                SingleBar bar = (SingleBar)this.handsCards.bars[i];
                bar.setCard(ll.get(i),0);
                bar.index_space = i;
                bar.refresh();
            }
        }

        /// <summary>
        /// 刷新固定牌
        /// </summary>
        public void showGroupCards()
        {
            int fnum = this.pcard.handcards.count+1;

            int num = this.pcard.fixCards.count;

            FixedCards[] cards = this.pcard.fixCards.toArray();

            ArrayList<int> list = new ArrayList<int>();
            for (int i=0;i<cards.Length;i++)
            {
                int[] c= cards[i].getCards();
                for (int j=0;j<c.Length;j++)
                {
                    list.add(c[j]);
                }

            }

            if(pcard.point>0&&card!=Card.NO_CARD)
                this.groupCards.resize(list.count+1);
            else
                this.groupCards.resize(list.count);
            for (int i = 0; i < list.count; i++)
            {
                GroupCardsBar bar = ((GroupCardsBar) this.groupCards.bars[i]);
                bar.setCard(list.get(i), fnum);
                bar.index_space = i;
                bar.refresh();
            }

            if (pcard.point > 0&&card!=Card.NO_CARD)
            {
                GroupCardsBar bar = (GroupCardsBar)this.groupCards.bars[list.count];
                bar.setCard(card, fnum+1);
                bar.index_space = list.count;
                bar.refresh();
            }
        }
    }
}
