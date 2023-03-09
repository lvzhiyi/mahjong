using cambrian.common;
using cambrian.unreal.display;
using UnityEngine;

namespace platform.spotred.room
{
    public class MovieClipView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 吃
        /// </summary>
        public const int MC_TYPE_CHI = 0;
        /// <summary>
        /// 扯
        /// </summary>
        public const int MC_TYPE_CHE = 1;
        /// <summary>
        /// 报牌
        /// </summary>
        public const int MC_TYPE_BAO = 2;
        /// <summary>
        /// 碰
        /// </summary>
        public const int MC_TYPE_PENG = 3;

        protected ArrayList<UnrealSpriteAnimation> list;

        protected override void xinit()
        {
            list = new ArrayList<UnrealSpriteAnimation>();
            foreach (Transform child in transform)
            {
                list.add(child.GetComponent<UnrealSpriteAnimation>());
            }
        }

        public virtual void playMovieClip(int type, int loc)
        {
            UnrealSpriteAnimation animation= list.get(type);

            if (loc == RoomPanel.LOC_DOWN)
            {
                DisplayKit.setLocalXY(animation.transform, 0, 0);
            }
            //右
            else if (loc == RoomPanel.LOC_RIGHT)
            {
                DisplayKit.setLocalXY(animation.transform, 300, 50);
            }
            else if (loc == RoomPanel.LOC_TOP)
            {
                DisplayKit.setLocalXY(animation.transform, 0, 150);
            }
            else if (loc == RoomPanel.LOC_LEFT)
            {
                DisplayKit.setLocalXY(animation.transform, -300, 50);
            }


            for (int i = 0; i < list.count; i++)
            {
                UnrealSpriteAnimation sprite = list.get(i);
                sprite.Stop();
                sprite.gameObject.SetActive(false);
            }

            animation.gameObject.SetActive(true);
            animation.Play(playover,0);


//            mc.gameObject.SetActive(true);
//            mc.gotoAndPlay(0);
        }

        public void playover(object obj)
        {
            this.setVisible(false);
        }
    }
}
