using cambrian.common;
using cambrian.unreal.display;
using UnityEngine;

namespace platform.spotred.waitroom
{
    public class MatchExpressMsgView: UnrealLuaSpaceObject
    {

        ArrayList<UnrealSpriteAnimation> list=new ArrayList<UnrealSpriteAnimation>();

        protected override void xinit()
        {
            list.clear();
            foreach (Transform child in transform)
            {
                list.add(child.GetComponent<UnrealSpriteAnimation>());
            }
        }


        public void showIndex(int index)
        {
            for (int i = 0; i < list.count; i++)
            {
                UnrealSpriteAnimation sprite = list.get(i);
                sprite.Stop();
                sprite.gameObject.SetActive(false);
            }

            list.get(index).gameObject.SetActive(true);
            list.get(index).Play();
        }
    }
}
