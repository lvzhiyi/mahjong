using basef.player;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    public class PlayerBar : UnrealLuaSpaceObject
    {
        [HideInInspector] public SimplePlayer player;

        /// <summary>
        /// 玩家名字
        /// </summary>
        protected UnrealTextField text;

        /// <summary>
        /// 玩家id
        /// </summary>
        protected UnrealTextField id;

        /// <summary>
        /// 玩家积分
        /// </summary>
        public UnrealTextField point;

        protected Transform banker;

        private Image xiaojia;

        Transform master;
        Transform voice;

        private Image sound_2;
        private Image sound_3;

        /// <summary>
        /// 分数
        /// </summary>
        protected long score;

        /// <summary>
        /// 头像
        /// </summary>
        protected PlayerHeadView playerHeadView;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("text") != null)
                this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
            if (this.transform.Find("id") != null)
                this.id = this.transform.Find("id").GetComponent<UnrealTextField>();
            if (this.transform.Find("point") != null)
                this.point = this.transform.Find("point").GetComponent<UnrealTextField>();
            this.banker = this.transform.Find("banker");
            if (this.transform.Find("xiaojia") != null)
                this.xiaojia = this.transform.Find("xiaojia").GetComponent<Image>();
            this.master = this.transform.Find("master");
            if (this.master != null)
                this.master.gameObject.SetActive(false);
            this.voice = this.transform.Find("voice");
            if (this.voice != null)
            {
                this.voice.gameObject.SetActive(false);
                this.sound_2 = voice.Find("2").GetComponent<Image>();
                this.sound_3 = voice.Find("3").GetComponent<Image>();
            }
            if (this.transform.Find("info") != null)
            {
                this.playerHeadView = this.transform.Find("info").GetComponent<PlayerHeadView>();
                this.playerHeadView.init();
            }
        }

        public void setPlayer(SimplePlayer player, long score)
        {
            this.player = player;
            this.score = score;
        }

        public SimplePlayer getPlayer()
        {
            return this.player;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if(this.text!=null)
                this.text.text = this.player.name;
            if (this.id != null)
                this.id.text = "ID:" + this.player.userid;
            if (this.point != null)
            {
                this.point.text = MathKit.getRound2LongStr(score);
            }

            if (this.banker != null)
                this.banker.gameObject.SetActive(false);
            if (this.xiaojia != null)
                this.xiaojia.gameObject.SetActive(false);
            playerHeadView.setDatas(this.player.head, player.userid,player.sex);
            playerHeadView.refresh();
        }


        public void showDang()
        {
            if (this.banker != null)
                this.banker.gameObject.SetActive(true);
        }

        public void showXiaoJia()
        {
            if (this.xiaojia != null)
                this.xiaojia.gameObject.SetActive(true);
        }

        public void hideDang()
        {
            if (this.banker != null)
                this.banker.gameObject.SetActive(false);
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
