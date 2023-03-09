using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.player
{
    public class OpenRoomListBar : ScrollBar
    {
        /// <summary>
        /// 
        /// </summary>
        public RoomEntry roomEntry;

        private int index;

        //UI
        private Text number;

        private Text room_id;

        private Text room_stauts;

        //大赢家数据
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerHeadView playerHeadView;

        private Text winer_id;

        private Text winer_score;

        private Transform winer;

        /// <summary>
        /// 分享房间
        /// </summary>
        public UnrealTextScaleButton share;

        private Image share_bg;

        /// <summary>
        /// 解散房间
        /// </summary>
        public UnrealScaleButton disband;


        protected override void xinit()
        {
            this.number = this.transform.Find("number").GetComponent<Text>();
            this.room_id = this.transform.Find("roomid").GetComponent<Text>();
            this.room_stauts = this.transform.Find("roomstatus").Find("text").GetComponent<Text>();

            this.winer = this.transform.Find("winer");
            this.playerHeadView = this.winer.GetComponent<PlayerHeadView>();
            this.playerHeadView.init();
            this.winer_id = this.winer.Find("id").GetComponent<Text>();
            this.winer_score = this.winer.Find("score").GetComponent<Text>();

            this.share = this.transform.Find("share").GetComponent<UnrealTextScaleButton>();
            this.share.init();
            this.share_bg = this.share.transform.Find("bg").GetComponent<Image>();
            this.disband = this.transform.Find("disbind_btn").GetComponent<UnrealScaleButton>();
        }

        protected override void xrefresh()
        {
            this.number.text = (index + 1) + "(" + roomEntry.matchCount + "局)";
            this.room_id.text = roomEntry.roomid + "";
            this.room_stauts.text = roomEntry.getStatusName();

            if (roomEntry.isStatus(RoomEntry.STATUS_INIT)|| roomEntry.isStatus(RoomEntry.STATUS_WAIT)||roomEntry.isStatus(RoomEntry.STATUS_TIMEOUT)) 
            {
                this.disband.setVisible(true);

                this.share.setState(UnrealButton.NORMAL);
                this.share.setVisible(true);
            }
            else
            {
                this.disband.setVisible(false);
                this.share.setState(UnrealButton.DISABLE);
                this.share.setVisible(false);
            }

            if (roomEntry.player == null)
            {
                this.winer.gameObject.SetActive(false);
            }
            else
            {
                this.winer.gameObject.SetActive(true);
                this.winer_id.text = "ID:" + roomEntry.player.userid;
                this.winer_score.text = "分数:" + roomEntry.player.score;

                playerHeadView.setData(roomEntry.player.headurl, roomEntry.player.userid);
                playerHeadView.refresh();

            }
        }

        public void setRoomEntry(RoomEntry roomEntry, int index)
        {
            this.roomEntry = roomEntry;
            this.index = index;
        }
    }
}
