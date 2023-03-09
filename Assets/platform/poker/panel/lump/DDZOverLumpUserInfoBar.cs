using basef.player;
using cambrian.common;

namespace platform.poker
{
    public class DDZOverLumpUserInfoBar : UnrealLuaSpaceObject
    {
        private SimplePlayer player;

        private UnrealTextField score;

        private PlayerHeadView userInfo;

        private UnrealTextField userName;

        private UnrealTextField userID;

        protected override void xinit()
        {
            this.score = this.transform.Find("score").GetComponent<UnrealTextField>();
            this.score.init();

            this.userInfo = this.transform.Find("head").GetComponent<PlayerHeadView>();
            this.userInfo.init();

            this.userName = this.transform.Find("username").GetComponent<UnrealTextField>();
            this.userName.init();

            this.userID = this.transform.Find("userid").GetComponent<UnrealTextField>();
            this.userID.init();
        }

        public void setData(long score,SimplePlayer player)
        {
            this.player = player;
            bool isSlef = (player.userid == Player.player.userid);

            setTextColor(this.score,getSocre(score),isSlef);
            setTextColor(this.userName,player.name,isSlef);
            setTextColor(this.userID,"ID:" + player.userid,isSlef);

            this.userInfo.setData(player.head,player.userid);
        }

        private string getSocre(long score)
        {
            return score < 0 ? "- " + MathKit.abs(score) : "+ " + MathKit.abs(score);
        }

        private void setTextColor(UnrealTextField textField,string context,bool isSlef)
        {
            textField.text = context;
            textField.color = ColorKit.getColor(isSlef ? "FAE144" : "FFFFFF");
        }

        protected override void xrefresh()
        {

        }
    }
}