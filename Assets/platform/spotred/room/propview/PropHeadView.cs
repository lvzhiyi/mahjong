using cambrian.common;
using cambrian.game;
using basef.prop;
using scene.game;
using platform.spotred.waitroom;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class PropHeadView:UnrealLuaSpaceObject
    {
        Image img;

        [HideInInspector] public PropEffectView effect;

        //翻牌动画
        public delegate void MoveOver(int sid);
        MoveOver moveover;

        float time = 0.6f;

        Vector2 initpos;

        protected override void xinit()
        {
            this.img = this.transform.Find("img").GetComponent<Image>();
            this.img.gameObject.SetActive(false);
            this.effect = this.transform.Find("propeffect").GetComponent<PropEffectView>();
            this.effect.init();
            this.initpos = this.transform.localPosition;
        }

        public void moveImage(Vector2 dest, int sid, MoveOver moveover)
        {
            Prop prop = (Prop)Sample.factory.newSample(sid);
            this.img.sprite = CacheManager.shopimg[prop.img];
            this.img.transform.localPosition = Vector2.zero;
            this.img.gameObject.SetActive(true);

            dest -= initpos;
            this.img.transform.DOLocalMove(dest, time).SetEase(Ease.Linear).OnComplete(() => {
                this.img.gameObject.SetActive(false);
                if (sid == 13001)//鲜花
                {
                    SoundManager.playRoseSound();
                }
                else if (sid == 13002)//鸡蛋
                {
                    SoundManager.playEggSound();
                }
                else if(sid==13004)
                {
                    SoundManager.playKissSound();
                }
                else if (sid == 13005)
                {
                    SoundManager.playBoomSound();
                }
                moveover(sid);
            });
        }
    }
}
