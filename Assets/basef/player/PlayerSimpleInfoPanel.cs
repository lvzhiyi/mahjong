using cambrian.common;
using platform.spotred.waitroom;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.player
{
    public class PlayerSimpleInfoPanel:UnrealLuaPanel
    {
        //UI

        private Text nickname;

        private Text userid;

        //private Text ip;

        private PlayerHeadView headView;


        protected override void xinit()
        {
            this.headView = this.transform.Find("Canvas").Find("content").Find("top").Find("head").GetComponent<PlayerHeadView>();
            this.headView.init();
            this.nickname = this.transform.Find("Canvas").Find("content").Find("top").Find("name").GetComponent<Text>();
            this.userid = this.transform.Find("Canvas").Find("content").Find("top").Find("id").GetComponent<Text>();
            //this.ip = this.transform.Find("Canvas").Find("content").Find("top").Find("ip").GetComponent<Text>();
        }

        public void setIp(string ip)
        {
           // this.ip.text = ip;
        }

        protected override void xrefresh()
        {
           
            this.userid.text = Player.player.userid+"";
            this.nickname.text = Player.player.name+"";
            headView.setData(Player.player.head,Player.player.userid);
            headView.refresh();
        }

    }
}
