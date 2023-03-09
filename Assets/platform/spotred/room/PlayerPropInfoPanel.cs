using basef.player;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class PlayerPropInfoPanel : UnrealLuaPanel
    {
        private Text nickname;

        private UnrealTextField ip;

        private Text playerid;

        /// <summary>
        /// 头像
        /// </summary>
        private PlayerHeadView playerHeadView;

        //UnrealTableContainer container;

        [HideInInspector] public MatchPlayer player;

        //public int[] props=new int[]{13001,13002,13004,13005};

        protected override void xinit()
        {
            base.xinit();

            Transform player = this.content.Find("content").Find("player");
            this.playerHeadView =player.Find("info").GetComponent<PlayerHeadView>();
            this.playerHeadView.init();

            this.nickname = player.Find("name").GetComponent<Text>();
            this.ip = player.Find("ip").GetComponent<UnrealTextField>();
            this.playerid=player.Find("id").GetComponent<Text>();


            //this.container = this.content.Find("content").Find("props").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            //this.container.init();
        }

        public void setMatchPlayer(MatchPlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            this.nickname.text = player.player.name;
            this.ip.text = player.ip;
            this.playerid.text = player.player.userid+"";

            playerHeadView.setDatas(player.player.head,player.player.userid,player.player.sex);
            playerHeadView.refresh();

            //Prop[] p=new Prop[props.Length];
            //for (int i = 0; i < props.Length; i++)
            //{
            //    p[i] = (Prop)Sample.factory.getSample(props[i]);
            //}
            
            //this.container.cols = props.Length;
            //this.container.resize(props.Length);

            //for (int i=0;i< props.Length;i++)
            //{
            //    PlayerPropInfoBar bar=(PlayerPropInfoBar)this.container.bars[i];
            //    bar.setProp(p[i]);
            //    bar.refresh();
            //}
            //this.container.resizeDelta();
        }
    }
}
