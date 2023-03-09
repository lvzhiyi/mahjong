using basef.im;
using cambrian.common;
using platform.spotred.waitroom;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class ExpressionPlayerView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 短语
        /// </summary>
        private MatchIMQuickMSgBar msgBar;
        /// <summary>
        /// 表情
        /// </summary>
        private MatchIMQuickMSgBar emojiBar;

        Transform voice;
        private Image sound_2;
        private Image sound_3;

        protected override void xinit()
        {
            this.msgBar = this.transform.Find("quickmsg").GetComponent<MatchIMQuickMSgBar>();
            this.msgBar.init();

            this.emojiBar = this.transform.Find("emoji").GetComponent<MatchIMQuickMSgBar>();
            this.emojiBar.init();

            this.voice = this.transform.Find("voice");
            if (this.voice != null)
            {
                this.voice.gameObject.SetActive(false);
                this.sound_2 = voice.Find("2").GetComponent<Image>();
                this.sound_3 = voice.Find("3").GetComponent<Image>();
            }
        }

        protected override void xrefresh()
        {
           this.msgBar.setVisible(false);
            this.emojiBar.setVisible(false);
        }

        public void playQuickMsg(IMQuickMsg msg)
        {
            if (msg.type==IMQuickMsg.TYPE_TEXT)
            {
                this.msgBar.setIMQuickMsg(msg);
                this.msgBar.refresh();
                this.msgBar.setVisible(true);
            }
            else
            {
                this.emojiBar.setIMQuickMsg(msg);
                this.emojiBar.refresh();
                this.emojiBar.setVisible(true);
            } 
        }

        public void setIMTextMsg(IMTextMsg msg)
        {
            this.msgBar.showIMTextMsg(msg);
            this.msgBar.setVisible(true);
        }



        private int curr_sound_index = 0;


        private bool isShow = false;

        public void showVoice(bool isShow)
        {
            if (this.voice != null)
            {
                this.isShow = isShow;
                this.voice.gameObject.SetActive(isShow);
                if (isShow)
                {
                    sound_2.gameObject.SetActive(false);
                    sound_3.gameObject.SetActive(false);
                    curr_sound_index = 1;
                    cut_time = 0;

                }
            }

            totaltime = -1;
        }

        public void InvokeSound()
        {
            if (curr_sound_index == 1)
            {
                sound_2.gameObject.SetActive(true);
            }
            else if (curr_sound_index == 2)
            {
                sound_3.gameObject.SetActive(true);
            }
            else if (curr_sound_index == 3)
            {
                sound_2.gameObject.SetActive(false);
                sound_3.gameObject.SetActive(false);
                curr_sound_index = 0;
            }
            curr_sound_index++;
        }

        private long cut_time;

        /// <summary>
        /// 播放语音开始时间
        /// </summary>
        private long totaltime = -1;


        protected override void xupdate()
        {
            if (this.voice == null) return;

            if (isShow)
            {
                long time = TimeKit.currentTimeMillis;

                if (totaltime == -1)
                    totaltime = time;

                if (time - totaltime > 12000)
                    isShow = false;

                if (cut_time == 0)
                {
                    cut_time = time;
                    return;
                }


                if (time - cut_time > 400)
                {
                    InvokeSound();
                    cut_time = time;
                }


            }
            else
            {
                this.voice.gameObject.SetActive(isShow);
            }
        }
    }
}
