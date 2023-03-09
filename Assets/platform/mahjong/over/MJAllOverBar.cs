using basef.arena;
using basef.player;
using cambrian.common;
using platform.spotred;
using platform.spotred.waitroom;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 总结算bar
    /// </summary>
    public class MJAllOverBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// WIN:大赢家,DIAN_PAO:点炮高手,NO=不显示
        /// </summary>
        public static int WIN = 1, DIAN_PAO = 2, NO = 0;
        /// <summary>
        /// 玩家显示
        /// </summary>
        PlayerBar playerbar;
        /// <summary>
        /// 大赢家
        /// </summary>
        Image win;
        /// <summary>
        /// 点炮高手
        /// </summary>
        //Image dian_pao;
        /// <summary>
        /// 房主
        /// </summary>
        private Image master;
        /// <summary>
        /// 局数
        /// </summary>
        private int jushu;
        /// <summary>
        /// 门票
        /// </summary>
        private long ticket;
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
        /// <summary>
        /// 玩家数据
        /// </summary>
        SimplePlayer player;

        TotalScore count;
        /// <summary>
        /// 是否是房主
        /// </summary>
        bool isMaster;
        /// <summary>
        /// 是否显示大赢家或者点炮高手
        /// </summary>
        int isShow = 0;
        /// <summary>
        /// 房间信息
        /// </summary>
        private Room room;
        /// <summary>
        /// 麻将结果
        /// </summary>
        MahjongCount mjcount;

        /// <summary> 自摸次数 </summary>
        UnrealTextField huselfTimes;
        /// <summary> 接炮次数 </summary>
        UnrealTextField huTimes;
        /// <summary> 点炮次数 </summary>
        UnrealTextField dianpaoTimes;
        /// <summary> 暗杠次数 </summary>
        UnrealTextField kongPriTimes;
        /// <summary> 明杠次数（包含补杠） </summary>
        UnrealTextField kongPubTimes;
        /// <summary> 查大叫次数 </summary>
        UnrealTextField checkReady;

        /// <summary>
        /// 边框
        /// </summary>
        //Image bg1;

        protected override void xinit()
        {
            base.xinit();
            huselfTimes = transform.Find("text1").GetComponent<UnrealTextField>();
            huTimes = transform.Find("text2").GetComponent<UnrealTextField>();
            dianpaoTimes = transform.Find("text3").GetComponent<UnrealTextField>();
            kongPriTimes = transform.Find("text4").GetComponent<UnrealTextField>();
            kongPubTimes = transform.Find("text5").GetComponent<UnrealTextField>();
            checkReady = transform.Find("text6").GetComponent<UnrealTextField>();

            playerbar = transform.Find("player").GetComponent<PlayerBar>();
            playerbar.init();
            win = transform.Find("win").GetComponent<Image>();
            //dian_pao = transform.Find("dipao").GetComponent<Image>();
            master = playerbar.transform.Find("fangzhu").GetComponent<Image>();
            ticketInfo = transform.Find("ticket").GetComponent<UnrealTextField>();
            ticketInfo.init();
            allScore1 = transform.Find("allscore1").GetComponent<Text>();
            allScore2 = transform.Find("allscore2").GetComponent<Text>();
            //bg1 = transform.Find("bg1").GetComponent<Image>();
        }

        public void setInfo(bool master, int isShow, long[] sc, Room room, MahjongCount mjcount)
        {
            isMaster = master;
            this.player = room.players[index_space].player;
            this.count = room.getRoomResult().getIndexCount(index_space);
            this.isShow = isShow;
            jushu = room.roomRule.rule.matchCount;
            ticket = room.getRoomResult().getTicket();
            this.mjcount = mjcount;
            this.room = room;
        }

        protected override void xrefresh()
        {
            this.master.gameObject.SetActive(this.isMaster);
            this.playerbar.setPlayer(this.player, count.score);
            this.playerbar.refresh();
            //this.bg1.gameObject.SetActive(room.indexOf() == index_space);

            this.win.gameObject.SetActive(false);
            //this.dian_pao.gameObject.SetActive(false);

            ticketInfo.text = MathKit.getRound2LongStr(ticket);
            ticketInfo.setVisible(isShow == WIN && room.isType(Room.ARENA));

            if (this.isShow == WIN)
            {
                this.win.gameObject.SetActive(true);
            }
            //else if (this.isShow == DIAN_PAO)
            //{
            //    this.dian_pao.gameObject.SetActive(true);
            //}

            allScore1.gameObject.SetActive(count.score >= 0);
            allScore2.gameObject.SetActive(count.score < 0);
            allScore1.text = MathKit.getRound2LongStr(count.score);
            allScore2.text = MathKit.getRound2LongStr(count.score);
        
            showdetailInfo();
        }

        private void showdetailInfo()
        {
            huselfTimes.text = mjcount.huselfTimes.ToString();
            huTimes.text = mjcount.huTimes.ToString();
            dianpaoTimes.text = mjcount.dianpaoTimes.ToString();
            kongPriTimes.text = mjcount.kongPriTimes.ToString();
            kongPubTimes.text = mjcount.kongPubTimes.ToString();
            checkReady.text = mjcount.checkReady.ToString();
        }
    }
}
