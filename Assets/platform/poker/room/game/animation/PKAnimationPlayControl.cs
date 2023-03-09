using cambrian.game;
using DragonBones;

namespace platform.poker
{
    public class PKAnimationPlayControl : UnrealLuaSpaceObject
    {
        protected UnityArmatureComponent armature;

        protected string armaturename;

        protected override void xinit()
        {
            //if (transform.Find("armature"))
            //{
            //    armature = transform.Find("armature").GetComponent<UnityArmatureComponent>();
            //}
            //else armature = GetComponentInChildren<UnityArmatureComponent>();
        }

        protected override void xrefresh()
        {
            resetAll();
        }

        private void resetAll()
        {
            //armature.gameObject.SetActive(false);
            //armature.animation.Reset();
        }

        public virtual void Play(int type, int cards, int sex, string armaturenames, string animatorname)
        {
            //armaturename = armaturenames;
            //if (armaturename.Length != 0)
            //{
            //    UnityFactory.factory.BuildArmatureComponent(armaturename, "", "", "", armature.gameObject);
            //    armature.animation.Reset();
            //    armature.AddDBEventListener(EventObject.LOOP_COMPLETE, new ListenerDelegate<EventObject>(once));
            //    armature.animation.Play(animatorname);
            //    armature.gameObject.SetActive(true);
            //}

            int sid = 0;
            if (Room.room != null)
                sid = Room.room.getRule().sid;

            SoundManager.playPKEffect(type, sex,sid);
            SoundManager.playPKCard(sex, type, cards,sid);                                         //播放声音
        }

        public virtual void once(string type, EventObject eventObject)
        {
            //UnityFactory.factory.BuildArmatureComponent(armaturename, "", "", "", armature.gameObject);
            //eventObject.armature.animation.Reset();
            //eventObject.armature.animation.Stop();
            //armature.gameObject.SetActive(false);
            //armature.RemoveDBEventListener(EventObject.LOOP_COMPLETE, new ListenerDelegate<EventObject>(once));
        }
    }
}
