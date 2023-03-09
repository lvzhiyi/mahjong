using platform.spotred;
using basef.player;
using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    public class IpMatchPlayerBar: UnrealLuaSpaceObject
    {
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView headView;
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;
        /// <summary>
        /// ip
        /// </summary>
        private Text ip;
        /// <summary>
        /// 
        /// </summary>
        private MatchPlayer player;

        private Image gps_icon;


        protected override void xinit()
        {
            this.headView = this.transform.GetComponent<PlayerCircleHeadView>();
            this.headView.init();
            this.nickname = this.transform.Find("name").GetComponent<Text>();
            this.ip = this.transform.Find("ip").GetComponent<Text>();
            this.gps_icon = this.transform.Find("gps").GetComponent<Image>();
        }

        public void setMatchPlayer(MatchPlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            this.nickname.text = player.player.name;
            this.ip.text = player.ip;

            if (player.gps_latitude == 0)
            {
                this.gps_icon.gameObject.SetActive(true);
            }
            else
            {
                this.gps_icon.gameObject.SetActive(false);
            }

            //headView.setDatas(player.player.head,player.userid,player.player.sex);
            headView.setData(player.player.head, player.userid);
            headView.refresh();
        }
    }
}
