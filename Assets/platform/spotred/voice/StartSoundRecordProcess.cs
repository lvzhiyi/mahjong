using UnityEngine;
using UnityEngine.EventSystems;

namespace platform.spotred.voice
{
    public class StartSoundRecordProcess : UnrealLuaSpaceObject, IPointerDownHandler, IPointerUpHandler
    {

        /// <summary>
        /// 状态 不可用,一般,激活
        /// </summary>
        public const int DISABLE = -1, NORMAL = 0, ACTIVED = 1;

        /// <summary>
        /// 一般
        /// </summary>
        protected Transform normal;

        /// <summary>
        /// 激活
        /// </summary>
        protected Transform actived;
        /// <summary>
        /// 不可用的
        /// </summary>
        protected Transform disabled;
        /// <summary>
        /// 按钮状态
        /// </summary>
        [HideInInspector]
        public int state;

        VoiceView vview;

        void Awake()
        {
            this.init();
        }

        protected override void xinit()
        {
            this.normal = this.transform.Find("normal");
            if (this.normal == null)
                this.normal = this.transform;
            this.actived = this.transform.Find("actived");
            if (this.actived == null)
                this.actived = this.normal;
            this.disabled = this.transform.Find("disabled");
            if (this.disabled == null)
                this.disabled = this.normal;

            this.vview = this.transform.parent.parent.Find("voiceview").GetComponent<VoiceView>();
        }

        public virtual void setState(int state)
        {
            if (this.state == DISABLE || state == DISABLE)
            {
                MouseClickProcess[] doors = this.GetComponents<MouseClickProcess>();
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].enabled = state != DISABLE;
                }
            }
            this.state = state;

            if (this.normal == null) return;
            switch (state)
            {
                case ACTIVED:
                    this.disabled.gameObject.SetActive(false);
                    this.normal.gameObject.SetActive(false);
                    this.actived.gameObject.SetActive(true);
                    break;
                case DISABLE:
                    this.actived.gameObject.SetActive(false);
                    this.normal.gameObject.SetActive(false);
                    this.disabled.gameObject.SetActive(true);
                    break;
                case NORMAL:
                    this.actived.gameObject.SetActive(false);
                    this.disabled.gameObject.SetActive(false);
                    this.normal.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           
            if (this.state == ACTIVED || this.state == DISABLE)
            {
                return;
            }
            this.setState(ACTIVED);
            string[] devicess = Microphone.devices;

            if (Microphone.devices.Length == 0)
            {
                Alert.show("麦克风权限被禁用，请打开");
                return;
            }
            
            this.vview.soundrecord(Room.room.getRoomIndex());
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            //一般状态
            if (this.state == NORMAL || this.state == DISABLE) return;
            this.setState(NORMAL);
            this.vview.soundrecordOver();
        }
    }
}
