using System.Collections.Generic;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.display
{
    [RequireComponent(typeof (Image))]
    [Hotfix]
    public class UnrealSpriteAnimation : MonoBehaviour
    {

        private Image ImageSource;
        private int mCurFrame = 0;
        private float mDelta = 0;
        /// <summary>
        /// 间隔时间(毫秒)
        /// </summary>
        public long intervalTime;

        public float FPS = 5;
        public List<Sprite> SpriteFrames;
        public bool IsPlaying = false;
        public bool Foward = true;
        public bool AutoPlay = false;
        public bool Loop = false;

        public delegate void CallBackOver(object obj);
        /// <summary>
        /// 播放完通知
        /// </summary>
        public CallBackOver over;

        public int FrameCount
        {
            get { return SpriteFrames.Count; }
        }

        void Awake()
        {
            ImageSource = GetComponent<Image>();
        }

        void Start()
        {
            IsPlaying = true;
        }

        private void SetSprite(int idx)
        {
            ImageSource.sprite = SpriteFrames[idx];
            //该部分为设置成原始图片大小，如果只需要显示Image设定好的图片大小，注释掉该行即可。
            // ImageSource.SetNativeSize();
        }

        public void Play()
        {
            IsPlaying = true;
            Foward = true;
        }

        public void setSprite(Sprite[] sprites)
        {
            if(SpriteFrames==null)
                SpriteFrames=new List<Sprite>();
            else
                SpriteFrames.Clear();

            for (int i = 0; i < sprites.Length; i++)
            {
                SpriteFrames.Add(sprites[i]);
            }
        }

        object obj;
        public void Play(CallBackOver over,object obj)
        {
            this.over = over;
            this.obj = obj;
            this.IsPlaying = true;
            this.Foward = true;
        }

        public void PlayReverse()
        {
            IsPlaying = true;
            Foward = false;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        private long curtime = 0;
        void Update()
        {
            if (!IsPlaying || 0 == FrameCount)
            {
                return;
            }

            if (intervalTime!=0&&curtime!=0)
            {
                if (TimeKit.currentTimeMillis - curtime > intervalTime)
                {
                    curtime = 0;
                    SetSprite(0);
                    return;
                }
                else
                {
                    return;
                }
            }

            mDelta += Time.deltaTime;
            if (mDelta > 1/FPS)
            {
                mDelta = 0;
                if (Foward)
                {
                    mCurFrame++;
                }
                else
                {
                    mCurFrame--;
                }

                if (mCurFrame >= FrameCount)
                {
                    if (Loop)
                    {
                        mCurFrame = 0;
                        if (intervalTime != 0)
                        {
                            curtime = TimeKit.currentTimeMillis;
                            return;
                        }
                    }
                    else
                    {
                       
                        IsPlaying = false;
                        if (over != null)
                        {
                            over(obj);
                        }
                        return;
                    }
                }
                else if (mCurFrame < 0)
                {
                    if (Loop)
                    {
                        mCurFrame = FrameCount - 1;
                    }
                    else
                    {
                        IsPlaying = false;
                        return;
                    }
                }

                SetSprite(mCurFrame);
            }
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Resume()
        {
            if (!IsPlaying)
            {
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            IsPlaying = false;
        }

        public void Rewind()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            Play();
        }

    }
}
