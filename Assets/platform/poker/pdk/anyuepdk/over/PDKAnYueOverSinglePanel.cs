using cambrian.unreal.scroll;
using cambrian.uui;
using System.Collections;
using UnityEngine;

namespace platform.poker
{
    public class PDKAnYueOverSinglePanel : UnrealLuaPanel
    {
        private SpritesList title;

        private ScrollContainer container;

        public GameObject nextbtn { get; private set; }

        private GameObject returnbtn;

        public const int CHUNTIAN = 2;

        private UnrealTextField multipleTitle;

        private string boomScore = "炸弹得分", boomMultiple = "炸弹倍数";
        /// <summary>
        /// 抽牌容器
        /// </summary>
        UnrealTableContainer remainContainer;

        protected override void xinit()
        {
            base.xinit();

            title = content.Find("title").GetComponent<SpritesList>();

            nextbtn = content.Find("downBtn").Find("nextbtn").gameObject;

            returnbtn = content.Find("downBtn").Find("returnbtn").gameObject;

            multipleTitle = content.Find("datatitle").Find("multiple").GetComponent<UnrealTextField>();
            multipleTitle.init();

            container = content.Find("container").GetComponent<ScrollContainer>();
            container.init();

            remainContainer = content.Find("remain").GetComponent<UnrealTableContainer>();
            remainContainer.init();
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(Room.room.roomRule.rule.playerCount);
            nextbtn.gameObject.SetActive(!Room.room.isStatus(Room.STATE_DESTORY));
            returnbtn.gameObject.SetActive(Room.room.isStatus(Room.STATE_DESTORY));
            container.refresh();

            int len = remianCards == null ? 0 : remianCards.Length;
            if (remianCards != null)
            {
                remianCards = CardSort.LTSCards(remianCards);
            }

            remainContainer.setVisible(len > 0);
            if (len > 0)
            {
                remainContainer.cols = len;
                remainContainer.resize(len);
                for (int i = 0; i < len; i++)
                {
                    PokerOverRemainBar bar = (PokerOverRemainBar)remainContainer.bars[i];
                    bar.setCard(remianCards[i]);
                    bar.refresh();
                }

                remainContainer.resizeDelta();
                remainContainer.relayout();
            }
        }

        private long[] socres;

        private int[] chuntian, boomsocre, cardsnum, remianCards;

        private int[][] handcardshave, handcardsuse;

        public void setData(long[] socres, int[] chuntian, int[] boomsocre, int[] cardsnum, int[] remianCards, int[][] handcardshave, int[][] handcardsuse)
        {
            this.socres = socres;
            this.chuntian = chuntian;
            this.boomsocre = boomsocre;
            this.cardsnum = cardsnum;
            this.remianCards = remianCards;
            this.handcardshave = handcardshave;
            this.handcardsuse = handcardsuse;
            title.ShowIndex((socres[Room.room.indexOf()] >= 0) ? 0 : 1, true);
            multipleTitle.text = Room.room.getRule().getIntAtribute("multipScore") == 0 ? boomMultiple : boomScore;
        }

        private void updateScrollData(ScrollBar bars, int index)
        {
            if (socres == null || chuntian == null) return;
            var socre = socres[index].ToString();
            var user = PDKAnYueMatch.match.players[index];
            var bet = PDKAnYueMatch.match.rule.getBet().ToString();
            var mutiple = boomsocre[index].ToString();
            var bar = (PDKOverSingleUserInfoBar)bars;
            bar.index_space = index;
            bar.refreshOverUserInfo(-1, socre, bet, mutiple, user, chuntian[index] == CHUNTIAN, cardsnum[index].ToString(), handcardshave[index], handcardsuse[index]);
            bar.index_space = index;
            bar.refresh();
        }

        public void changeLayer(float time = 1f)
        {
            setLayer(transform, (int)Layer.Default);
            gameObject.SetActive(true);
            StopCoroutine("_changeLayer");
            StartCoroutine("_changeLayer", time);
        }

        private IEnumerator _changeLayer(float time)
        {
            yield return new WaitForSeconds(time);
            setLayer(transform, (int)Layer.Panel_3);
            setCVisible(true);
            yield break;
        }
    }
}
