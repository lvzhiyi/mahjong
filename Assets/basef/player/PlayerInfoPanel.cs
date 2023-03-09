using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.player
{
    public class PlayerInfoPanel:UnrealLuaPanel
    {

        public const int PROMOTER=1,PROMOTEROVER=2;

        private ScrollContainer container;

        private ArrayList<RoomEntry> list;

        public int room_count;

        public bool isOverRoomList;

        public int index_over_room_list;

        //UI
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerHeadView playerHeadView;

        private Text nickname;

        private UnrealTextField userid;

        private UnrealTextField ip;


        public UnrealScaleButton promoteButton;

        private Text promote_active;

        private Text promote_normal;

        public UnrealScaleButton promoteOverButton;

        private Text promoteOver_active;

        private Text promoteOver_normal;

        protected override void xinit()
        {
            this.container = this.transform.Find("Canvas").Find("content").Find("container").GetComponent<ScrollContainer>();
            this.container.init();

            this.playerHeadView=this.transform.Find("Canvas").Find("content").Find("top").Find("head")
                .GetComponent<PlayerHeadView>();
            this.playerHeadView.init();
            this.nickname = this.transform.Find("Canvas").Find("content").Find("top").Find("name").GetComponent<Text>();
            this.userid = this.transform.Find("Canvas").Find("content").Find("top").Find("id").GetComponent<UnrealTextField>();
            this.ip = this.transform.Find("Canvas").Find("content").Find("top").Find("ip").GetComponent<UnrealTextField>();

            this.promoteButton =
                this.transform.Find("Canvas")
                    .Find("content")
                    .Find("center_info")
                    .Find("btn")
                    .Find("promoter")
                    .GetComponent<UnrealScaleButton>();
            this.promote_active = this.promoteButton.transform.Find("text").GetComponent<Text>();
            this.promote_normal = this.promoteButton.transform.Find("text_1").GetComponent<Text>();
            this.promoteOverButton =
               this.transform.Find("Canvas")
                   .Find("content")
                   .Find("center_info")
                   .Find("btn")
                   .Find("promoterover")
                   .GetComponent<UnrealScaleButton>();
            this.promoteOver_active = this.promoteOverButton.transform.Find("text").GetComponent<Text>();
            this.promoteOver_normal = this.promoteOverButton.transform.Find("text_1").GetComponent<Text>();
        }


        public void setData(ArrayList<RoomEntry> list,string ip,bool isOverRoomList)
        {
            this.list = list;
            this.ip.text = ip;
            this.isOverRoomList = isOverRoomList;
            this.index_over_room_list = 0;
        }

        public void addOpenRoomList(ArrayList<RoomEntry> list)
        {
            this.list.add(list.toArray());
            this.container.incrResize(this.list.count);
        }

        public void setIndex(int index)
        {
            this.index_over_room_list = index;
        }

        protected override void xrefresh()
        {
            container.updateData(callback);
            container.resize(list.count);
            this.userid.text = "" + Player.player.userid;
            this.nickname.text = "昵称:" + Player.player.name;

            playerHeadView.setData(Player.player.head,Player.player.userid);
            playerHeadView.refresh();
        }

        /// <summary>
        /// 禁用当前选中的按钮
        /// </summary>
        public void disabelCurrActiveButton(int type)
        {
            this.promoteButton.setState(UnrealButton.NORMAL);
            this.promoteOverButton.setState(UnrealButton.NORMAL);

            this.promote_active.gameObject.SetActive(false);
            this.promote_normal.gameObject.SetActive(false);
            this.promoteOver_normal.gameObject.SetActive(false);
            this.promoteOver_active.gameObject.SetActive(false);


            switch (type)
            {
                case PROMOTER:
                    this.promoteButton.setState(UnrealButton.DISABLE);
                    this.promote_active.gameObject.SetActive(true);
                    this.promoteOver_normal.gameObject.SetActive(true);
                    break;
                case PROMOTEROVER:
                    this.promoteOverButton.setState(UnrealButton.DISABLE);
                    this.promote_normal.gameObject.SetActive(true);
                    this.promoteOver_active.gameObject.SetActive(true);
                    break;
            }
        }

        public void callback(ScrollBar bar,int index)
        {
            OpenRoomListBar roombar=(OpenRoomListBar)bar;

            roombar.setRoomEntry(list.get(index),index);
            roombar.refresh();
           
            if (isOverRoomList&&(list.count-1)==index)
            {
                if (list.count >= room_count) return;
                index_over_room_list += 1;
                new PlayerGetMoreOverRoomProcess(index_over_room_list).execute();
            }
        }
    }
}
