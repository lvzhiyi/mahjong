using System;
using UnityEngine;
using XLua;

namespace cambrian.uui
{
    /// <summary>
    /// 动画
    /// </summary>
    [Hotfix]
    public class UUIMovieClip : UnrealContainer
    {
        GameObject model;

        public override void init()
        {
            base.init();
            this.model = this.transform.Find("model").gameObject;
        }

        public void addUUISprite(Url url, Action<UnrealSprite> action)
        {
            UnrealSprite obj = this.cloneAdd(this.model, url.ToString()).GetComponent<UnrealSprite>();
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.init();
            Loader.getLoader().load<UnrealSprite, Sprite>(url, obj, (uuisprite, sprite) =>
            {
                uuisprite.setSprite(sprite);
                uuisprite.setNativeSize();
                uuisprite.setVisible(false);
                action(obj);
            });
        }
        public void addUUISprites(Url url, Action<UnrealSprite[]> action)
        {
            Loader.getLoader().loads<object, Sprite>(url, null, (param, sprites) =>
            {
                UnrealSprite[] uuisprites = new UnrealSprite[sprites.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    uuisprites[i] = this.cloneAdd(this.model, sprites[i].name).GetComponent<UnrealSprite>();
                    uuisprites[i].transform.localScale = new Vector3(1, 1, 1);
                    uuisprites[i].init();
                    uuisprites[i].setSprite(sprites[i]);
                    uuisprites[i].setNativeSize();
                    uuisprites[i].setVisible(false);
                }
                action(uuisprites);
            });
        }
    }
}