using cambrian.common;
using cambrian.unreal.display;
using System.Collections;
using scene.game;
using UnityEngine;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 道具特效面板
    /// </summary>
    public class PropEffectView:UnrealLuaSpaceObject
    {
        Hashtable props = new Hashtable();

        protected override void xinit()
        {
            foreach (Transform child in transform)
            {
                UnrealSpriteAnimation animation= child.GetComponent<UnrealSpriteAnimation>();
                animation.gameObject.SetActive(false);
                props.Add(StringKit.parseInt(child.name), animation);
            }
        }

        public void showIndex(int sid)
        {
            UnrealSpriteAnimation sprite=(UnrealSpriteAnimation)props[sid];

            if (sprite.FrameCount == 0)
            {
                sprite.setSprite(PropAnimationCacheManager.intance().getPropCache(sid));
            }
            sprite.gameObject.SetActive(true);
            sprite.Play(over,sid);
            
        }

        public void over(object sid)
        {
            UnrealSpriteAnimation sprite = (UnrealSpriteAnimation)props[(int)sid];
            sprite.Stop();
            sprite.gameObject.SetActive(false);
            
        }
    }
}
