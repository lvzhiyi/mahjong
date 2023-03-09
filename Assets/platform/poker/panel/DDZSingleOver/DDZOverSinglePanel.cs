using System.Collections;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;
namespace platform.poker
{
    /// <summary> 斗地主 结算界面 </summary>
    public class DDZOverSinglePanel : UnrealLuaPanel
    {
        private SpritesList title;

        private ScrollContainer container;

        public GameObject nextbtn { get; private set; }

        private GameObject returnbtn;

        //private SpritesList npc;

        /// <summary> 抽牌容器 </summary>
        private UnrealTableContainer remainContainer;

        protected override void xinit()
        {
            base.xinit();
            title = content.Find("title").GetComponent<SpritesList>();

            //npc = content.Find("npc").Find("img").GetComponent<SpritesList>();

            nextbtn = content.Find("downBtn").Find("nextbtn").gameObject;

            returnbtn = content.Find("downBtn").Find("returnbtn").gameObject;

            remainContainer = content.Find("remain").GetComponent<UnrealTableContainer>();
            remainContainer.init();

            container = content.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(Room.room.roomRule.rule.playerCount);
            nextbtn.gameObject.SetActive(!Room.room.isStatus(Room.STATE_DESTORY));
            returnbtn.gameObject.SetActive(Room.room.isStatus(Room.STATE_DESTORY));
            container.refresh();

            if (sendcards == null || sendcards.Length == 0)
            {
                remainContainer.setVisible(false);
            }
            else//显示剩余牌堆的牌
            {
                var len = sendcards.Length;
                sendcards = CardSort.LTSCards(sendcards);
                remainContainer.cols = len;
                remainContainer.resize(len);
                for (int i = 0; i < len; i++)
                {
                    var bar = (PokerOverRemainBar)remainContainer.bars[i];
                    bar.setCard(sendcards[i]);
                    bar.refresh();
                }
                remainContainer.resizeDelta();
                remainContainer.relayout();
                remainContainer.setVisible(true);
            }
        }

        private long[] socres;
        private int[][] handcardshave, handcardsuse;
        private int[] sendcards;

        public void setData(long[] socres, int[] statuss, int[][] handcardshave, int[][] handcardsuse, int[] sendcards)
        {
            this.socres = socres;
            int index = 0, npcindex = 0; ;
            int status = statuss[Room.room.indexOf()];
            if (StatusKit.hasStatus(status, PlayerStatus.CHUN_TIAN))
            {
                index = (socres[Room.room.indexOf()] >= 0) ? 2 : 3;
            }
            else if (StatusKit.hasStatus(status, PlayerStatus.FAN_CHUN_TIAN))
            {
                index = (socres[Room.room.indexOf()] >= 0) ? 4 : 5;
            }
            else
            {
                index = (socres[Room.room.indexOf()] >= 0) ? 0 : 1;
            }
            title.ShowIndex(index, true);

            if (DDZMatch.match.mineDiZhu)
            {
                npcindex = (socres[Room.room.indexOf()] >= 0) ? 2 : 3;
            }
            else npcindex = (socres[Room.room.indexOf()] >= 0) ? 0 : 1;
            //npc.ShowIndex(npcindex, true);

            this.handcardshave = handcardshave;
            this.handcardsuse = handcardsuse;
            this.sendcards = sendcards;
        }

        private void updateScrollData(ScrollBar bars, int index)
        {
            if (socres == null) return;

            var socre = socres[index].ToString();
            var user = Room.room.players[index];
            var bet = DDZMatch.match.multipleBean.LastPoint.ToString();
            var mutiple = 0;
            if (DDZMatch.match.replay) mutiple = DDZMatch.match.multipleBean.getSinglePoint(index);
            else mutiple = DDZMatch.match.multipleBean.points[index];
            var bar = (DDZOverSingleUserInfoBar)bars;
            var maxmutiple = Room.room.getRule().getIntAtribute("maxmultiple");
            bar.refreshOverUserInfo(socre, bet, mutiple > maxmutiple ? maxmutiple.ToString() : mutiple.ToString(), user, handcardshave[index], handcardsuse[index]);
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
