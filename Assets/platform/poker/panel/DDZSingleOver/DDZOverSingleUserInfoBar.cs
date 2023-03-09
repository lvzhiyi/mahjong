using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;
using platform.spotred;
using UnityEngine.UI;

namespace platform.poker
{
    public class DDZOverSingleUserInfoBar : ScrollBar
    {
        private UnrealTextField socre;

        private UnrealTextField bet;

        private UnrealTextField mutiple;

        private UnrealTextField userName;

        private UnrealTextField userID;

        private PlayerHeadView head;

        //private SpritesList bg;

        private OverSingleAllCards cardsmanager;

        private Image dizhu;

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

            head = transform.Find("userHead").GetComponent<PlayerHeadView>();
            head.init();

            //bg = transform.Find("bg").GetComponent<SpritesList>();

            cardsmanager = transform.Find("allCards").GetComponent<OverSingleAllCards>();
            cardsmanager.init();

            dizhu = transform.Find("dizhu").GetComponent<Image>();
        }

        public void refreshOverUserInfo(string rSocre, string rBet, string rMutiple, MatchPlayer user, int[] use, int[] nouse)
        {
            //bg.ShowIndex(user.userid == Player.player.userid ? 0 : 1, false);
            socre.text = MathKit.getRound2LongStr(long.Parse(rSocre));
            socre.color = ColorKit.getColor(int.Parse(rSocre) >= 0 ? orange : green);
            bet.text = MathKit.getRound2LongStr(long.Parse(rBet));
            mutiple.text = rMutiple;
            userName.text = user.player.name;
            userID.text = user.userid.ToString();
            head.setData(user.player.head, user.userid);
            head.refresh();

            if (DDZMatch.match.diZhuIndex >= 0)
            {
                dizhu.gameObject.SetActive(Room.room.players[DDZMatch.match.diZhuIndex].userid == user.userid);
            }
            else dizhu.gameObject.SetActive(false);

            cardsmanager.setData(use, nouse);                        //刷新数据
            cardsmanager.refresh();
        }
    }
}
