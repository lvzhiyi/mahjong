using cambrian.game;
using platform;
using scene.game;
using UnityEngine.UI;

namespace basef.setting
{
    /// <summary>
    /// 设置面板  音效和音量
    /// </summary>
    public class SettingPanel : UnrealViewPanel
    {

        public const int TYPE_OLD = 0, TYPE_NEW = 1;

        Slider musicbar;

        Slider soundbar;

        protected UnrealScaleButton quit;

        UnrealTextField versions;

        UnrealCheckBox setgps;

        /// <summary>
        /// 返回大厅界面按钮(具体游戏的设置界面在使用)
        /// </summary>
        protected UnrealScaleButton backLobby;

        protected override void xinit()
        {
            base.xinit();
            this.musicbar = this.center.Find("music").Find("slider").GetComponent<Slider>();
            this.musicbar.value = SoundManager.getMusicVolume();

            this.soundbar = this.center.Find("sound").Find("slider").GetComponent<Slider>();
            this.soundbar.value = SoundManager.getSoundVolume();
            this.versions = this.center.Find("versions").GetComponent<UnrealTextField>();
            this.versions.init();
            if (this.center.Find("logout") != null)
            {
                this.quit = this.center.Find("logout").GetComponent<UnrealScaleButton>();
                this.quit.setVisible(false);
            }

            if (this.center.Find("setgps") != null)
            {
                this.setgps = this.center.Find("setgps").GetComponent<UnrealCheckBox>();
                this.setgps.init();
            }
            if(center.Find("btns")!=null)
                backLobby = center.Find("btns").Find("backlobby").GetComponent<UnrealScaleButton>();

            this.resizeDelta(new Margin());
        }

        public virtual void showQuit(bool b)
        {
            this.quit.setVisible(b);
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
            this.versions.text = SpotRedRoot.root.versions;
            if (this.setgps != null)
            {
                bool b = WXManager.getInstance().isOpenGPS();
                this.setgps.setState(b);
                this.setgps.refresh();
                if (b)
                {
                    this.setgps.transform.Find("text").GetComponent<Text>().text = "已打开GPS";
                }
                else
                {
                    this.setgps.transform.Find("text").GetComponent<Text>().text = "打开GPS";
                }
            }
        }
    }
}
