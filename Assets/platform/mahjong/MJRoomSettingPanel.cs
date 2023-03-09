using basef.setting;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将房间设置界面
    /// </summary>
    public class MJRoomSettingPanel: SettingPanel
    {
        private string[] bgnames = { "经典绿", "经典蓝" };

        private UnrealTableContainer deskbgContainer;
        protected override void xinit()
        {
            base.xinit();
            deskbgContainer = center.Find("desktop").GetComponent<UnrealTableContainer>();
            deskbgContainer.init();

            quit = center.Find("btns").Find("logout").GetComponent<UnrealScaleButton>();
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            deskbgContainer.resize(2);

            int type = MJSettingManager.getDeskTop();
            for (int i = 0; i < deskbgContainer.size; i++)
            {
                MJSettingDeskBgBar bar = (MJSettingDeskBgBar)deskbgContainer.bars[i];
                bar.index_space = i;
                bar.setData(type == i, bgnames[i]);
                bar.refresh();
            }

            backLobby.setVisible(false);
            if (Room.room.isType(Room.STATUS_INIT) || Room.room.isType(Room.STATUS_READY) || Room.room.isType(Room.STATE_MATCH))
            {
                backLobby.setVisible(true);
                if (Room.room.roomRule.getGameNum() > -1)
                {
                    backLobby.setVisible(false);
                }
            }
        }

        public void selectedDeskTop(int index)
        {
            for (int i = 0; i < deskbgContainer.size; i++)
            {
                MJSettingDeskBgBar bar = (MJSettingDeskBgBar) this.deskbgContainer.bars[i];
                bar.setData(i==index, bgnames[index]);
            }
        }


        public void selectedCardBg(int index)
        {
           
        }

        public void selectedCardDi(int index)
        {
            
        }
    }
}
