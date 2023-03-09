using basef.player;
using UnityEngine;
using UnityEngine.UI;

namespace platform
{
    public class RoomPlayerInvitePanel : UnrealLuaPanel
    {

        PlayerCircleHeadView head;

        Text nameinfo;

        Text wfinfo;

        Text roomid;

        Text titles;

        UnrealScaleButton refuse;
        /// <summary>
        /// 勾
        /// </summary>
        public Image normal { get; set; }

        /// <summary>
        /// 类型(传入的值)
        /// </summary>
        public int type { get; set; }

        public RoomAgainGame againGame { get; set; }
        
        public RoomInviteInfo inviteInfo { get; set; }


        protected override void xinit()
        {
            Transform infopanel = transform.Find("Canvas").Find("content").Find("infopanel");

            titles = transform.Find("Canvas").Find("content").Find("title").GetComponent<Text>();

            refuse = transform.Find("Canvas").Find("content").Find("down").Find("refuse").GetComponent<UnrealScaleButton>();

            normal = transform.Find("Canvas").Find("content").Find("down").Find("refuse").Find("normal1").GetComponent<Image>();

            head = infopanel.Find("head").GetComponent<PlayerCircleHeadView>();

            head.init();

            nameinfo = infopanel.Find("nameinfo").GetComponent<Text>();

            wfinfo = infopanel.Find("wftitle").Find("text").GetComponent<Text>();

            roomid = infopanel.Find("roomnum").Find("text").GetComponent<Text>();
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public void setRoomAgainGame(RoomAgainGame againGame)
        {
            this.againGame = againGame;
        }

        public void setRoomInviteInfo(RoomInviteInfo inviteInfo)
        {
            this.inviteInfo = inviteInfo;
        }

        protected override void xrefresh()
        {
            normal.gameObject.SetActive(false);

            if (type == 1)
            { //邀请

                nameinfo.text = "你收到来自亲友圈【" + inviteInfo.clubname + "(" + inviteInfo.clubid + ")】的好友【" + inviteInfo.name + "】的游戏邀请。";

                head.setData(inviteInfo.head, inviteInfo.userid);

                head.refresh();

                wfinfo.text = inviteInfo.rule.rule.getPlayRule(true);

                roomid.text = inviteInfo.roomid + "";

                refuse.setVisible(true);

                titles.text = "游戏邀请";
            }
            else if (type==3)
            {
                nameinfo.text = "你收到来自休闲场【" + inviteInfo.clubname + "(" + inviteInfo.clubid + ")】的好友【" + inviteInfo.name + "】的游戏邀请。";

                head.setData(inviteInfo.head, inviteInfo.userid);

                head.refresh();

                wfinfo.text = inviteInfo.rule.rule.getPlayRule(true);

                roomid.text = inviteInfo.roomid + "";

                refuse.setVisible(false);

                titles.text = "游戏邀请";
            }
            else
            {
                //再来一局
                nameinfo.text = "玩家【" + againGame.name + "】邀请你再来一局。";

                head.setData(againGame.head, againGame.userid);

                head.refresh();

                wfinfo.text = againGame.rule.getPlayRule(true);

                roomid.text = againGame.roomid + "";

                refuse.setVisible(false);

                titles.text = "再来一局";//显示再来一局
            }
        }
    }
}
