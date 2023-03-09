using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace platform.poker
{
    public class DDZMultipleInfoBar : ScrollBar
    {
        private UnrealTextField username;

        private UnrealTextField lord;

        private UnrealTextField boor;

        private UnrealTextField publics;

        private UnrealTextField gross;

        private Image lordIcon;

        protected override void xinit()
        {
            username = transform.Find("username").GetComponent<UnrealTextField>();
            username.init();

            lord = transform.Find("dizhu").GetComponent<UnrealTextField>();
            lord.init();

            boor = transform.Find("boor").GetComponent<UnrealTextField>();
            boor.init();

            publics = transform.Find("public").GetComponent<UnrealTextField>();
            publics.init();

            gross = transform.Find("gross").GetComponent<UnrealTextField>();
            gross.init();

            lordIcon = transform.Find("dizhuicon").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            lordIcon.gameObject.SetActive(isLord);
            username.text = playername;
            lord.text = "" + dizhuP;
            gross.text = "" + grossP;
            publics.text = "X" + publicP;

            if (isLord)
            {
                string str = "";
                for (int i = 0; i < DDZMatch.match.playerCount; i++)
                {
                    if (DDZMatch.match.players[i] != DDZMatch.match.players[DDZMatch.match.diZhuIndex])
                    {
                        str = str + DDZMatch.match.multipleBean.boorPoint[i] + "+";
                    }
                }
                boor.text = "X(" + str.Substring(0,str.Length - 1) + ")";
            }
            else boor.text = "X" + boorP;
        }

        private string playername;
        private int boorP;
        private int dizhuP;
        private int grossP;
        private int publicP;
        private bool isLord;

        public void setData(string name,int dizhuPoint,int boorPoint,int allPoint,int gross,bool isDiZhu)
        {
            playername = name;
            dizhuP = dizhuPoint;
            boorP = boorPoint;
            publicP = allPoint;
            grossP = gross;
            isLord = isDiZhu;
        }
    }
}
