using basef.player;
using cambrian.common;
using platform.spotred.waitroom;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.over
{
    public class AllOverBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// WIN:大赢家,DIAN_PAO:点炮高手,NO=不显示
        /// </summary>
        public static int WIN = 1, DIAN_PAO = 2, NO = 0;

        PlayerBar playerbar;

        SimplePlayer player;

        TotalScore count;

        /// <summary>
        /// 大赢家
        /// </summary>
        Transform win;

        /// <summary>
        /// 点炮高手
        /// </summary>
        //Transform dian_pao;

        bool master;

        /// <summary>
        /// 是否显示大赢家或者点炮高手
        /// </summary>
        int isShow = 0;

        /// <summary>
        /// 显示每局分数容器
        /// </summary>
        private UnrealTableContainer scores;

        /// <summary>
        /// 分数数组
        /// </summary>
        private long[] sc;

        private Image fangzhu;

        /// <summary>
        /// 局数
        /// </summary>
        private int jushu;
        /// <summary>
        /// 门票
        /// </summary>
        private long ticket;

        private UnrealTableContainer point;
        /// <summary>
        /// 正数
        /// </summary>
        private Text allScore1;
        /// <summary>
        /// 负数
        /// </summary>
        private Text allScore2;
        /// <summary>
        /// 门票
        /// </summary>
        private UnrealTextField ticketInfo;

        private Room room;

        protected override void xinit()
        {
            base.xinit();
            this.playerbar = this.transform.Find("player").GetComponent<PlayerBar>();
            this.playerbar.init();
            this.win = this.transform.Find("win").GetComponent<Transform>();
            //this.dian_pao = this.transform.Find("win_dipao").Find("dipao").GetComponent<Transform>();
            this.scores = this.transform.Find("center").Find("mask").Find("score").GetComponent<UnrealTableContainer>();
            this.scores.init();

            this.fangzhu = this.transform.Find("player").Find("fangzhu").GetComponent<Image>();
            this.point = this.transform.Find("group").GetComponent<UnrealTableContainer>();
            this.point.init();
            this.point.resize(5);

            ticketInfo = transform.Find("ticket").GetComponent<UnrealTextField>();
            ticketInfo.init();

            allScore1 = transform.Find("allscore1").GetComponent<Text>();
            allScore2 = transform.Find("allscore2").GetComponent<Text>();
        }

        public void setInfo(bool master, SimplePlayer player, TotalScore count, int isShow, long[] sc,Room room)
        {
            this.master = master;
            this.player = player;
            this.count = count;
            this.isShow = isShow;
            this.sc = sc;
            this.jushu = room.roomRule.rule.matchCount;
            this.ticket = room.getRoomResult().getTicket();
            this.room = room;
        }

        protected override void xrefresh()
        {
            this.fangzhu.gameObject.SetActive(this.master);
            this.playerbar.setPlayer(this.player, count.score);
            this.playerbar.refresh();

            this.win.gameObject.SetActive(false);
            //this.dian_pao.gameObject.SetActive(false);

            ticketInfo.text = MathKit.getRound2LongStr(ticket);
            ticketInfo.setVisible(isShow==WIN&&room.isType(Room.ARENA));

            if (this.isShow == WIN)
            {
                this.win.gameObject.SetActive(true);
            }
            //else if (this.isShow == DIAN_PAO)
            //{
            //    this.dian_pao.gameObject.SetActive(true);
            //}
 
            allScore1.gameObject.SetActive(count.score>=0);
            allScore2.gameObject.SetActive(count.score < 0);
            allScore1.text = MathKit.getRound2LongStr(count.score);
            allScore2.text = MathKit.getRound2LongStr(count.score);

            this.scores.resize(jushu);

            for (int i = 0; i < jushu; i++)
            {
                AllOverScoreBar bar = (AllOverScoreBar)this.scores.bars[i];
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
            this.scores.resizeDelta();
        }
    }
}
