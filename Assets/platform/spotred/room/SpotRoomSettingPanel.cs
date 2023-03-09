using basef.setting;
using System;

namespace platform.spotred.room
{
    public class SpotRoomSettingPanel:SettingPanel
    {
        private UnrealTableContainer container;

        string[] desktop=new string[] {"舒适黄","奢华绿","木纹黄","清新绿"};

        private Action callback;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.center.Find("desktop").GetComponent<UnrealTableContainer>();
            this.container.init();

            quit = center.Find("btns").Find("logout").GetComponent<UnrealScaleButton>();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.container.resize(desktop.Length);

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

        public void setCallBack(Action action)
        {
            this.callback = action;
        }

        public void executeAction()
        {
            this.callback();
        }

        public void selectedDeskTop(int index)
        {
            for (int i = 0; i < desktop.Length; i++)
            {
                DeskTopBar bar = (DeskTopBar)this.container.bars[i];
                if (index == bar.index_space)
                    bar.checkedImg(true);
                else
                    bar.checkedImg(false);
            }
        }
    }
}
