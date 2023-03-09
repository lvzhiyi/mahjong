using UnityEngine;

namespace platform.spotred.playback
{
    public class PlayBackShareView : UnrealLuaSpaceObject
    {
        [HideInInspector]
        public long id;
        [HideInInspector]
        public RoomRule rule;

        protected override void xinit()
        {
            base.xinit();
        }

        public void setData(long id,RoomRule rule)
        {
            this.id = id;
            this.rule = rule;
        }
    }
}
