using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;


namespace cambrian.uui
{
    [RequireComponent(typeof(Image))]
    [Hotfix]
    public class SpritesList:MonoBehaviour
    {
        public List<Sprite> list;

        private Image source;

        void Awake()
        {
            this.source = GetComponent<Image>();
        }

        public void ShowIndex(int index)
        {
            ShowIndex(index,true);
        }

        public Image getSource()
        {
            return source;
        }

        /// <summary>
        /// 设置图片的透明度
        /// </summary>
        public void resetFade()
        {
            source.DOFade(1, 0);
        }

        public void setFade(float end,float time,TweenCallback back)
        {
            source.DOFade(end, time).onComplete=back;
        }

        /// <summary>
        /// 显示指定索引图片，是否使用图片原始大小
        /// </summary>
        /// <param name="index"></param>
        /// <param name="b"></param>
        public void ShowIndex(int index,bool b)
        {
            if (this.source == null)
                this.source = GetComponent<Image>();
            this.source.sprite = list[index];
            if(b) this.source.SetNativeSize();
        }

        public void setVisible(bool b)
        {
            this.gameObject.SetActive(b);
        }
    }
}
