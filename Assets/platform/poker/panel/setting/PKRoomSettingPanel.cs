using cambrian.game;
using UnityEngine.UI;

namespace platform.poker
{
    /// <summary> 设置面板 </summary>
    public class PKRoomSettingPanel : UnrealViewPanel
    {
        public const int TYPE_OLD = 0, TYPE_NEW = 1;

        private Slider musicbar;

        private Slider soundbar;

        private UnrealScaleButton quit;

        private UnrealTextField versions;

        private UnrealCheckBox setgps;

        private UnrealTableContainer deskbgContainer;

        private UnrealTableContainer cardContainer;

        private UnrealTableContainer cardbgContainer;

        /// <summary>
        /// 返回大厅界面按钮(具体游戏的设置界面在使用)
        /// </summary>
        protected UnrealScaleButton backLobby;

        protected override void xinit()
        {
            base.xinit();

            var contents = content.Find("content");

            musicbar = center.Find("music").Find("slider").GetComponent<Slider>();
            musicbar.value = SoundManager.getMusicVolume();

            soundbar = center.Find("sound").Find("slider").GetComponent<Slider>();
            soundbar.value = SoundManager.getSoundVolume();

            if (contents.Find("other").Find("versions"))
            {
                versions = contents.Find("other").Find("versions").GetComponent<UnrealTextField>();
                versions.init();
            }

            if (contents.Find("other").Find("logout"))
            {
                quit = contents.Find("other").Find("logout").GetComponent<UnrealScaleButton>();
                quit.setVisible(false);
            }

            if (contents.Find("other").Find("setgps"))
            {
                setgps = contents.Find("other").Find("setgps").GetComponent<UnrealCheckBox>();
                setgps.init();
            }

           
            backLobby = contents.Find("other").Find("btns").Find("backlobby").GetComponent<UnrealScaleButton>();
            quit = contents.Find("other").Find("btns").Find("logout").GetComponent<UnrealScaleButton>();

            resizeDelta(new Margin());
        }

        public void showQuit(bool b)
        {
            this.quit.setVisible(false);
            var room = Room.room;
            if (room != null)
            {
                if (room.isType(Room.CLUB) || room.isType(Room.ARENA))
                {
                    if (room.canDisbandCount == 0)
                    {
                        //>-1 代表已经开始游戏
                        quit.setVisible(!(room.roomRule.getGameNum() > -1));
                    }
                    else
                    {
                        if (!room.isType(Room.JBC))
                        {
                            quit.setVisible(true);
                        }
                    }
                }
                else
                {
                    quit.setVisible(true);
                }
            }
        }

        public void setMusicVolume()
        {
            SoundManager.setMusicVolume(this.musicbar.value);
        }

        public void setSoundVolume()
        {
            SoundManager.setSoundVolume(this.soundbar.value);
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            versions.text = UnrealRoot.root.versions;
            if (setgps != null)
            {
                setgps.setState(WXManager.getInstance().isOpenGPS());
                setgps.refresh();
                setgps.transform.Find("text").GetComponent<Text>().text = WXManager.getInstance().isOpenGPS() ? "已打开GPS" : "打开GPS";
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
    }
}
