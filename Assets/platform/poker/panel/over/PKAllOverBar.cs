using basef.arena;
using basef.player;
using cambrian.common;
using platform.spotred;
using platform.spotred.over;
using platform.spotred.waitroom;
using UnityEngine.UI;

namespace platform.poker
{
    public class PKAllOverBar : UnrealLuaSpaceObject
    {
        private MatchPlayerBar playerbar;
        private SimplePlayer player;
        private TotalScore count;

        private bool master;

        private UnrealTableContainer scores;

        private long[] sc;

        private Image fangzhu;

        private int jushu;

        private long ticket;

        private UnrealTableContainer point;

        private Text allScore1;

        private Text allScore2;

        private UnrealTextField ticketInfo;

        private Room room;

        //private Image bg1;

        private bool win;

        protected override void xinit()
        {
            base.xinit();
            playerbar = transform.Find("player").GetComponent<MatchPlayerBar>();
            playerbar.init();

            scores = transform.Find("center").Find("mask").Find("score").GetComponent<UnrealTableContainer>();
            scores.init();

            point = transform.Find("group").GetComponent<UnrealTableContainer>();
            point.init();
            point.resize(5);

            ticketInfo = transform.Find("ticket").GetComponent<UnrealTextField>();
            ticketInfo.init();

            fangzhu = transform.Find("player").Find("fangzhu").GetComponent<Image>();
            allScore1 = transform.Find("allscore1").GetComponent<Text>();
            allScore2 = transform.Find("allscore2").GetComponent<Text>();

            //bg1 = transform.Find("bg1").GetComponent<Image>();
        }

        public void setInfo(bool master, SimplePlayer player, TotalScore count, long[] sc, bool win, Room room)
        {
            this.master = master;
            this.player = player;
            this.count = count;
            this.sc = sc;
            this.jushu = room.roomRule.rule.matchCount;
            this.ticket = room.getRoomResult().getTicket();
            this.room = room;
            this.win = win;
        }

        protected override void xrefresh()
        {
            //bg1.gameObject.SetActive(room.indexOf() == index_space);
            fangzhu.gameObject.SetActive(master);
            playerbar.setPlayer(player, count.score);
            playerbar.refresh();

            ticketInfo.text = MathKit.getRound2LongStr(ticket);
            ticketInfo.setVisible(win && room.isType(Room.ARENA));

            allScore1.gameObject.SetActive(count.score >= 0);
            allScore2.gameObject.SetActive(count.score < 0);
            allScore1.text = MathKit.getRound2LongStr(count.score);
            allScore2.text = MathKit.getRound2LongStr(count.score);
           
            scores.resize(jushu);

            for (int i = 0; i < jushu; i++)
            {
                AllOverScoreBar bar = (AllOverScoreBar)scores.bars[i];
                if (i < sc.Length)
                {
                    bar.setData(i + 1, sc[i]);
                    bar.index_space = i;
                    bar.refresh();
                }
                else
                {
                    bar.setData(i + 1, 0);
                    bar.index_space = i;
                    bar.re();
                }
            }
            scores.resizeDelta();
        }
    }
}
