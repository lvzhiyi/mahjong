using cambrian.common;
using basef.player;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rank
{
    /// <summary>
    /// 排行榜
    /// </summary>
    public class RankListPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 财富榜，今日，昨日，本周,金豆周榜
        /// </summary>
        public const int WEALTH = 0, DAY = 1, LAST_DAY = 2, WEEK = 3, CLUB_DAY=4, CLUB_WEEK=5, CLUB_LAST_WEEK=6,GOLD_WEEK=7,CLUB_SCORE_DAY=8,CLUB_SCORE_LAST_DAY=9;
        /// <summary>
        /// 排行榜名组
        /// </summary>
        public static string[] typeNames = { "财富榜", "今日排行", "昨日排行", "本周排行" ,"今日排行","本周排行","上周排行","金豆周榜","今日积分","昨日积分"};
        /// <summary>
        /// 
        /// </summary>
        private ScrollContainer container;
        /// <summary>
        /// 周排行
        /// </summary>
        private RankList list;
        /// <summary>
        /// 排行类型列表
        /// </summary>
        private UnrealListContainer leftlist;

        private Text info;

        private Text jushu;

        private Text nickname;

        private PlayerHeadView head;
        /// <summary>
        /// 当前选中排行
        /// </summary>
        private int selectType;
        /// <summary>
        /// 排行显示类型组
        /// </summary>
        int[] types;

        public static string getRankValuleName(int type)
        {
            switch (type)
            {
                case WEALTH:
                    return "金豆：";
                case CLUB_SCORE_DAY:
                case CLUB_SCORE_LAST_DAY:
                    return "积分：";
                default:
                    return "局数:";
            }
        }


        protected override void xinit()
        {
            base.xinit();
            Transform content = this.transform.Find("Canvas").Find("content");
            this.leftlist =content.Find("left").Find("center").Find("mask").Find("container").GetComponent<UnrealListContainer>();
            this.leftlist.init();

            this.container = content.Find("container").GetComponent<ScrollContainer>();
            this.container.init();

            this.info = content.Find("bottom").Find("info").GetComponent<Text>();
           
            this.jushu = content.Find("bottom").Find("jushu").GetComponent<Text>();

            this.nickname = content.Find("bottom").Find("name").GetComponent<Text>();
            this.head = content.Find("bottom").GetComponent<PlayerHeadView>();
            this.head.init();

        }

        protected override void xrefresh()
        {
            this.nickname.text = Player.player.name;
            head.setData(Player.player.head,Player.player.userid);
            head.refresh();

            this.container.updateData(updateBarData);

            int len = list.getRankList().Length;

            this.container.resize(len);

            int rank_index = getRank();
            if (rank_index != -1)
            {
                this.info.text = "NO." + (rank_index + 1);
            }
            else
            {
                this.info.text = "未上榜";
            }

            if(selectType== RankListPanel.CLUB_SCORE_DAY)
                this.jushu.text = getRankValuleName(selectType) + MathKit.getRound2LongStr(list.getJueShu());
            else
                this.jushu.text = getRankValuleName(selectType) + MathKit.getDecimal(list.getJueShu());

        }

        public void setTypes(int[] types)
        {
            this.types = types;
        }

        public void addRandList(RankList list)
        {
            this.list = list;
        }

        public void updateBarData(ScrollBar bar,int index)
        {
            ((RankBar)bar).setType(selectType);
            ((RankBar)bar).setData(list.getRankList()[index], index);
        }

        /// <summary>
        /// 获取排名
        /// </summary>
        /// <returns></returns>
        public int getRank()
        {
            RankPlayer[] players = list.getRankList();
            if (players.Length == 0) return -1;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].getUserId() == Player.player.userid) return i;
            }

            return -1;
        }

        public void refreshSelected(int type)
        {
            this.selectType = type;
            this.leftlist.resize(types.Length);
            for (int i = 0; i < types.Length; i++)
            {
                RankTypeBar bar = (RankTypeBar)this.leftlist.bars[i];
                bar.typename = typeNames[types[i]];
                bar.rankType = types[i];
                bar.select(types[i] == type);
                bar.refresh();
            }
            this.leftlist.relayout();
            this.leftlist.resizeDelta();
        }
    }
}
