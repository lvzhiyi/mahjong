using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;
using platform.spotred;
using UnityEngine.UI;

namespace platform.poker
{
    public class PDKOverSingleUserInfoBar : ScrollBar
    {
        /// <summary> 加分 减分 </summary>
        private UnrealTextField socre;

        /// <summary> 底分 </summary>
        private UnrealTextField bet;

        /// <summary> 倍数 </summary>
        private UnrealTextField mutiple;

        /// <summary> 玩家名字 </summary>
        private UnrealTextField userName;

        /// <summary> 玩家ID </summary>
        private UnrealTextField userID;

        /// <summary> 剩余牌数 </summary>
        private UnrealTextField cardnum;

        /// <summary> 玩家头像 </summary>
        private PlayerHeadView head;

        /// <summary> 玩家背景 </summary>
        //private SpritesList bg;

        /// <summary> 春天 被关门 </summary>
        private Image chuntian;
        /// <summary> 红桃十抓鸟 </summary>
        private Image bird;

        private OverSingleAllCards cardsmanager;

        private string green = "#8DA92A", orange = "#EA7243";

        protected override void xinit()
        {
            socre = transform.Find("socre").GetComponent<UnrealTextField>();
            socre.init();

            bet = transform.Find("difen").GetComponent<UnrealTextField>();
            bet.init();

            mutiple = transform.Find("mutiple").GetComponent<UnrealTextField>();
            mutiple.init();

            userName = transform.Find("userName").GetComponent<UnrealTextField>();
            userName.init();

            userID = transform.Find("userID").GetComponent<UnrealTextField>();
            userID.init();

            //cardnum = transform.Find("cardnum").GetComponent<UnrealTextField>();
            //cardnum.init();

            head = transform.Find("userHead").GetComponent<PlayerHeadView>();
            head.init();

            //bg = transform.Find("bg").GetComponent<SpritesList>();

            cardsmanager = transform.Find("allCards").GetComponent<OverSingleAllCards>();
            cardsmanager.init();

            chuntian = transform.Find("chuntian").GetComponent<Image>();

            bird = transform.Find("bird").GetComponent<Image>();
        }

        /// <summary> 刷新结算界面信息 </summary>
        public void refreshOverUserInfo(int bird10Index,string rSocre, string rBet, string rMutiple, MatchPlayer user, bool ischuntian, string num, int[] use, int[] nouse)
        {
            //bg.ShowIndex(user.userid == Player.player.userid ? 0 : 1, false);
            socre.text = MathKit.getRound2LongStr(long.Parse(rSocre));
            socre.color = ColorKit.getColor(long.Parse(rSocre) >= 0 ? orange : green);
            bet.text = MathKit.getRound2LongStr(long.Parse(rBet));
            bool b = Room.room.getRule().getIntAtribute("multipScore")==0;
            long mutiplen = long.Parse(rMutiple);
            mutiple.text = b ? mutiplen.ToString() : MathKit.getRound2LongStr(mutiplen);
            userName.text = user.player.name;
            userID.text = user.userid.ToString();
            head.setData(user.player.head, user.userid);
            head.refresh();
            //cardnum.text = num;
            chuntian.gameObject.SetActive(ischuntian);
            bool redbird = Room.room.getRule().getRuleAtribute("birdDouble");
            bird.gameObject.SetActive(redbird && bird10Index == index_space);

            cardsmanager.setData(use, nouse);                        //刷新数据
            cardsmanager.refresh();
        }
    }
}
