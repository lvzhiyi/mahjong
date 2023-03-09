using basef.im;
using cambrian.common;
using cambrian.game;
using UnityEngine.UI;

namespace platform.spotred
{
    public class VoiceView: UnrealLuaSpaceObject
    {
        private const int VOICE_CD = 0;
        public int roomindex;
        UnrealTextField timeText;
        long longTime; //计时器时间
        int cdtime; //发言冷却时间
        int lasttime; //通话剩余时间
        UnrealLuaSpaceObject view;
        UnrealTextButton startButton;
        
        
        private Image voice_2;
        private Image voice_3;
        private Image voice_4;
        private Image voice_5;


        protected override void xinit()
        {
            base.xinit();
            this.view = this.transform.Find("view").GetComponent<UnrealLuaSpaceObject>();
            this.view.init();
            this.view.setVisible(false);
            this.timeText = this.transform.Find("view").Find("num").GetComponent<UnrealTextField>();
            this.timeText.init();
            this.startButton = this.transform.parent.Find("top").Find("startbutton").GetComponent<UnrealTextButton>();
            this.startButton.init();
            this.gameObject.SetActive(false);
            this.lasttime = -1;

            if (this.transform.Find("view").Find("overbutton").Find("2") != null)
            {
                this.voice_2 = this.transform.Find("view").Find("overbutton").Find("2").GetComponent<Image>();
                this.voice_3 = this.transform.Find("view").Find("overbutton").Find("3").GetComponent<Image>();
                this.voice_4 = this.transform.Find("view").Find("overbutton").Find("4").GetComponent<Image>();
                this.voice_5 = this.transform.Find("view").Find("overbutton").Find("5").GetComponent<Image>();
            }
        }

        protected override void xupdate()
        {
            if (TimeKit.currentTimeMillis - this.longTime >= 1000)
            {
                this.longTime = TimeKit.currentTimeMillis;
                
                if (this.cdtime > 0)
                {
                    this.cdtime--;
                    this.startButton.text.text = this.cdtime + "";
                }
                else
                {
                    this.startButton.text.text = "";
                }

                if (this.lasttime > 0)
                {
                    this.lasttime--;
                    this.timeText.text = this.lasttime + "";
                }
                else
                {
                    this.soundrecordOver();
                }
            }
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        /// <param name="roomindex"></param>
        public void soundrecord(int roomindex)
        {
            if (this.cdtime > 0)
                return;
            if (SoundManager.manager.voices.count > 0)
                return;
            SoundManager.setAllMute(true);
            this.roomindex = roomindex;
            this.lasttime = SoundManager.maxRecordTime;
            this.timeText.text = lasttime + "";
            YunVaImManager.yunva.startRecord();
            this.view.setVisible(true);
            
            refreshVoice();
        }

        private int curr_index = 0;
        /// <summary>
        /// 刷新时间
        /// </summary>
        public void refreshVoice()
        {
            this.voice_2.gameObject.SetActive(false);
            this.voice_3.gameObject.SetActive(false);
            this.voice_4.gameObject.SetActive(false);
            this.voice_5.gameObject.SetActive(false);
            curr_index = 1;
            InvokeRepeating("invokeVoice",0.5f,0.3f);
        }


        public void invokeVoice()
        {
            if (curr_index == 1)
            {
                this.voice_2.gameObject.SetActive(true);
            }
            else if (curr_index==2)
            {
                this.voice_3.gameObject.SetActive(true);
            }
            else if(curr_index==3)
            {
                this.voice_4.gameObject.SetActive(true);
            }
            else if (curr_index==4)
            {
                this.voice_5.gameObject.SetActive(true);
            }
            else if (curr_index==5)
            {
                this.voice_2.gameObject.SetActive(false);
                this.voice_3.gameObject.SetActive(false);
                this.voice_4.gameObject.SetActive(false);
                this.voice_5.gameObject.SetActive(false);
                curr_index = 1;
                return;
            }
            curr_index++;
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void soundrecordOver()
        {
            this.view.setVisible(false);

            SoundManager.setAllMute(false);
            if (this.lasttime == -1)
            {
                return;
            }

            if (SoundManager.maxRecordTime - this.lasttime < 2)
            {
                this.lasttime = -1;
            }

            this.lasttime = -1;
            this.cdtime = VOICE_CD;
            this.startButton.text.text = this.cdtime + "";

            YunVaImManager.yunva.stopRecord(recordPathBack, uploadUrlBack);

            CancelInvoke("invokeVoice");
        }

        void recordPathBack(string path)
        {

        }

        protected virtual void uploadUrlBack(string url)
        {
            //发送后台 
            CommandManager.addCommand(new SendAudioCommand(url));
        }
    }
}
