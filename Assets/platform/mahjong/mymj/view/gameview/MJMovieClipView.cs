using cambrian.unreal.display;
using platform.spotred.room;

namespace platform.mahjong
{
    public class MJMovieClipView:MovieClipView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        public override void playMovieClip(int type, int loc)
        {
            UnrealSpriteAnimation animation = list.get(type);

            if (loc == RoomPanel.LOC_DOWN)
            {
                DisplayKit.setLocalXY(animation.transform, 0, -117);
            }
            //右
            else if (loc == RoomPanel.LOC_RIGHT)
            {
                DisplayKit.setLocalXY(animation.transform, 296, 0);
            }
            else if (loc == RoomPanel.LOC_TOP)
            {
                DisplayKit.setLocalXY(animation.transform, 0, 117);
            }
            else if (loc == RoomPanel.LOC_LEFT)
            {
                DisplayKit.setLocalXY(animation.transform, -296, 0);
            }


            for (int i = 0; i < list.count; i++)
            {
                UnrealSpriteAnimation sprite = list.get(i);
                sprite.Stop();
                sprite.gameObject.SetActive(false);
            }

            animation.gameObject.SetActive(true);
            animation.Play(playover, 0);
        }
    }
}
